using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Config;
using System.Data;

namespace NovenaLibrary.SqlGenerators
{
    public abstract class BaseSqlGenerator
    {
        protected Dictionary<string, bool> typeMappings = new Dictionary<string, bool>();
        protected char openingColumnMark;
        protected char closingColumnMark;
        protected DataTable tableSchema;


        public BaseSqlGenerator()
        {
        }

        public abstract string CreateSql(DataTable tableSchema, bool distinct = false, List<string> columns = null, string table = null, List<Criteria> criteria = null, 
            bool groupBy = false, bool orderBy = false, string limit = null, string offset = null, bool asc = false);

        protected virtual StringBuilder CreateSELECTClause(bool distinct, List<string> columns)
        {
            if (columns == null) return null;

            string startSql = (distinct == true) ? "SELECT DISTINCT " : "SELECT ";
            StringBuilder sql = new StringBuilder(startSql);
            foreach (string column in columns)
            {
                sql.Append(string.Format("{0}{1}{2}, ", openingColumnMark, column, closingColumnMark));
            }
            sql = sql.Remove(sql.Length - 2, 2).Append(" ");
            return sql.Replace("  ", " ");
        }

        protected virtual StringBuilder CreateFROMClause(string table)
        {
            if (table == null) return null;

            string s = string.Format(" FROM {0}{1}{2} ", openingColumnMark, table, closingColumnMark);
            StringBuilder sql = new StringBuilder(s);
            return sql.Replace("  ", " ");
        }

        protected virtual StringBuilder CreateWHEREClause(List<Criteria> criteria)
        {
            if (criteria == null) return null;

            if (criteria.Count > 0)
            {
                //set first cell to blank since we don't need an "And" or "Or" there.
                criteria.First().AndOr = "";

                StringBuilder sql = new StringBuilder(" WHERE ");

                //for each row
                foreach (var theCriteria in criteria)
                {
                    sql.Append(theCriteria.AndOr == null ? null : $" {theCriteria.AndOr} ");
                    sql.Append(theCriteria.FrontParenthesis == null ? null : $" {theCriteria.FrontParenthesis} ");
                    sql.Append(theCriteria.Column == null ? null : $" {theCriteria.Column} ");
                    sql.Append(theCriteria.Operator == null ? null : $" {theCriteria.Operator} ");
                    sql.Append(theCriteria.Filter == null ? null : $" {theCriteria.Filter} ");
                    sql.Append(theCriteria.EndParenthesis == null ? null : $" {theCriteria.EndParenthesis} ");
                }

                return sql.Replace("  ", " ");
            }
            return null;
        }

        protected virtual StringBuilder CreateGROUPBYCluase(bool groupBy, List<string> columns)
        {
            if (groupBy == false) return null;

            if (columns.Count > 0)
            {
                StringBuilder sql = new StringBuilder(" GROUP BY ");
                foreach (string item in columns)
                {
                    sql.Append(string.Format("{0}{1}{2}, ", openingColumnMark, item, closingColumnMark));
                }
                sql.Remove(sql.Length - 2, 2).Append(" ");
                return sql.Replace("  ", " ");
            }
            return null;
        }

        protected virtual StringBuilder CreateORDERBYCluase(bool orderBy, List<string> columns, bool asc)
        {
            if (orderBy == false) return null;

            if (columns.Count > 0)
            {
                StringBuilder sql = new StringBuilder(" ORDER BY ");
                foreach (string item in columns)
                {
                    sql.Append(string.Format("{0}{1}{2}, ", openingColumnMark, item, closingColumnMark));
                }
                sql.Remove(sql.Length - 2, 2).Append(" ");
                return (asc == true) ? sql.Append(" ASC ").Replace("  ", " ") : sql.Append(" DESC ").Replace("  ", " ");
            }
            return null;
        }

        protected virtual StringBuilder CreateLimitClause(string limit)
        {
            return (limit == null) ? null : new StringBuilder(" LIMIT " + limit).Replace("  ", " ");
        }

        protected virtual StringBuilder CreateOffsetClause(string offset)
        {
            return (offset == null) ? null : new StringBuilder(" OFFSET " + offset).Replace("  ", " ");
        }

        public bool IsColumnQuoted(string columnDataType)
        {
            try
            {
                return typeMappings[columnDataType];
            }
            catch (Exception)
            {
                //if column does not exist in typeMappings list
                //best guess if can't find column type - have 50/50 chance and worst that happens is SQL query fails
                return false;
            }
        }
    }
}
