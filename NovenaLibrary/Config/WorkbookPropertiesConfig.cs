using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using NovenaLibrary.Exceptions;
using System.Runtime.InteropServices;
using NovenaLibrary.SqlGenerators;

namespace NovenaLibrary.Config
{
    public class WorkbookPropertiesConfig
    {
        private Excel.Workbook activeWorkbook;
        public BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        public string currentSql = "";
        public List<Criteria> criteria = new List<Criteria>();
        public string drilldownSql = "";
        public Dictionary<string, string> dependentTables = new Dictionary<string, string>();
        public readonly Dictionary<string, string> properties = new Dictionary<string, string>();
        private Dictionary<string, string> propertyAddresses = new Dictionary<string, string>();
        public Excel.Worksheet propertiesSheet = null;
        public bool refreshColumnHeaders = false;
        public List<string> selectedColumns = new List<string>();
        public string selectedTable = "";
        public string limit = "1000000";
        public Query LastMainQuery;


        //generic constructor for unit testing so that workbook object does not need to be mocked or initialized in tests
        public WorkbookPropertiesConfig()
        {
        }

        public WorkbookPropertiesConfig(Excel.Workbook activeWorkbook)
        {
            properties.Add("selectedTable", "string");
            properties.Add("selectedColumns", "delimitedStringToList");
            properties.Add("criteria", "delimitedStringToCriteria");
            properties.Add("drilldownSql", "string");
            properties.Add("refreshColumnHeaders", "string");
            properties.Add("dependentTables", "delimitedStringToDict");
            properties.Add("limit", "string");

            this.activeWorkbook = activeWorkbook;

            try
            {
                propertiesSheet = GetPropertiesSheet();
            }
            catch (Exception)
            {
                try
                {
                    AddPropertiesWorksheet();
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not add a Properties worksheet.  Make sure the workbook is not protected and allows worksheets to be added.", ex);
                }
                propertiesSheet = GetPropertiesSheet();
            }
        }

        public WorkbookPropertiesConfig LoadWorkbookProperties()
        {
            try
            {
                foreach (KeyValuePair<string, string> pair in properties)
                {
                    if (pair.Value == "string")
                    {
                        LoadStringProperties(pair.Key);
                    }
                    else if (pair.Value == "delimitedStringToList")
                    {
                        LoadListProperties(pair.Key);
                    }
                    else if (pair.Value == "delimitedStringToCriteria")
                    {
                        LoadCriteriaProperties(pair.Key);
                    }
                    else if (pair.Value == "delimitedStringToDict")
                    {
                        LoadDictProperties(pair.Key);
                    }
                }
                return this;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
        }

        public void ClearWorkbookProperties()
        {
            foreach (KeyValuePair<string, string> pair in properties)
            {
                if (pair.Value == "string")
                {
                    ClearStringProperties(pair.Key);
                }
                else if (pair.Value == "delimitedStringToList")
                {
                    ClearListProperties(pair.Key);
                }
                else if (pair.Value == "delimitedStringToCriteria")
                {
                    ClearCriteriaProperties(pair.Key);
                }
                else if (pair.Value == "delimitedStringToDict")
                {
                    ClearDictProperties(pair.Key);
                }
            }
        }

        private void ClearStringProperties(string propertyName)
        {
            foreach (FieldInfo field in typeof(WorkbookPropertiesConfig).GetFields(bindingFlags))
            {
                if (field.Name == propertyName)
                {
                    try
                    {
                        field.SetValue(this, "");
                        break;
                    }
                    catch (Exception)
                    {
                        // catch exception that is throw when trying to set bool field to empty string, "".
                        field.SetValue(this, false);
                        break;
                    }
                    
                }
            }
        }

        private void ClearListProperties(string propertyName)
        {
            foreach (FieldInfo field in typeof(WorkbookPropertiesConfig).GetFields(bindingFlags))
            {
                if (field.Name == propertyName)
                {
                    field.SetValue(this, new List<string>());
                    break;
                }
            }
        }

        private void ClearCriteriaProperties(string propertyName)
        {
            foreach (FieldInfo field in typeof(WorkbookPropertiesConfig).GetFields(bindingFlags))
            {
                if (field.Name == propertyName)
                {
                    field.SetValue(this, new List<Criteria>());
                    break;
                }
            }
        }

        private void ClearDictProperties(string propertyName)
        {
            foreach (FieldInfo field in typeof(WorkbookPropertiesConfig).GetFields(bindingFlags))
            {
                if (field.Name == propertyName)
                {
                    field.SetValue(this, new Dictionary<string, string>());
                    break;
                }
            }
        }

        private void LoadStringProperties(string propertyName)
        {
            if (WorkbookPropertyExists(propertyName))
            {
                foreach (FieldInfo field in typeof(WorkbookPropertiesConfig).GetFields(bindingFlags))
                {
                    if (field.Name == propertyName)
                    {
                        field.SetValue(this, propertiesSheet.Range[propertyName].Value);
                        break;
                    }
                }
            }
            else
            {
                AddWorkbookProperty(propertyName);
            }
        }

        private void LoadListProperties(string propertyName)
        {
            if (WorkbookPropertyExists(propertyName))
            {
                string propertyAsString = propertiesSheet.Range[propertyName].Value.ToString();
                List<string> propertyAsList = propertyAsString.Split('|').ToList();
                propertyAsList.RemoveAll(s => s == "");

                foreach (FieldInfo field in typeof(WorkbookPropertiesConfig).GetFields(bindingFlags))
                {
                    if (field.Name == propertyName)
                    {
                        field.SetValue(this, propertyAsList);
                        break;
                    }
                }
            }
            else
            {
                AddWorkbookProperty(propertyName);
            }
        }

        private void LoadCriteriaProperties(string propertyName)
        {
            if (WorkbookPropertyExists(propertyName))
            {
                string propertyAsString = propertiesSheet.Range[propertyName].Value.ToString();
                List<string> propertyAsList = propertyAsString.Split('|').ToList();
                propertyAsList.RemoveAll(s => s == "");

                int propertyCount = typeof(Criteria).GetProperties().Length;
                if (propertyAsList.Count % propertyCount != 0)
                {
                    throw new IncorrectCriteriaCountException();
                }

                List<Criteria> criteriaList = new List<Criteria>();
                Criteria newCriteria = new Criteria();
                for (int i = 0; i < propertyAsList.Count; i += propertyCount)
                {
                    newCriteria.AndOr = propertyAsList.ElementAt(i) == "NULL" ? null : propertyAsList.ElementAt(i);
                    newCriteria.FrontParenthesis = propertyAsList.ElementAt(i + 1) == "NULL" ? null : propertyAsList.ElementAt(i + 1);
                    newCriteria.Column = propertyAsList.ElementAt(i + 2) == "NULL" ? null : propertyAsList.ElementAt(i + 2);
                    newCriteria.Operator = propertyAsList.ElementAt(i + 3) == "NULL" ? null : propertyAsList.ElementAt(i + 3);
                    newCriteria.Filter = propertyAsList.ElementAt(i + 4) == "NULL" ? null : propertyAsList.ElementAt(i + 4);
                    newCriteria.EndParenthesis = propertyAsList.ElementAt(i + 5) == "NULL" ? null : propertyAsList.ElementAt(i + 5);

                    criteriaList.Add(newCriteria);

                    newCriteria = new Criteria();
                }

                foreach (FieldInfo field in typeof(WorkbookPropertiesConfig).GetFields(bindingFlags))
                {
                    if (field.Name == propertyName)
                    {
                        field.SetValue(this, criteriaList);
                        break;
                    }
                }
            }
            else
            {
                AddWorkbookProperty(propertyName);
            }
        }

        private void LoadDictProperties(string propertyName)
        {
            if (WorkbookPropertyExists(propertyName))
            {
                string propertyAsString = propertiesSheet.Range[propertyName].Value.ToString();
                List<string> propertyAsList = propertyAsString.Split('|').ToList();
                Dictionary<string, string> propertyAsDict = new Dictionary<string, string>();

                for (int i = 0; i < propertyAsList.Count; i += 2)
                {
                    try
                    {
                        propertyAsDict.Add(propertyAsList[i], propertyAsList[i + 1]);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw;
                    }
                }

                foreach (FieldInfo field in typeof(WorkbookPropertiesConfig).GetFields(bindingFlags))
                {
                    if (field.Name == propertyName)
                    {
                        field.SetValue(this, propertyAsDict);
                        break;
                    }
                }
            }
            else
            {
                AddWorkbookProperty(propertyName);
            }
        }

        private bool WorkbookPropertyExists(string propertyName)
        {
            try
            {
                string testVariable = propertiesSheet.Range[propertyName].Value.ToString();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void AddPropertiesWorksheet()
        {
            activeWorkbook.Worksheets.Add();
            Excel.Worksheet propertiesSheet = activeWorkbook.ActiveSheet;
            propertiesSheet.Name = "Properties";
        }

        private void AddWorkbookProperty(string propertyName)
        {
            // Get range addresses for each of the keys in properties field
            foreach (var property in properties.Keys)
            {
                try
                {
                    string address = activeWorkbook.Names.Item(property).RefersToRange.Address[false, false]; // Don't want absolute reference, so RowAbsolute and ColumnAbsolute parameters are false  
                    propertyAddresses.Add(property, address);
                }
                catch (COMException)
                {
                    continue; // property's named range does not exist
                }
            }

            var i = 1;
            while (true)
            {
                // If range address is not currently used by a property, then add new named range with address of 'A' + i
                if (!propertyAddresses.Values.Contains("A" + i))
                {
                    activeWorkbook.Names.Add(propertyName, "Properties!A" + i);
                    propertyAddresses.Add(propertyName, "A" + i);
                    break;
                }
                else
                {
                    // Else increment by 1 and try again
                    i++;
                }
            }
        }

        private Excel.Worksheet GetPropertiesSheet()
        {
            return (Excel.Worksheet)activeWorkbook.Worksheets["properties"];
        }
    }
}
