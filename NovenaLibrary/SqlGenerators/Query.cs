using System;
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

        public Query(string queryName, string sql = null)
        {
            _queryName = queryName;
            _sql = sql;
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

        public Query SetLimit(string limit)
        {
            Limit = limit;
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

        public IList<Criteria> FindAllCriteriaByColumn(string column)
        {
            return Criteria.FindAll(x => x.Column == column);
        }

        public bool CriteriaExistsForColumn(string column)
        {
            var test = Criteria.First(x => x.Column == column);

            if (test != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
