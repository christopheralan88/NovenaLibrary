using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Repositories;

namespace NovenaLibrary.Config
{
    public class AppConfig
    {
        //public string Username { get; set; }
        //public string Password { get; set; }
        private User _user;
        private readonly string _connectionString;
        private readonly string _availableTablesSQL;
        private readonly DatabaseType _databaseType;

        public AppConfig(string connectionString, string availableTablesSQL, DatabaseType databaseType)
        {
            //_connectionString = Properties.Settings.Default.ConnectionString.ToString();
            //_availableTablesSQL = Properties.Settings.Default.AvailableTablesSQL.ToString();
            //_databaseType = (DatabaseType)Properties.Settings.Default.DatabaseType;
            _connectionString = connectionString;
            _availableTablesSQL = availableTablesSQL;
            _databaseType = databaseType;
        }

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public string AvailableTablesSql
        {
            get { return _availableTablesSQL; }
        }

        public DatabaseType DatabaseType
        {
            get { return _databaseType; }
        }
    }
}
