using NovenaLibrary.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Exceptions;

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

        //public override string CreateSql(DataTable tableSchema, bool distinct = false, List<string> columns = null, string table = null, List<Criteria> criteria = null,
        //    bool groupBy = false, bool orderBy = false, string limit = null, string offset = null, bool asc = false)
        //{
        //    base.tableSchema = tableSchema;

        //    StringBuilder sql = new StringBuilder("");
        //    sql.Append(CreateSELECTClause(distinct, columns));
        //    sql.Append(CreateFROMClause(table));
        //    sql.Append(CreateWHEREClause(criteria));
        //    sql.Append(CreateGROUPBYCluase(groupBy, columns));
        //    sql.Append(CreateORDERBYCluase(orderBy, columns, asc));
        //    sql.Append(CreateLimitClause(limit));
        //    sql.Append(CreateOffsetClause(offset));
        //    return sql.ToString().Replace("  ", " ");
        //}

        public override string CreateSql(Query query)
        {
            base.tableSchema = query.TableSchema;

            try
            {
                StringBuilder sql = new StringBuilder("");
                sql.Append(CreateSELECTClause(query.Distinct, query.Columns));
                sql.Append(CreateFROMClause(query.Table));
                sql.Append(CreateWHEREClause(query.Criteria, query.Columns));
                sql.Append(CreateGROUPBYCluase(query.GroupBy, query.Columns));
                sql.Append(CreateORDERBYCluase(query.OrderBy, query.Columns, query.Ascending));
                sql.Append(CreateLimitClause(query.Limit));
                sql.Append(CreateOffsetClause(query.Offset));
                return sql.ToString().Replace("  ", " ");
            }
            catch (BadSQLException)
            {
                throw;
            }
        }
    }
}
