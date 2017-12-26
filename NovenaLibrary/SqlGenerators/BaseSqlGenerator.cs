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

        public abstract string CreateSql(Query query);

        //public abstract string CreateSql(DataTable tableSchema, bool distinct = false, List<string> columns = null, string table = null, List<Criteria> criteria = null, 
        //    bool groupBy = false, bool orderBy = false, string limit = null, string offset = null, bool asc = false);

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

        protected virtual StringBuilder CreateWHEREClause(List<Criteria> criteria, List<string> columns)
        {
            if (criteria == null) return null;

            criteria = (List<Criteria>)AddParenthesisToCriteria(criteria);

            if (criteria.Count > 0)
            {
                //set first cell to blank since we don't need an "And" or "Or" there.
                criteria.First().AndOr = "";

                StringBuilder sql = new StringBuilder(" WHERE ");

                //for each row
                foreach (var theCriteria in criteria)
                {
                    if (!CriteriaHasNullValues(theCriteria))
                    {
                        sql.Append(theCriteria.AndOr == null ? null : $" {theCriteria.AndOr} ");
                        sql.Append(theCriteria.FrontParenthesis == null ? null : $" {theCriteria.FrontParenthesis} ");
                        sql.Append(theCriteria.Column == null ? null : $" {openingColumnMark}{theCriteria.Column}{closingColumnMark} ");
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
                    else
                    {
                        throw new BadSQLException($"The criteria at index {criteria.IndexOf(theCriteria)} in the Criteria list has a " +
                                                   "null value for it's Column, Operator, or Filter.  Please make sure each Criteria has " +
                                                   "has a non-null value for each of these properties");
                    }
                }

                // Add IS NOT NULL statements for each column to suppress rows that return nulls for all fields.
                foreach (var column in columns)
                {
                    // If there are criteria, start with "AND" because there will already be WHERE clauses.
                    if (criteria.Count > 0)
                    {
                        sql.Append($" AND {column} IS NOT NULL ");
                    }
                    else
                    {
                        sql.Append($" {column} IS NOT NULL ");
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
                return typeMappings[columnDataType.ToLower()];
            }
            catch (Exception)
            {
                // If column does not exist in typeMappings list.
                // Best guess if can't find column type - have 50/50 chance and worst that happens is SQL query fails.
                // It's more conservative to use quotes, so that SQL injection has less chance of happening because criteria
                // is wrapped in single quotes.
                return true;
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

        private IList<Criteria> AddParenthesisToCriteria(IList<Criteria> criteria)
        {
            // If there is only one or zero items in criteria list, then just return criteria unaltered.
            if (criteria.Count <= 1)
            {
                return criteria;
            }

            for (var i = 0; i < criteria.Count; i++)
            {
                var currentColumn = criteria[i].Column;
                string priorColumn;
                string nextColumn;

                // If first criteria in list
                if (i == 0)
                {
                    nextColumn = criteria[i + 1].Column;
                    if (currentColumn == nextColumn) { criteria[i].FrontParenthesis = "("; }
                }
                // If last criteria in list
                else if (i == criteria.Count - 1)
                {
                    priorColumn = criteria[i - 1].Column;
                    if (currentColumn == priorColumn) { criteria[i].EndParenthesis = ")"; }
                }
                // If criteria is neither the first or last
                else
                {
                    priorColumn = criteria[i - 1].Column;
                    nextColumn = criteria[i + 1].Column;
                    if (currentColumn != priorColumn && currentColumn == nextColumn)
                    {
                        criteria[i].FrontParenthesis = "(";
                    }
                    else if (currentColumn == priorColumn && currentColumn != nextColumn)
                    {
                        criteria[i].EndParenthesis = ")";
                    }
                }
            }

            return criteria;
        }

        private bool CriteriaHasNullValues(Criteria criteria)
        {
            // Test each criteria's Column, Operator, and Filter properties.  If any criteria in list is null, then return true.
            if (criteria.Column == null) return true;
            if (criteria.Operator == null) return true;
            if (criteria.Filter == null) return true;
           
            return false;
        }
    }
}
