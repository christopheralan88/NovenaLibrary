using Microsoft.VisualStudio.TestTools.UnitTesting;
using NovenaLibrary.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Rhino.Mocks;
using NovenaLibrary.SqlGenerators;

namespace NovenaLibrary.Config.Tests
{
    [TestClass]
    public class WorkbookPropertiesConfigTests
    {
        private WorkbookPropertiesConfig starterBookConfig = new WorkbookPropertiesConfig();

        [TestMethod]
        public void DeserializeXML()
        {
            //var xml = "<?xml version=\"1.0\" encoding=\"utf - 16\"?><xsd:WorkbookProperties xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><xsd:Limit>100</xsd:Limit><xsd:SelectedTable>table1</xsd:SelectedTable><xsd:AdditionalQueries><xsd:string>query1|SELECT * FROM table1;</xsd:string></xsd:AdditionalQueries><xsd:DrilldownSql>column1,column2</xsd:DrilldownSql><xsd:RefreshColumnHeaders>true</xsd:RefreshColumnHeaders><xsd:SelectedColumns><xsd:string>column1</xsd:string><xsd:string>column2</xsd:string></xsd:SelectedColumns></xsd:WorkbookProperties>";
            var xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><xsd:WorkbookProperties xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><xsd:Limit>100</xsd:Limit><xsd:SelectedTable>cash_contributions</xsd:SelectedTable><xsd:Criteria><xsd:Criteria><xsd:AndOr>And</xsd:AndOr><xsd:FrontParenthesis /><xsd:Column>ContributionDate</xsd:Column><xsd:Operator>LIKE</xsd:Operator><xsd:Filter>(01/11/2017,03/15/2017)</xsd:Filter><xsd:EndParenthesis /></xsd:Criteria></xsd:Criteria><xsd:AdditionalQueries><xsd:string>Cities|;select State, sum(ContributionAmount) from cash_contributions where state != '' group by State;</xsd:string></xsd:AdditionalQueries><xsd:DrilldownSql>OrgID,ContributionAmount,CountributionDate,FirstName,LastName,State,Address1,City,Zip</xsd:DrilldownSql><xsd:RefreshColumnHeaders>true</xsd:RefreshColumnHeaders><xsd:SelectedColumns><xsd:string>ContributionDate</xsd:string><xsd:string>ContributionAmount</xsd:string></xsd:SelectedColumns></xsd:WorkbookProperties>";

            var newBookConfig = starterBookConfig.DeserializeXML(xml);

            Assert.IsTrue(newBookConfig.Limit == "100");
            Assert.IsTrue(newBookConfig.SelectedColumns.Count == 2);
            Assert.IsTrue(newBookConfig.AdditionalQueries.Count == 1);
            Assert.IsTrue(newBookConfig.DrilldownSql != null);
            Assert.IsTrue(newBookConfig.RefreshColumnHeaders == true);
            Assert.IsTrue(newBookConfig.SelectedTable == "cash_contributions");
            Assert.IsTrue(newBookConfig.Criteria.Count == 1);
        }

        [TestMethod]
        public void SerializeXML()
        {
            var bookConfig = new WorkbookPropertiesConfig();
            bookConfig.Limit = "100";
            bookConfig.SelectedColumns = new List<string>() { "ContributionDate", "ContributionAmount" };
            bookConfig.AdditionalQueries = new List<string>() { "Cities|;select State, sum(ContributionAmount) from cash_contributions where state != '' group by State;" };
            bookConfig.DrilldownSql = "OrgID,ContributionAmount,CountributionDate,FirstName,LastName,State,Address1,City,Zip";
            bookConfig.RefreshColumnHeaders = true;
            bookConfig.SelectedTable = "cash_contributions";
            var criteria1 = new Criteria("And", "", "ContributionDate", "LIKE", "(01/11/2017,03/15/2017)");
            bookConfig.Criteria.Add(criteria1);

            var xml = bookConfig.SerializeXML();

            System.Diagnostics.Debug.Print(xml);
            Assert.IsTrue(xml != null);
        }

        [TestMethod]
        public void AdditionalQueriesAsDictionary()
        {
            var list = new List<string>() { "query1|select * from table1;", "query2|select * from table2;" };
            var bookConfig = new WorkbookPropertiesConfig();
            bookConfig.AdditionalQueries = list;

            var dict = bookConfig.AdditionalQueriesAsDictionary();

            Assert.IsTrue(dict.Count == 2);
            Assert.IsTrue(dict["query1"] == "select * from table1;");
            Assert.IsTrue(dict["query2"] == "select * from table2;");
        }
    }
}