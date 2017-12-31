using System;
using System.Collections.Generic;
using NovenaLibrary.Config;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using NovenaLibrary.View.LogIn;
using NovenaLibrary.View.SqlCreator;
using NovenaLibrary.Presenter.Excel;
using System.Data;
using NovenaLibrary.View.DrilldownColumns;
using NovenaLibrary.View.ConfigurationEditor;
using System.Collections;
using NovenaLibrary.Utilities;

namespace NovenaLibrary
{
    public class NovenaReportingAPI
    {
        public AppConfig _appConfig;
        public WorkbookPropertiesConfig _workbookPropertiesConfig;
        public Excel.Application _application;
        public ExcelPresenter _presenter;

        public NovenaReportingAPI(Excel.Application application, string connectionString, string availableTablesSQL, DatabaseType databaseType)
        {
            _appConfig = new AppConfig(connectionString, availableTablesSQL, databaseType);
            _application = application;
            try
            {
                _workbookPropertiesConfig = new WorkbookPropertiesConfig(application.ActiveWorkbook).LoadWorkbookProperties();
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            _presenter = new ExcelPresenter(application, 
                                           new DatabaseConnectionFactory().CreateDbConnection(_appConfig.DatabaseType, _appConfig.ConnectionString), 
                                           new SqlGeneratorFactory().CreateSqlGenerator(databaseType), 
                                           _workbookPropertiesConfig);
        }

        public void LogIn()
        {
            var loginForm = new LogInView(_appConfig);
            DialogResult result = loginForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                _appConfig = loginForm.AppConfig;
            }
        }

        public void ShowSqlCreator()
        {
            if (_appConfig.GetCredentialsRequired == AppConfig.CredentialsRequired.None || _appConfig.User != null)
            {
                var sqlCreator = new SqlCreatorView(_appConfig, _workbookPropertiesConfig);
                DialogResult result = sqlCreator.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _workbookPropertiesConfig = sqlCreator.WorkbookPropertiesConfig;

                    //var queries = new Dictionary<string, DataTable>();
                    //queries.Add("main", sqlCreator.SQLResult);
                    var queries = new Dictionary<string, DataTable>();
                    queries = sqlCreator.SQLResult; // add main query to dict

                    if (_workbookPropertiesConfig.LastMainQuery != null)
                    {
                        var dbConnection = new DatabaseConnectionFactory().CreateDbConnection(_appConfig.DatabaseType, _appConfig.ConnectionString);
                        foreach (KeyValuePair<string, string> query in _workbookPropertiesConfig.dependentTables)
                        {
                            // create interpolator object
                            var interpolator = new Interpolator();

                            // test if string needs interpolation 
                            var needsInterpolation = interpolator.SetFormattable(query.Value).SetCriteria(_workbookPropertiesConfig.LastMainQuery.Criteria).NeedsInterpolation();

                            // if no, then use sql to query against database and add results to queries dict
                            if (!needsInterpolation)
                            {
                                try
                                {
                                    queries.Add(query.Key, dbConnection.query(query.Value));
                                }
                                catch
                                {
                                    MessageBox.Show($"{query.Key} did not run successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                // if yes, then interpolate
                                try
                                {
                                    var interpolatedQuery = interpolator.Interpolate();
                                    queries.Add(query.Key, dbConnection.query(interpolatedQuery));
                                }
                                catch
                                {
                                    MessageBox.Show($"{query.Key} did not run successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    
                    _presenter.PasteQueriesIntoExcel(queries);
                }
            }
            else
            {
                LogIn();
            }
        }

        public void SetDrilldownColumns()
        {
            if (_appConfig.GetCredentialsRequired == AppConfig.CredentialsRequired.None || _appConfig.User != null)
            {
                var drilldownColumns = new DrilldownColumns(_appConfig, _workbookPropertiesConfig);
                DialogResult result = drilldownColumns.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _workbookPropertiesConfig = drilldownColumns.WorkbookPropertiesConfig;
                }
            }
            else
            {
                LogIn();
            }
        }

        public void Drilldown()
        {
            if (_appConfig.GetCredentialsRequired == AppConfig.CredentialsRequired.None || _appConfig.User != null)
            {
                if (_workbookPropertiesConfig.drilldownSql == null)
                {
                    SetDrilldownColumns();
                }
                else
                {
                    Dictionary<string, DataTable> query = new Dictionary<string, DataTable>();
                    query = _presenter.Drilldown();

                    if (query != null)
                    {
                        _presenter.CreateDrilldownExcelWorksheet(query);
                    }
                }
            }
            else
            {
                LogIn();
            }
        }

        public void RefreshData()
        {
            if (_appConfig.GetCredentialsRequired == AppConfig.CredentialsRequired.None || _appConfig.User != null)
            {
                DataTable dt = _presenter.RefreshData();
                if (dt != null)
                {
                    var queries = new Dictionary<string, DataTable>();
                    queries.Add("main", dt);
                    _presenter.PasteQueriesIntoExcel(queries);
                }
            }
            else
            {
                LogIn();
            }
        }

        //public void verifyReportStructure()
        //{
        //    // dict of missing structure and related message to display to user
        //    Dictionary<string, string> errors = new Dictionary<string, string>();

        //    // check for Data worsheet
        //    try
        //    {
        //        Excel.Worksheet sheet = app.ActiveWorkbook.Sheets["Data"];
        //    }
        //    catch (Exception)
        //    {
        //        errors["Data Worksheet"] = "A worksheet named 'Data' does not exist";
        //    }

        //    // check for startCell
        //    try
        //    {
        //        Excel.Range range = app.ActiveWorkbook.Sheets["Data"].Range["startCell"];
        //    }
        //    catch (Exception)
        //    {
        //        errors["Start Cell"] = "Named range, 'startCell', does not exist in Data worksheet";
        //    }

        //    // check that each pivot table's data source exists (truncate pivot table's name and search)
        //    foreach (Excel.PivotTable pt in app.ActiveWorkbook.PivotTables)
        //    {
        //        string sourceSheetName = pt.Name.Substring(0, pt.Name.IndexOf("__"));
        //        try
        //        {
        //            Excel.Worksheet sourceSheet = app.ActiveWorkbook.Sheets[sourceSheetName];
        //        }
        //        catch (Exception)
        //        {
        //            errors[sourceSheetName] = string.Format("{0} is not a sheet in this workbook.  If left uncorrected, this pivot table will not update " +
        //                "when expected", sourceSheetName);
        //        }
        //    }

        //    // check that Properties worksheet exists
        //    try
        //    {
        //        Excel.Worksheet sheet = app.ActiveWorkbook.Sheets["Properties"];
        //    }
        //    catch (Exception)
        //    {
        //        errors["Properties Worksheet"] = "A worksheet named 'Properties' does not exist";
        //    }

        //    // check that each workbook property exists in Properties worksheet
        //    foreach (string property in wBookPropertiesConfig.properties.Keys)
        //    {
        //        try
        //        {
        //            Excel.Range range = app.ActiveWorkbook.Sheets["Properties"].Range[property];
        //        }
        //        catch (Exception)
        //        {
        //            errors[property] = string.Format("Named range, '{0}', does not exist in the Properties worksheet", property);
        //        }
        //    }

        //    // show message box with 1) missing structure and 2) related message
        //    if (errors.Count > 0)
        //    {
        //        string message = "The following errors should be fixed before the workbook is saved:\r\n";
        //        foreach (KeyValuePair<string, string> error in errors)
        //        {
        //            message += string.Format("***{0}:  {1}\r\n", error.Key, error.Value);
        //        }
        //        MessageBox.Show(message, "Report Structure Errors", MessageBoxButtons.OK);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Report structure is correct!", "Report Structure Check", MessageBoxButtons.OK);
        //    }

        //}

        //public void addCellMapping()
        //{
        //    //not implemented yet
        //}

        //public void deleteCellMapping()
        //{
        //    //not implemented yet
        //}

        public Hashtable EditConfiguration(IList<string> dbConnStrings)
        {
            var editConfig = new ConfigurationEditorView(dbConnStrings);
            DialogResult result = editConfig.ShowDialog();
            if (result == DialogResult.OK)
            {
                // wipe appConfig object, which reloads the ConnectionString and SQL_getAvailableTables settings set above
                var defaultDatabaseType = editConfig.DefaultConnectionDatabaseType;
                var availableTablesSQL = AvailableTablesSql.availableTablesSql[defaultDatabaseType];
                var defaultConnectionString = editConfig.DefaultConnectionString;

                // Update class fields with new default connection string, availableTablesSQL, and database type (don't update _application field, though)
                // Updating the _presenter field here allows the new connection string and database type to be tested for validity.  If either is not valid
                // then the an exception will be throw, a message will be displayed to the user, and no settings (null) will be returned to the calling method,
                // so that no application settings are updated in the calling application.  Essentially it guards against malformed connection strings or mismatched
                // database types leaving the NovenaLibrary application and going to the calling appliation.
                try
                {
                    _presenter = new ExcelPresenter(_application,
                                                    new DatabaseConnectionFactory().CreateDbConnection(defaultDatabaseType, defaultConnectionString),
                                                    new SqlGeneratorFactory().CreateSqlGenerator(defaultDatabaseType),
                                                    _workbookPropertiesConfig);

                    _appConfig = new AppConfig(defaultConnectionString, availableTablesSQL, defaultDatabaseType);
                    _workbookPropertiesConfig.ClearWorkbookProperties();
                    //_appConfig.User = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                    return null;
                }

                // Pass connection strings to calling application method to update application's settings.
                Hashtable results = new Hashtable();
                results.Add("dbConnStrings", editConfig.DatabaseConnections);
                results.Add("activeConnectionString", defaultConnectionString);
                results.Add("activeDatabaseType", (int)defaultDatabaseType);
                results.Add("availableTablesSQL", availableTablesSQL);
                return results;
            }

            return null;
        }
    }
}
