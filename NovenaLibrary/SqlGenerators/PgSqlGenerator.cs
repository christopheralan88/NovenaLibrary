using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.SqlGenerators
{
    public class PgSqlGenerator : BaseSqlGenerator
    {
        public PgSqlGenerator()
        {
            base.openingColumnMark = '\"';
            base.closingColumnMark = '\"';

            //Website for type mappings:
            //http://www.npgsql.org/doc/types/basic.html
            Dictionary<string, bool> mappings = new Dictionary<string, bool>();
            mappings.Add("bool", false);
            mappings.Add("int2", false);
            mappings.Add("int4", false);
            mappings.Add("int8", false);
            mappings.Add("float4", false);
            mappings.Add("float8", false);
            mappings.Add("numeric", false);
            mappings.Add("money", false);
            mappings.Add("text", true);
            mappings.Add("varchar", true);
            mappings.Add("bpchar", true);
            mappings.Add("citext", true);
            mappings.Add("json", true);
            mappings.Add("jsonb", true);
            mappings.Add("date", true);
            mappings.Add("interval", true);
            mappings.Add("timestamptz", true);
            mappings.Add("time", true);
            mappings.Add("timetz", true);

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
