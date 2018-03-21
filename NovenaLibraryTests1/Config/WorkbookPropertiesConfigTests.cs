using Microsoft.VisualStudio.TestTools.UnitTesting;
using NovenaLibrary.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QueryBuilder.Config;
using QueryBuilder.SqlGenerators;

namespace NovenaLibrary.Config.Tests {
    [TestClass]
    public class WorkbookPropertiesConfigTests {
        [TestMethod]
        public void test() {
            var bookConfig = new WorkbookPropertiesConfig();
            bookConfig.Limit = 100;
            bookConfig.SelectedColumns = new List<string>() { "ContributionDate", "ContributionAmount" };
            bookConfig.AdditionalQueries = new List<string>() { "Cities|;select State, sum(ContributionAmount) from cash_contributions where state != '' group by State;" };
            bookConfig.DrilldownSql = "OrgID,ContributionAmount,CountributionDate,FirstName,LastName,State,Address1,City,Zip";
            bookConfig.RefreshColumnHeaders = true;
            bookConfig.SelectedTable = "cash_contributions";
            var criteria1 = new Criteria();
            criteria1.AndOr = Conjunction.And;
            criteria1.Column = "ContributionDate";
            criteria1.Operator = Operator.Like;
            criteria1.Filter = "(01/11/2017,03/15/2017)";
            criteria1.OrIsNull = true;
            bookConfig.Criteria.Add(criteria1);

            var xml = bookConfig.SerializeXML();
        }
    }
}