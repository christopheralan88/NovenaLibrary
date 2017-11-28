using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Repositories;
using NovenaLibrary.Config;

namespace NovenaLibrary.SqlGenerators
{
    public abstract class BaseSqlGenerator
    {
        public Dictionary<string, bool> typeMappings = new Dictionary<string, bool>();
        public char openingColumnMark;
        public char closingColumnMark;


        public BaseSqlGenerator()
        {
        }

        public abstract string createSql(bool distinct, string[] columns, string table, string[,] criteria, string[] groupBy, string[] orderBy, string limit, string offset, bool asc);

        protected virtual StringBuilder createSELECTClause(bool distinct, string[] columns)
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

        protected virtual StringBuilder createFROMClause(string table)
        {
            if (table == null) return null;

            string s = string.Format(" FROM {0}{1}{2} ", openingColumnMark, table, closingColumnMark);
            StringBuilder sql = new StringBuilder(s);
            return sql.Replace("  ", " ");
        }

        protected virtual StringBuilder createWHEREClause(string[,] criteria)
        {
            if (criteria == null) return null;

            if (criteria.Length > 0)
            {
                //set first cell to blank since we don't need an "And" or "Or" there.
                criteria[0, 0] = "";

                StringBuilder sql = new StringBuilder(" WHERE ");

                //for each row
                for (int r = 0; r < criteria.GetLength(0); r++)
                {
                    //for each column
                    for (int c = 0; c < criteria.GetLength(1); c++)
                    {
                        object currentItem = criteria[r, c];
                        if (currentItem != null) sql.Append(string.Format(" {0} ", currentItem));
                    }
                }
                //replace all double spaces with a single space
                return sql.Replace("  ", " ");
            }
            return null;
        }

        protected virtual StringBuilder createGROUPBYCluase(string[] groupBy)
        {
            if (groupBy == null) return null;

            if (groupBy.Length > 0)
            {
                StringBuilder sql = new StringBuilder(" GROUP BY ");
                foreach (string item in groupBy)
                {
                    sql.Append(string.Format("{0}{1}{2}, ", openingColumnMark, item, closingColumnMark));
                }
                sql.Remove(sql.Length - 2, 2).Append(" ");
                return sql.Replace("  ", " ");
            }
            return null;
        }

        protected virtual StringBuilder createORDERBYCluase(string[] orderBy, bool asc)
        {
            if (orderBy == null) return null;

            if (orderBy.Length > 0)
            {
                StringBuilder sql = new StringBuilder(" ORDER BY ");
                foreach (string item in orderBy)
                {
                    sql.Append(string.Format("{0}{1}{2}, ", openingColumnMark, item, closingColumnMark));
                }
                sql.Remove(sql.Length - 2, 2).Append(" ");
                return (asc == true) ? sql.Append(" ASC ").Replace("  ", " ") : sql.Append(" DESC ").Replace("  ", " ");
                //return sql.Replace("  ", " ");
            }
            return null;
        }

        protected virtual StringBuilder createLimitClause(string limit)
        {
            return (limit == null) ? null : new StringBuilder(" LIMIT " + limit).Replace("  ", " ");
        }

        protected virtual StringBuilder createOffsetClause(string offset)
        {
            return (offset == null) ? null : new StringBuilder(" OFFSET " + offset).Replace("  ", " ");
        }

        public bool isQuotedTest(string columnDataType)
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
