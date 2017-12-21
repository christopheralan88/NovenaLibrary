using NovenaLibrary.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.SqlGenerators
{
    public class SqliteSqlGenerator : BaseSqlGenerator
    {
        public SqliteSqlGenerator()
        {
            base.openingColumnMark = '"';
            base.closingColumnMark = '"';

            //Website for type mappings:
            //https://www.devart.com/dotconnect/sqlite/docs/DataTypeMapping.html
            Dictionary<string, bool> mappings = new Dictionary<string, bool>();
            mappings.Add("boolean", false);
            mappings.Add("smallint", false);
            mappings.Add("int16", false);
            mappings.Add("int", false);
            mappings.Add("int32", false);
            mappings.Add("integer", false);
            mappings.Add("int64", false);
            mappings.Add("real", false);
            mappings.Add("numeric", false);
            mappings.Add("decimal", false);
            mappings.Add("money", false);
            mappings.Add("currency", false);
            mappings.Add("date", true);
            mappings.Add("time", true);
            mappings.Add("datetime", true);
            mappings.Add("smalldate", true);
            mappings.Add("datetimeoffset", true);
            mappings.Add("text", true);
            mappings.Add("ntext", true);
            mappings.Add("char", true);
            mappings.Add("nchar", true);
            mappings.Add("varchar", true);
            mappings.Add("nvarchar", true);
            mappings.Add("string", true);

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
    }
}
