﻿using NovenaLibrary.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.SqlGenerators
{
    public class SqlServerSqlGenerator : BaseSqlGenerator
    {
        public SqlServerSqlGenerator()
        {
            base.openingColumnMark = '[';
            base.closingColumnMark = ']';

            //Website for type mappings:
            //https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings
            Dictionary<string, bool> mappings = new Dictionary<string, bool>();
            mappings.Add("bit", false);
            mappings.Add("bigint", false);
            mappings.Add("decimal", false);
            mappings.Add("float", false);
            mappings.Add("int", false);
            mappings.Add("money", false);
            mappings.Add("real", false);
            mappings.Add("smallint", false);
            mappings.Add("smallmoney", false);
            mappings.Add("numeric", false);
            mappings.Add("char", true);
            mappings.Add("text", true);
            mappings.Add("varchar", true);
            mappings.Add("ntext", true);
            mappings.Add("nchar", true);
            mappings.Add("nvarchar", true);
            mappings.Add("VARCHAR2", true);
            mappings.Add("date", true);
            mappings.Add("datetime", true);
            mappings.Add("datetime2", true);
            mappings.Add("datetimeoffset", true);
            mappings.Add("smalldatetime", true);
            mappings.Add("time", true);
            mappings.Add("timestamp", true);

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
