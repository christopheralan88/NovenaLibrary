using NovenaLibrary.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.SqlGenerators
{
    public class OracleSqlGenerator : BaseSqlGenerator
    {
        public OracleSqlGenerator()
        {
            base.openingColumnMark = '"';
            base.closingColumnMark = '"';

            //Website for type mappings:
            //https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/oracle-data-type-mappings
            Dictionary<string, bool> mappings = new Dictionary<string, bool>();
            mappings.Add("INTEGER", false);
            mappings.Add("FLOAT", false);
            mappings.Add("UNSIGNED INTEGER", false);
            mappings.Add("NUMBER", false);
            mappings.Add("CHAR", true);
            mappings.Add("LONG", false);
            mappings.Add("NCHAR", true);
            mappings.Add("NVARCHAR2", true);
            mappings.Add("VARCHAR2", true);
            mappings.Add("DATE", true);
            mappings.Add("TIMESTAMP", true);
            mappings.Add("TIMESTAMP WITH LOCAL TIME ZONE", true);
            mappings.Add("TIMESTAMP WITH TIME ZONE", true);
            mappings.Add("INTERVAL YEAR TO MONTH", true);
            mappings.Add("INTERVAL DAY TO SECOND", true);

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
