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
    public class OracleSqlGenerator : BaseSqlGenerator
    {
        public OracleSqlGenerator()
        {
            base.openingColumnMark = '"';
            base.closingColumnMark = '"';

            //Website for type mappings:
            //https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/oracle-data-type-mappings
            Dictionary<string, bool> mappings = new Dictionary<string, bool>();
            mappings.Add("integer", false);
            mappings.Add("float", false);
            mappings.Add("unsigned integer", false);
            mappings.Add("number", false);
            mappings.Add("char", true);
            mappings.Add("long", false);
            mappings.Add("nchar", true);
            mappings.Add("nvarchar2", true);
            mappings.Add("varchar2", true);
            mappings.Add("date", true);
            mappings.Add("timestamp", true);
            mappings.Add("timestamp with local time zone", true);
            mappings.Add("timestamp with time zone", true);
            mappings.Add("interval year to month", true);
            mappings.Add("interval day to second", true);

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
