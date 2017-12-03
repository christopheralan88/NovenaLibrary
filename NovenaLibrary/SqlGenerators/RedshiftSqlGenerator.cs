using NovenaLibrary.Config;
using System;
using System.Collections.Generic;
using System.Data;
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

        public override string CreateSql(DataTable tableSchema, bool distinct = false, List<string> columns = null, string table = null, List<Criteria> criteria = null,
            bool groupBy = false, bool orderBy = false, string limit = null, string offset = null, bool asc = false)
        {
            base.tableSchema = tableSchema;

            StringBuilder sql = new StringBuilder("");
            sql.Append(CreateSELECTClause(distinct, columns));
            sql.Append(CreateFROMClause(table));
            sql.Append(CreateWHEREClause(criteria));
            sql.Append(CreateGROUPBYCluase(groupBy, columns));
            sql.Append(CreateORDERBYCluase(orderBy, columns, asc));
            sql.Append(CreateLimitClause(limit));
            sql.Append(CreateOffsetClause(offset));
            return sql.ToString().Replace("  ", " ");
        }
    }
}
