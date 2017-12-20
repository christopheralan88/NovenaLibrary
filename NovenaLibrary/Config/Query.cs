using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NovenaLibrary.Config
{
    public class Query
    {
        private string _queryName;
        private string _sql;
        public DataTable tableSchema { get; set; }
        public bool distinct { get; set; }
        public List<string> columns { get; set; }
        public string table { get; set; }
        public List<Criteria> criteria { get; set; }
        public bool groupBy { get; set; }
        public bool orderBy { get; set; }
        public string limit { get; set; }

        public Query(string queryName, string sql)
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
    }
}
