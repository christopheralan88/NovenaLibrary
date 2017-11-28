using System;
using System.Collections.Generic;
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
