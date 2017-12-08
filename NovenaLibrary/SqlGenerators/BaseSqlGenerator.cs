using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Config;
using System.Data;
using NovenaLibrary.Exceptions;

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
                sql.Append(string.Format("{0}{1}{2}, ", openingColumnMark, SQLCleanser.EscapeAndRemoveWords(column), closingColumnMark));
            }
            sql = sql.Remove(sql.Length - 2, 2).Append(" ");
            return sql.Replace("  ", " ");
        }

        protected virtual StringBuilder CreateFROMClause(string table)
        {
            if (table == null) return null;

            string s = string.Format(" FROM {0}{1}{2} ", openingColumnMark, SQLCleanser.EscapeAndRemoveWords(table), closingColumnMark);
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
                    sql.Append(theCriteria.EndParenthesis == null ? null : $" {theCriteria.EndParenthesis} ");

                    // determine if Criteria's filter property is a subquery
                    if (IsSubQuery(theCriteria.Filter))
                    {
                        sql.Append($" ({SQLCleanser.EscapeAndRemoveWords(theCriteria.Filter)}) ");
                    }

                    // if not subquery, then determine if column needs quotes or not
                    var columnDataType = GetColumnDataType(theCriteria.Column);
                    if (columnDataType == null)
                    {
                        // if the column name cannot be found in the table schema datatable, then throw BadSQLException
                        throw new BadSQLException(string.Format($"Could not find column name, {theCriteria.Column}, in table schema for {tableSchema.TableName}"));
                    } 
                    else
                    {
                        var shouldHaveQuotes = IsColumnQuoted(columnDataType);

                        if (theCriteria.Operator == "In" || theCriteria.Operator == "Not In")
                        {
                            var originalFilters = theCriteria.Filter.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            var newFilters = new string[originalFilters.Count()];
                            if (shouldHaveQuotes)
                            {
                                for (var i = 0; i < originalFilters.Count(); i++)
                                {
                                    newFilters[i] = $"'{SQLCleanser.EscapeAndRemoveWords(originalFilters[i])}'";
                                }
                                sql.Append($" ({string.Join(",", newFilters)}) ");
                            }
                            else
                            {
                                sql.Append($" ({SQLCleanser.EscapeAndRemoveWords(theCriteria.Filter)}) ");
                            }
                        }
                        else
                        {
                            var cleansedValue = SQLCleanser.EscapeAndRemoveWords(theCriteria.Filter);
                            sql.Append((shouldHaveQuotes) ? $" '{cleansedValue}' " : $" {cleansedValue} ");
                        }
                    }
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
                foreach (string column in columns)
                {
                    sql.Append(string.Format("{0}{1}{2}, ", openingColumnMark, SQLCleanser.EscapeAndRemoveWords(column), closingColumnMark));
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
                foreach (string column in columns)
                {
                    sql.Append(string.Format("{0}{1}{2}, ", openingColumnMark, SQLCleanser.EscapeAndRemoveWords(column), closingColumnMark));
                }
                sql.Remove(sql.Length - 2, 2).Append(" ");
                return (asc == true) ? sql.Append(" ASC ").Replace("  ", " ") : sql.Append(" DESC ").Replace("  ", " ");
            }
            return null;
        }

        protected virtual StringBuilder CreateLimitClause(string limit)
        {
            if (limit == null) return null;

            var cleansedLimit = SQLCleanser.EscapeAndRemoveWords(limit);
            return (limit == null) ? null : new StringBuilder(" LIMIT " + cleansedLimit).Replace("  ", " ");
        }

        protected virtual StringBuilder CreateOffsetClause(string offset)
        {
            if (offset == null) return null;

            var cleansedOffset = SQLCleanser.EscapeAndRemoveWords(offset);
            return (offset == null) ? null : new StringBuilder(" OFFSET " + cleansedOffset).Replace("  ", " ");
        }

        private string GetColumnDataType(string columnName)
        {
            EnumerableRowCollection<DataRow> columnInfo = tableSchema.AsEnumerable()
                                                                     .Where(row => row.Field<string>("COLUMN_NAME")
                                                                     .Equals(columnName));

            if (columnInfo.Count() == 1)
            {
                return (string)columnInfo.ElementAt(0).ItemArray[11];
            }
            else
            {
                return null;
            }
        }

        private bool IsColumnQuoted(string columnDataType)
        {
            try
            {
                return typeMappings[columnDataType];
            }
            catch (Exception)
            {
                // if column does not exist in typeMappings list
                // best guess if can't find column type - have 50/50 chance and worst that happens is SQL query fails
                return false;
            }
        }

        private bool IsSubQuery(string filter)
        {
            if (filter.Length >= 6)
            {
                return (filter.Substring(0, 6).ToLower() == "select") ? true : false;
            }
            return false;
        }
    }
}
