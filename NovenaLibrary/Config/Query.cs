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
        public DataTable tableSchema { get; set; }
        public bool distinct { get; set; }
        public List<string> columns { get; set; }
        public string table { get; set; }
        public List<Criteria> criteria { get; set; }
        public bool groupBy { get; set; }
        public bool orderBy { get; set; }
        public string limit { get; set; }

        public Query(string queryName)
        {
            _queryName = queryName;
        }

        public string QueryName
        {
            get { return _queryName; }
        }
    }
}
