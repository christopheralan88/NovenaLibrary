using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSExcel = Microsoft.Office.Interop.Excel;
using NovenaLibrary.Repositories;
using NovenaLibrary.SqlGenerators;
using NovenaLibrary.Config;
using System.Data;
using System.Windows.Forms;

namespace NovenaLibrary.Presenter.Excel
{
    public class ExcelPresenter
    {
        private MSExcel.Application _app;
        private MSExcel.Workbook _view;
        private IDatabaseConnection _dbConnection;
        private BaseSqlGenerator _sqlGenerator;
        private WorkbookPropertiesConfig _workbookPropertiesConfig;

        public ExcelPresenter(MSExcel.Application app, IDatabaseConnection dbConnection, BaseSqlGenerator sqlGenerator, WorkbookPropertiesConfig workbookPropertiesConfig)
        {
            _app = app;
            _view = app.ActiveWorkbook;
            _workbookPropertiesConfig = workbookPropertiesConfig;
            _dbConnection = dbConnection;
            _sqlGenerator = sqlGenerator;
        }

        public void RefreshAllPivotTablesWithCurrentDataSource()
        {
            foreach (MSExcel.Worksheet sheet in _view.Sheets)
            {
                foreach (MSExcel.PivotTable pivotTable in sheet.PivotTables())
                {
                    try
                    {
                        pivotTable.RefreshTable();
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
        }

        public void PasteQueriesIntoExcel(Dictionary<string, DataTable> queries)
        {
            RefreshAllPivotTablesWithCurrentDataSource();

            foreach (var query in queries)
            {
                string[] headersArray = ConvertDataTableHeadersToArray(query.Value);
                object[,] array = ConvertDataTableToArray(query.Value);

                MSExcel.Worksheet sheet;
                try
                {
                    sheet = _view.Sheets[query.Key];
                }
                catch (Exception)
                {
                    _app.Worksheets.Add();
                    _app.ActiveWorkbook.ActiveSheet.Name = query.Key; // might need to move Name property assignment to another line...compiler is kind of picky sometime.
                    sheet = _app.ActiveWorkbook.ActiveSheet;
                }
                
                MSExcel.Range startRange;
                if (query.Key.Equals("main"))
                {
                    startRange = _app.Range["StartCell"];
                }
                else
                {
                    startRange = sheet.Range["A2"];
                }
                startRange.CurrentRegion.ClearContents();
                //startRange.Resize[rows, columns].ClearContents();

                if (_workbookPropertiesConfig.refreshColumnHeaders)
                {
                    int headerColumns = headersArray.Length;
                    MSExcel.Range headerStartRange = startRange.Offset[-1, 0];
                    headerStartRange = startRange.Offset[-1, 0].Resize[1, headerColumns];
                    headerStartRange.Value = headersArray;
                }

                var rows = array.GetLength(0);
                var columns = array.GetLength(1);
                startRange = startRange.Resize[rows, columns];
                startRange.Value = array;
                startRange.CurrentRegion.NumberFormat = "General";

                RefreshDataSourcePivotTables(query);
            }

            //string[] headersArray = ConvertDataTableHeadersToArray(queries);
            //object[,] array = ConvertDataTableToArray(queries);

            //MSExcel.Worksheet sheet = _view.Sheets["Data"];
            //MSExcel.Range startRange = _app.Range["StartCell"];
            ////MSExcel.Range startRange = sheet.Range["StartCell"];
            //startRange.CurrentRegion.ClearContents();

            //if (_workbookPropertiesConfig.refreshColumnHeaders)
            //{
            //    int headerColumns = headersArray.Length;
            //    MSExcel.Range headerStartRange = startRange.Offset[-1, 0];
            //    headerStartRange = startRange.Offset[-1, 0].Resize[1, headerColumns];
            //    headerStartRange.Value = headersArray;
            //}

            //var rows = array.GetLength(0);
            //var columns = array.GetLength(1);
            //startRange = startRange.Resize[rows, columns];
            //startRange.Value = array;
            //startRange.CurrentRegion.NumberFormat = "General";

            //RefreshAllPivotTablesWithNewDataSource();
        }

        public void RefreshDataSourcePivotTables(KeyValuePair<string, DataTable> query)
        {
            foreach (MSExcel.Worksheet sheet in _view.Sheets)
            {
                foreach (MSExcel.PivotTable pivotTable in sheet.PivotTables())
                {
                    _app.EnableEvents = false;
                    try
                    {
                        string sourceSheet = pivotTable.Name.Substring(0, pivotTable.Name.IndexOf("__"));

                        if (sourceSheet.ToLower().Equals(query.Key.ToLower()))
                        {
                            MSExcel.Range startRange;
                            if (query.Key.Equals("main"))
                            {
                                startRange = _app.Range["StartCell"];
                            }
                            else
                            {
                                startRange = _app.Worksheets[query.Key].Range["A2"];
                            }
                            var rows = query.Value.Rows.Count;
                            var columns = query.Value.Columns.Count;
                            MSExcel.Range dataSource = startRange.Offset[-1,0].Resize[rows + 1, columns]; // Offsest and resize +1 because of header row.
                            MSExcel.PivotCache cache = _view.PivotCaches().Create(SourceType: MSExcel.XlPivotTableSourceType.xlDatabase,
                                                                                  SourceData: dataSource);
                            pivotTable.ChangePivotCache(cache);
                            pivotTable.RefreshTable();
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        // Catches ArgumentOutOfRangeException that is raised when IndexOf() does not find "__" in pivottable name and returns -1.  
                        // Substring will raise a ArgumentOutOfRangeException because length parameter is -1 and is therefore less than 0.
                        // Continue execution, because this simply means the pivot table is not tied to the data source we are looking for.
                        _app.EnableEvents = true;
                        continue;
                    }
                    catch (Exception ex)
                    {
                        _app.EnableEvents = true;
                        MessageBox.Show(ex.Message, "Pivot Table Refresh Exception", MessageBoxButtons.OK);
                    }
                    _app.EnableEvents = true;
                }
            }
        }

        //public void RefreshAllPivotTablesWithNewDataSource()
        //{
        //    foreach (MSExcel.Worksheet sheet in _view.Sheets)
        //    {
        //        foreach (MSExcel.PivotTable pTable in sheet.PivotTables())
        //        {
        //            _app.EnableEvents = false;
        //            try
        //            {
        //                string sourceSheet = pTable.Name.Substring(0, pTable.Name.IndexOf("__"));
        //                MSExcel.Worksheet wSheet = _view.Sheets[sourceSheet];
        //                MSExcel.Range startRange = wSheet.Range["startCell"];
        //                MSExcel.Range dataSource = startRange.CurrentRegion;
        //                MSExcel.PivotCache cache = _view.PivotCaches().Create(SourceType: MSExcel.XlPivotTableSourceType.xlDatabase,
        //                    SourceData: dataSource);
        //                pTable.ChangePivotCache(cache);
        //                pTable.RefreshTable();
        //            }
        //            catch (ArgumentException)
        //            {
        //                _app.EnableEvents = true;
        //                continue;
        //            }
        //            catch (Exception)
        //            {
        //                _app.EnableEvents = true;
        //                MessageBox.Show(string.Format("The data may not be a continuous block.  The data source for {0} may need to be adjusted",
        //                    pTable.Name), "Error", MessageBoxButtons.OK);
        //            }
        //            _app.EnableEvents = true;
        //        }
        //    }
        //}

        public void CreateDrilldownExcelWorksheet(string sql, DataTable dataTable)
        {
            int sheetCount = 1;

            foreach (MSExcel.Worksheet sheet in _view.Sheets)
            {
                if (sheet.Name.Contains("Drilldown"))
                {
                    sheetCount++;
                }
            }

            //the added sheet becomes the active sheet automatically.
            _view.Sheets.Add(_view.ActiveSheet);
            _view.ActiveSheet.Name = "Drilldown_" + sheetCount;
            _view.ActiveSheet.Range["A1"].Value = sql;

            MSExcel.Range rng = _view.ActiveSheet.Range["B3"];
            foreach (DataColumn column in dataTable.Columns)
            {
                rng.Value = column.ColumnName;
                rng = rng.Offset[0, 1];
            }

            _view.Application.ScreenUpdating = false;

            object[,] array = ConvertDataTableToArray(dataTable);
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            MSExcel.Worksheet activeSheet = _view.ActiveSheet;
            MSExcel.Range startRange = activeSheet.Range["B4"];
            startRange = startRange.Resize[rows, columns];
            startRange.Value = array;
            activeSheet.ListObjects.Add(SourceType: MSExcel.XlListObjectSourceType.xlSrcRange,
                                        Source: activeSheet.Range["B4"].CurrentRegion,
                                        XlListObjectHasHeaders: MSExcel.XlYesNoGuess.xlYes).Name = "Table";
            activeSheet.ListObjects["Table"].TableStyle = "TableStyleMedium6";

            _view.Application.ScreenUpdating = true;
        }

        public DataTable RefreshData()
        {
            string sql = _workbookPropertiesConfig.currentSql;

            if (sql != null)
            {
                try
                {
                    DataTable dt = _dbConnection.query(sql);
                    if (dt.Rows.Count <= 0)
                    {
                        MessageBox.Show("Query returned no results", "No Results", MessageBoxButtons.OK);
                        return null;
                    }
                    else
                    {
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("You must run the Query Creator once before being allowed to refresh the query",
                        "Run SQL Creator First", MessageBoxButtons.OK);
                return null;
            }
        }

        private bool StartCellExists()
        {
            try
            {
                MSExcel.Range testVariable = _app.Range["StartCell"];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string[] ConvertDataTableHeadersToArray(DataTable dt)
        {
            string[] headersArray = new string[dt.Columns.Count];

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                headersArray[i] = dt.Columns[i].ColumnName;
            }

            return headersArray;
        }

        private object[,] ConvertDataTableToArray(DataTable dt)
        {
            object[,] array = new object[dt.Rows.Count, dt.Columns.Count];

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    array[r, c] = (object)dt.Rows[r][c];
                }
            }

            return array;
        }
    }
}
