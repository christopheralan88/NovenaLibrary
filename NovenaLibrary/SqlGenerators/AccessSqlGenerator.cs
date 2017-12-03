using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Config;
using System.Data;

namespace NovenaLibrary.SqlGenerators
{
    public class AccessSqlGenerator : BaseSqlGenerator
    {
        public AccessSqlGenerator()
        {
            base.openingColumnMark = '[';
            base.closingColumnMark = ']';

            //Website for type mappings:
            //https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ole-db-data-type-mappings
            Dictionary<string, bool> mappings = new Dictionary<string, bool>();
            mappings.Add("adBoolean", false);
            mappings.Add("adBigInt", false);
            mappings.Add("adCurrency", false);
            mappings.Add("adDecimal", false);
            mappings.Add("adDouble", false);
            mappings.Add("adInteger", false);
            mappings.Add("adNumeric", false);
            mappings.Add("adSingle", false);
            mappings.Add("adSmallInt", false);
            mappings.Add("adTinyInt", false);
            mappings.Add("adUnsignedBigInt", false);
            mappings.Add("adUnsignedInt", false);
            mappings.Add("adUnsignedSmallInt", false);
            mappings.Add("adBSTR", true);
            mappings.Add("adChar", true);
            mappings.Add("adWChar", true);
            mappings.Add("adDate", true);
            mappings.Add("adDBDate", true);
            mappings.Add("adDBTime", true);
            mappings.Add("adDBTimeStamp", true);
            mappings.Add("adFileTime", true);

            base.typeMappings = mappings;
        }

        public override string CreateSql(DataTable tableSchema, bool distinct = false, List<string> columns = null, string table = null, List<Criteria> criteria = null,
            bool groupBy = false, bool orderBy = false, string limit = null, string offset = null, bool asc = false)
        {
            base.tableSchema = tableSchema;

            StringBuilder sql = new StringBuilder("");
            sql.Append(CreateSELECTClause(distinct, columns, limit));
            sql.Append(CreateFROMClause(table));
            sql.Append(CreateWHEREClause(criteria));
            sql.Append(CreateGROUPBYCluase(groupBy, columns));
            sql.Append(CreateORDERBYCluase(orderBy, columns, asc));
            sql.Append(CreateOffsetClause(offset));
            return sql.ToString().Replace("  ", " ");
        }

        private StringBuilder CreateSELECTClause(bool distinct, List<string> columns, string limit)
        {
            if (columns == null) return null;

            string startSql = (distinct == true) ? "SELECT DISTINCT " : "SELECT ";
            StringBuilder sql = new StringBuilder(startSql);

            if (limit != null)
            {
                sql.Append(" TOP " + limit);
            }

            foreach (string column in columns)
            {
                sql.Append(string.Format("{0}{1}{2}, ", openingColumnMark, column, closingColumnMark));
            }
            sql = sql.Remove(sql.Length - 2, 2).Append(" ");
            return sql.Replace("  ", " ");
        }
    }
}
