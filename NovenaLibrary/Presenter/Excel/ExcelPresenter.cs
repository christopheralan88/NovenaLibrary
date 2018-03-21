using System;
using System.Collections.Generic;
using System.Linq;
using MSExcel = Microsoft.Office.Interop.Excel;
using QueryBuilder.DatabaseConnections;
using QueryBuilder.SqlGenerators;
using QueryBuilder.Config;
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
        private readonly string[] NULL_EQUIVALENTS = new string[] { "(blank)" };

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

                if (_workbookPropertiesConfig.RefreshColumnHeaders)
                {
                    int headerColumns = headersArray.Length;
                    MSExcel.Range headerStartRange = startRange.Offset[-1, 0];
                    headerStartRange = startRange.Offset[-1, 0].Resize[1, headerColumns];
                    headerStartRange.Value = headersArray;
                }

                var rows = array.GetLength(0);
                var columns = array.GetLength(1);
                startRange = startRange.Resize[rows, columns];
                startRange.NumberFormat = "General"; // reset the cells to the default format, so that Excel can automatically adjust the cell's format based on the cell value.
                startRange.Value = array;

                RefreshDataSourcePivotTables(query);
            }
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

        public void CreateDrilldownExcelWorksheet(Dictionary<string, DataTable> query)
        {
            var sql = query.Keys.First();
            var dataTable = query[sql];

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
            string sql = _workbookPropertiesConfig.CurrentSQL;

            if (sql != null)
            {
                try
                {
                    DataTable dt = _dbConnection.Query(sql);
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

        public Dictionary<string, DataTable> Drilldown()
        {
            Dictionary<string, DataTable> dict = new Dictionary<string, DataTable>();
            MSExcel.Range thisCell = _app.ActiveCell;

            if (!IsPivotCell(thisCell) && !thisCell.HasFormula)
            {
                // thisCell is not on a pivot table and has no formuala, so it can't have prcedents on a pivot table.
                MessageBox.Show("Cannot drill on this cell.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else if (IsPivotCell(thisCell) && !thisCell.HasFormula)
            {
                // thisCell is in pivot table.
                try
                {
                    var query = CreateQueryFromPivotCell(thisCell);
                    if (query != null) {
                        var sql = _sqlGenerator.CreateSql((Query)query);
                        var dt = _dbConnection.Query(sql);
                        if (dt.Rows.Count == 0) {
                            MessageBox.Show("Query returned no results.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return null;
                        }
                        dict.Add(sql, dt);
                        return dict;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            else if (!IsPivotCell(thisCell) && thisCell.HasFormula)
            {
                // thisCell is not on a pivot table, but it's precendents may be on a pivot table
                if (!PrecedentsArePivotCells(thisCell))
                {
                    // static report cell mapping
                    try
                    {
                        var query = CreateQueryFromRange(thisCell);
                        if (query != null) {
                            var sql = _sqlGenerator.CreateSql((Query)query); // Cast because CreateQueryFromRange returns nullable Query type.
                            var dt = _dbConnection.Query(sql);
                            dict.Add(sql, dt);
                            return dict;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else
                {
                    // dynamic report cell mapping
                    MSExcel.Range pCell = FindPrecedentCellEqualToCell(thisCell);
                    if (pCell != null)
                    {
                        try
                        {
                            var query = CreateQueryFromPivotCell(pCell);
                            if (query != null) {
                                var sql = _sqlGenerator.CreateSql((Query)query);
                                var dt = _dbConnection.Query(sql);
                                dict.Add(sql, dt);
                                return dict;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Returns Query object to be passed to SqlGenerator, which transforms the Query object into a SQL string.  
        /// This is meant to be used with a Range that is NOT a pivot cell.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns>Query</returns>
        private Query? CreateQueryFromRange(MSExcel.Range cell)
        {
            // create new Query object
            var query = new Query("drilldown");

            // Add main query's criteria
            if (_workbookPropertiesConfig.LastMainQuery.Criteria != null)
            {
                query.AddMultipleCriteria(_workbookPropertiesConfig.LastMainQuery.Criteria);
            }

            // Loop through named ranges.
            // Create list of named ranges that are novena named ranges and intersect with the cell parameter.
            var namedRanges = _app.ActiveWorkbook.Names;
            var relevantNamedRanges = new List<MSExcel.Name>();
            foreach (MSExcel.Name name in namedRanges)
            {
                // test if named range is a novena named range (starts with "novena__")
                if (IsNovenaNamedRange(name))
                {
                    // find named ranges that have an address that intersects with cell's column or row
                    if (name.RefersToRange.Column == cell.Column || name.RefersToRange.Row == cell.Row)
                    {
                        relevantNamedRanges.Add(name);
                    }
                }
            }

            if (relevantNamedRanges.Count > 0)
            {
                foreach (var name in relevantNamedRanges)
                {
                    // add Criteria to Query that includes column (parsed named range), operator ("IN"), and filter (cell value)
                    var criteria = new Criteria();

                    criteria.AndOr = Conjunction.And;
                    criteria.FrontParenthesis = null;
                    criteria.Column = ParseColumnFromNamedRange(name); // Wrap in try/catch block to catch exception
                    criteria.Operator = Operator.In;
                    criteria.Filter = (string)name.RefersToRange.Text;
                    criteria.EndParenthesis = null;

                    query.AddSingleCriteria(criteria);
                }

                return query;
            }
            else
            {
                // if no named ranges that intersect with cell's column and row, then return null
                MessageBox.Show("Cannot drill on this cell", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        /// <summary>
        /// Returns Query object to be passed to SqlGenerator, which transforms the Query object into SQL string.
        /// This is meant to be used with a Range that is a pivot cell.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns>Query</returns>
        private Query? CreateQueryFromPivotCell(MSExcel.Range cell)
        {
            // First check if pivot cell is a type that can be drilled into (custom subtotal, subtotal, grand total, or cell value.
            // If not, then throw exception.
            if (!PivotCellTypeIsAmount(cell.PivotCell))
            {
                throw new Exception("Cannot drill into this cell");
            }

            var query = new Query("drilldown");

            query.SetColumns(_workbookPropertiesConfig.DrilldownSql.Split(',').ToList());
            var table = _workbookPropertiesConfig.SelectedTable + "_detail";
            query.SetTable(table);
            query.SetSuppressNulls(false);

            try
            {
                var tableSchema = _dbConnection.GetColumns(table);
                query.SetTableSchema(tableSchema);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Add row fields and items.
            Criteria criteria;
            foreach (MSExcel.PivotItem item in cell.PivotCell.RowItems)
            {
                criteria = new Criteria();

                var columnName = item.Parent.Name;

                criteria.AndOr = Conjunction.And;
                //criteria.FrontParenthesis = null;
                criteria.Column = columnName;
                criteria.Operator = Operator.EqualTo;
                //criteria.EndParenthesis = null;

                if (NULL_EQUIVALENTS.Contains(item.Name))
                {
                    criteria.OrIsNull = true;
                } 
                else
                {
                    criteria.Filter = item.Name;
                }

                query.AddSingleCriteria(criteria);
            }

            // Add column fields and items.
            foreach (MSExcel.PivotItem item in cell.PivotCell.ColumnItems)
            {
                criteria = new Criteria();

                var columnName = item.Parent.Name;

                criteria.AndOr = Conjunction.And;
                //criteria.FrontParenthesis = null;
                criteria.Column = columnName;
                criteria.Operator = Operator.EqualTo;
                //criteria.EndParenthesis = null;

                if (NULL_EQUIVALENTS.Contains(item.Name))
                {
                    criteria.OrIsNull = true;
                }
                else
                {
                    criteria.Filter = item.Name;
                }

                query.AddSingleCriteria(criteria);
            }

            // Add all other pivot fields and items that are visible.
            try
            { 
                // Loop thru each drilldown table field
                foreach (DataRow row in query.TableSchema.Rows)
                {
                    var field = row["COLUMN_NAME"].ToString();

                    // If a criteria does not already exist for this field/column
                    if (!query.CriteriaExistsForColumn(field))
                    {
                        // Only attempt to add fields that exist in the pivot table (ie: fields that have items in the pivot table).
                        if (FieldExistsInPivotTable(field, cell.PivotCell))
                        {
                            // And the field if is NOT a data field.
                            if (!IsPivotTableDataField(cell.PivotCell, field))
                            {
                                var fieldHasNullItems = false;
                                MSExcel.PivotItems fieldItems = cell.PivotCell.PivotTable.PivotFields(field).PivotItems();
                                var filter = "";

                                foreach (MSExcel.PivotItem item in fieldItems)
                                {
                                    if (item.Visible)
                                    {
                                        if (NULL_EQUIVALENTS.Contains(item.Name))
                                        {
                                            fieldHasNullItems = true;
                                        }
                                        else
                                        {
                                            filter += $"{item.Name},";
                                        }
                                    }
                                }

                                if (filter.Length > 0)
                                {
                                    filter = filter.Substring(0, filter.Length - 1);

                                    criteria = new Criteria();

                                    filter.Substring(0, filter.Length - 1);

                                    criteria.AndOr = Conjunction.And;
                                    //criteria.FrontParenthesis = (fieldHasNullItems) ? "(" : null;
                                    criteria.Column = field;
                                    criteria.Operator = Operator.In;
                                    criteria.Filter = filter;
                                    criteria.OrIsNull = (fieldHasNullItems) ? true : false;
                                    //criteria.Filter = (fieldHasNullItems) ? $"{filter} OR {field} IS NULL" : filter;
                                    //criteria.EndParenthesis = (fieldHasNullItems) ? ")" : null;

                                    query.ReplaceAllMatchingCriteria(criteria);
                                }

                                //if (filter.Length > 0)
                                //{
                                //    criteria = new Criteria();

                                //    filter.Substring(0, filter.Length - 1);

                                //    criteria.AndOr = "AND";
                                //    criteria.FrontParenthesis = (fieldHasNullItems) ? "(" : null;
                                //    criteria.Column = field;
                                //    criteria.Operator = "In";
                                //    criteria.Filter = (fieldHasNullItems) ? $"{filter} OR {field} IS NULL" : filter;
                                //    criteria.EndParenthesis = (fieldHasNullItems) ? ")" : null;

                                //    query.ReplaceAllMatchingCriteria(criteria);
                                //}
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Drilldown", MessageBoxButtons.OK);
                return null;
            }

            return query;
        }

        private bool IsPivotTableDataField(MSExcel.PivotCell cell, string fieldName)
        {
            MSExcel.PivotFields dataFields = cell.PivotTable.DataFields;

            foreach (MSExcel.PivotField dataField in dataFields)
            {
                if (dataField.SourceName == fieldName) return true;
            }

            return false;
        }

        private bool IsNovenaNamedRange(MSExcel.Name namedRange)
        {
            // get first 7 characters of named range
            var endIndex = namedRange.Name.IndexOf("__") + 2;
            var first7Chars = namedRange.Name.Substring(0, endIndex ).ToLower();

            return (first7Chars == "novena__") ? true : false; 
        }

        private string ParseColumnFromNamedRange(MSExcel.Name namedRange)
        {
            // get column name from named range's name
            var startIndex = namedRange.Name.IndexOf("__");
            if (startIndex != -1)
            {
                // if novena
                return namedRange.Name.Substring(startIndex + 1);
            }
            else
            {
                return null;
            }
            

            // return column name
        }

        private bool PivotCellTypeIsAmount(MSExcel.PivotCell pivotCell)
        {
            if (pivotCell.PivotCellType == MSExcel.XlPivotCellType.xlPivotCellCustomSubtotal || 
                pivotCell.PivotCellType == MSExcel.XlPivotCellType.xlPivotCellGrandTotal ||
                pivotCell.PivotCellType == MSExcel.XlPivotCellType.xlPivotCellSubtotal ||
                pivotCell.PivotCellType == MSExcel.XlPivotCellType.xlPivotCellValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool FieldExistsInPivotTable(string field, MSExcel.PivotCell cell)
        {
            MSExcel.PivotFields pFields = cell.PivotTable.PivotFields();
            foreach (MSExcel.PivotField pField in pFields)
            {
                if (pField.Name.Equals(field))
                {
                    return true;
                }
            }

            return false;
        }

        private MSExcel.Range FindPrecedentCellEqualToCell(MSExcel.Range thisCell)
        {
            if (thisCell.Precedents.Count > 0)
            {
                foreach (MSExcel.Range cell in thisCell.Precedents)
                {
                    if (IsPivotCell(cell))
                    {
                        if (cell.PivotCell.PivotCellType == MSExcel.XlPivotCellType.xlPivotCellValue)
                        {
                            if (cell.Value == thisCell.Value)
                            {
                                return cell;
                            }
                        }
                    }
                }
                MessageBox.Show("Cannot drill into this cell", "Drilldown Error", MessageBoxButtons.OK);
                return null;
            }
            else
            {
                MessageBox.Show("Cannot drill into this cell", "Drilldown Error", MessageBoxButtons.OK);
                return null;
            }
        }

        private bool PrecedentsArePivotCells(MSExcel.Range cell)
        {
            foreach (MSExcel.Range range in cell.Precedents)
            {
                if (range.PivotCell != null) return true;
            }
            return false;
        }

        private bool IsPivotCell(MSExcel.Range cell)
        {
            try
            {
                string answer = cell.PivotCell.Range.Address;
                return true;
            }
            catch (Exception)
            {
                return false;
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
