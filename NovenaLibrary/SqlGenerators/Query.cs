﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NovenaLibrary.SqlGenerators
{
    public class Query
    {
        private string _queryName;
        private string _sql;
        public DataTable TableSchema { get; private set; }
        public bool Distinct { get; private set; }
        public List<string> Columns { get; private set; }
        public string Table { get; private set; }
        public List<Criteria> Criteria { get; private set; }
        public bool GroupBy { get; private set; }
        public bool OrderBy { get; private set; }
        public string Limit { get; private set; }
        public bool Ascending { get; private set; }
        public string Offset { get; private set; }
        public bool SuppressNulls { get; private set; }

        public Query(string queryName, string sql = null)
        {
            _queryName = queryName;
            _sql = sql;
            Columns = new List<string>();
            Criteria = new List<Criteria>();
            SuppressNulls = true;
        }

        public string QueryName
        {
            get { return _queryName; }
        }

        public string SQL
        {
            get { return _sql; }
        }

        public Query SetTableSchema(DataTable tableSchema)
        {
            TableSchema = tableSchema;
            return this;
        }

        public Query SetDistinct(bool distinct)
        {
            Distinct = distinct;
            return this;
        }

        public Query SetColumns(List<string> columns)
        {
            Columns = columns;
            return this;
        }

        public Query SetTable(string table)
        {
            Table = table;
            return this;
        }

        public Query SetCriteria(List<Criteria> criteria)
        {
            Criteria = criteria;
            return this;
        }

        public Query SetGroupBy(bool groupBy)
        {
            GroupBy = groupBy;
            return this;
        }

        public Query SetOrderBy(bool orderBy)
        {
            OrderBy = orderBy;
            return this;
        }

        /// <summary>
        /// Sets the query's limit property
        /// </summary>
        /// <param name="limit"></param>
        /// <returns>Query</returns>
        /// <exception cref="member">description</exception> 
        public Query SetLimit(string limit)
        {
            // make sure
            int parsedLimit;
            bool isInteger = int.TryParse(limit, out parsedLimit);

            if (! isInteger)
            {
                Limit = limit;
            }
            else
            {
                if (parsedLimit > 1000000)
                {
                    Limit = "1000000"; // 1 million.
                }
                else
                {
                    Limit = limit;
                }
            }
            
            return this;
        }

        public Query SetAscending(bool ascending)
        {
            Ascending = ascending;
            return this;
        }

        public Query SetOffset(string offset)
        {
            Offset = offset;
            return this;
        }

        public Query SetSuppressNulls(bool suppress)
        {
            SuppressNulls = suppress;
            return this;
        }

        public bool AddSingleCriteria(Criteria criteria)
        {
            try
            {
                Criteria.Add(criteria);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddMultipleCriteria(IEnumerable<Criteria> criteria)
        {
            try
            {
                Criteria.AddRange(criteria);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveSingleCriteria(Criteria criteria)
        {
            try
            {
                Criteria.Remove(criteria);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveSingleCriteria(int index)
        {
            try
            {
                Criteria.RemoveAt(index);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ReplaceAllMatchingCriteria(Criteria criteria)
        {
            try
            {
                RemoveAllCriteriaWithMatchingColumn(criteria.Column);
                AddSingleCriteria(criteria);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveAllCriteriaWithMatchingColumn(string column)
        {
            try
            {
                Criteria.RemoveAll(x => x.Column == column);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IList<Criteria> FindAllCriteriaByColumn(string column)
        {
            return Criteria.FindAll(x => x.Column == column);
        }

        public bool CriteriaExistsForColumn(string column)
        {
            foreach (var criteria in Criteria)
            {
                if (criteria.Column == column) return true;
            }

            return false;
        }

        //private bool LimitIsGreaterThan1Million(string limit)
        //{
        //    // parse to int
        //    int limitAsInt = 0;
        //    try
        //    {
        //        limitAsInt = int.Parse(limit);
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        limitAsInt = 0;
        //    }

        //    // if parse is greater than 1 million return true
        //    if (limitAsInt > 1000000)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}