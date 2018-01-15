using Microsoft.VisualStudio.TestTools.UnitTesting;
using NovenaLibrary.SqlGenerators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.SqlGenerators.Tests
{
    [TestClass]
    public class OracleSqlGeneratorTests
    {
        private OracleSqlGenerator generator = new OracleSqlGenerator();
        private static string table;
        private static List<string> columns;
        private static string limit;
        private static bool groupBy;
        private static bool orderBy;
        private static bool ascending;
        private static string offset;
        private static Query query;

        [TestInitialize]
        public void run_before_each_test()
        {
            table = "table1";
            columns = new List<string>() { "column1", "column2" };
            limit = "100";
            groupBy = false;
            orderBy = false;
            ascending = false;
            offset = "0";
            query = new Query("main").SetTableSchema(MultiColumnDataTableBuilder())
                                     .SetColumns(columns)
                                     .SetTable(table)
                                     .SetLimit(limit)
                                     .SetGroupBy(groupBy)
                                     .SetOrderBy(orderBy)
                                     .SetAscending(ascending)
                                     .SetOffset(offset);
    }


        [TestMethod]
        public void CreateSql_ExistingWhereClause()
        {
            var criteria1 = new Criteria("And", null, "column1", "=", "filter1", null);
            var criteria2 = new Criteria("And", null, "column2", "In", "1,2,3", null);
            query.SetCriteria(new List<Criteria>() { criteria1, criteria2 });
            var expectedSql = $"SELECT \"{columns[0]}\", \"{columns[1]}\" FROM \"{table}\" " +
                              $"WHERE \"column1\" = 'filter1' " +
                              $"AND \"column2\" In ('1','2','3') " +
                              $"AND column1 IS NOT NULL " +
                              $"AND column2 IS NOT NULL " +
                              $"AND ROWNUM < {limit} " +
                              $"OFFSET {offset} ROWS";

            var sql = generator.CreateSql(query);

            Assert.IsTrue(sql.Count() == expectedSql.Count());
        }

        [TestMethod]
        public void CreateSql_NoExistingWhereClause()
        {
            var expectedSql = $"SELECT \"{columns[0]}\", \"{columns[1]}\" FROM \"{table}\" " +
                              $"WHERE column1 IS NOT NULL " +
                              $"AND column2 IS NOT NULL " +
                              $"AND ROWNUM < {limit} " +
                              $"OFFSET {offset} ROWS";

            var sql = generator.CreateSql(query);

            Assert.IsTrue(sql.Count() == expectedSql.Count());
        }

        private static DataTable MultiColumnDataTableBuilder()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("COLUMN_NAME");
            dt.Columns.Add("col2");
            dt.Columns.Add("col3");
            dt.Columns.Add("col4");
            dt.Columns.Add("col5");
            dt.Columns.Add("col6");
            dt.Columns.Add("col7");
            dt.Columns.Add("col8");
            dt.Columns.Add("col9");
            dt.Columns.Add("col10");
            dt.Columns.Add("co11");
            dt.Columns.Add("DATA_TYPE");
            dt.Rows.Add(new object[] { "column1", null, null, null, null, null, null, null, null, null, null, "text" });
            dt.Rows.Add(new object[] { "column2", null, null, null, null, null, null, null, null, null, null, "text" });
            dt.Rows.Add(new object[] { "column3", null, null, null, null, null, null, null, null, null, null, "int2" });

            return dt;
        }
    }
}