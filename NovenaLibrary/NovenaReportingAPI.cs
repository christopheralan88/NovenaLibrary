using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Config;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using NovenaLibrary.View;

namespace NovenaLibrary
{
    public class NovenaReportingAPI
    {
        public AppConfig appConfig;
        public WorkbookPropertiesConfig wBookPropertiesConfig;
        public Excel.Application app;
        //public ExcelPresenter presenter;


        public NovenaReportingAPI(Excel.Application application)
        {
            appConfig = new AppConfig();
            app = application;
            try
            {
                wBookPropertiesConfig = new WorkbookPropertiesConfig(application.ActiveWorkbook).LoadWorkbookProperties();
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            //presenter = new ExcelPresenter(application, appConfig, wBookPropertiesConfig);
        }

        //public void signIn()
        //{
        //    LoginForm loginForm = new LoginForm(appConfig);
        //    DialogResult result = loginForm.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        appConfig = loginForm.AppConfig;
        //    }
        //}

        public void ShowSqlCreator()
        {
            if (appConfig.Username != null && appConfig.Password != null)
            {
                var sqlCreator = new SqlCreatorView(appConfig, wBookPropertiesConfig);
                DialogResult result = sqlCreator.ShowDialog();
                if (result == DialogResult.OK)
                {
                    wBookPropertiesConfig = sqlCreator.WorkbookPropertiesConfig;
                    //presenter.copyQueryIntoExcel(sqlCreator.queryResults);
                }
            }
            else
            {
                //signIn();
            }
        }

        //public void setDrilldownColumns()
        //{
        //    if (appConfig.username != null && appConfig.password != null)
        //    {
        //        DrilldownColumns drilldownColumns = new DrilldownColumns(appConfig, wBookPropertiesConfig);
        //        DialogResult result = drilldownColumns.ShowDialog();
        //        if (result == DialogResult.OK)
        //        {
        //            wBookPropertiesConfig = drilldownColumns.WorkbookPropertiesConfig;
        //        }
        //    }
        //    else
        //    {
        //        signIn();
        //    }
        //}

        //public void drilldown()
        //{
        //    if (appConfig.username != null && appConfig.password != null)
        //    {
        //        if (wBookPropertiesConfig.drilldownSql == null)
        //        {
        //            setDrilldownColumns();
        //        }
        //        else
        //        {
        //            Dictionary<string, DataTable> dict = new Dictionary<string, DataTable>();
        //            dict = presenter.drilldown();

        //            if (dict != null)
        //            {
        //                string sql = dict.ElementAt(0).Key;
        //                DataTable dt = dict.ElementAt(0).Value;

        //                presenter.createDrilldownExcelWorksheet(sql, dt);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        signIn();
        //    }
        //}

        //public void refreshData()
        //{
        //    if (appConfig.username != null && appConfig.password != null)
        //    {
        //        DataTable dt = presenter.refreshData();
        //        if (dt != null)
        //        {
        //            presenter.copyQueryIntoExcel(dt);
        //        }
        //    }
        //    else
        //    {
        //        signIn();
        //    }
        //}

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

        //public Hashtable editConfiguration(string dbConnStrings)
        //{
        //    EditConfiguration editConfig = new EditConfiguration(dbConnStrings);
        //    DialogResult result = editConfig.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        // wipe appConfig object, which reloads the ConnectionString and SQL_getAvailableTables settings set above
        //        appConfig = new AppConfig();

        //        // wipe all wBookConfig properties except for "properties" and "wBookProperties" 
        //        wBookPropertiesConfig.currentSql = "";
        //        wBookPropertiesConfig.selectedTable = "";
        //        wBookPropertiesConfig.selectedColumns = new List<string>();
        //        wBookPropertiesConfig.criteria = new List<string>();
        //        wBookPropertiesConfig.drilldownSql = "";
        //        wBookPropertiesConfig.dependentTables = new Dictionary<string, string>();
        //        appConfig.username = null;
        //        appConfig.password = null;

        //        Hashtable results = new Hashtable();
        //        results.Add("dbConnStrings", editConfig.dbConnStrings);
        //        results.Add("activeConnectionString", editConfig.activeConnectionString);
        //        results.Add("activeDatabaseType", (int)editConfig.activeDatabaseType);
        //        results.Add("availableTablesSQL", editConfig.availableTablesSQL);
        //        return results;
        //    }
        //    return null;
        //}
    }
}
