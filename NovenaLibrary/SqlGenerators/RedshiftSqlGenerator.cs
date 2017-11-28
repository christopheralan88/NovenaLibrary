using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.SqlGenerators
{
    public class RedshiftSqlGenerator : BaseSqlGenerator
    {
        public RedshiftSqlGenerator()
        {
            base.openingColumnMark = '"';
            base.closingColumnMark = '"';

            //Website for type mappings (Redshift uses System.Data.ODBC):
            //https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/odbc-data-type-mappings
            Dictionary<string, bool> mappings = new Dictionary<string, bool>();
            mappings.Add("SQL_BIT", false);
            mappings.Add("SQL_BIGINT", false);
            mappings.Add("SQL_INTEGER", false);
            mappings.Add("SQL_DECIMAL", false);
            mappings.Add("SQL_DOUBLE", false);
            mappings.Add("SQL_NUMERIC", false);
            mappings.Add("SQL_REAL", false);
            mappings.Add("SQL_SMALLINT", false);
            mappings.Add("SQL_CHAR", true);
            mappings.Add("SQL_LONG_VARCHAR", true);
            mappings.Add("SQL_WCHAR", true);
            mappings.Add("SQL_WLONGVARCHAR", true);
            mappings.Add("SQL_WVARCHAR", true);
            mappings.Add("SQL_TYPE_TIMES", true);
            mappings.Add("SQL_TYPE_TIMESTAMP", true);

            base.typeMappings = mappings;
        }

        public override string createSql(bool distinct, string[] columns, string table, string[,] criteria, string[] groupBy, string[] orderBy, string limit, string offset, bool asc)
        {
            StringBuilder sql = new StringBuilder("");
            sql.Append(createSELECTClause(distinct, columns));
            sql.Append(createFROMClause(table));
            sql.Append(createWHEREClause(criteria));
            sql.Append(createGROUPBYCluase(groupBy));
            sql.Append(createORDERBYCluase(orderBy, asc));
            sql.Append(createLimitClause(limit));
            sql.Append(createOffsetClause(offset));
            return sql.ToString().Replace("  ", " ");
        }
    }
}
