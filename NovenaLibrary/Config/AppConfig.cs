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
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConnectionString { get; set; }
        public string AvailableTablesSql { get; set; }
        public DatabaseType DatabaseType { get; set; }

        public AppConfig()
        {
            ConnectionString = Properties.Settings.Default.ConnectionString.ToString();
            AvailableTablesSql = Properties.Settings.Default.AvailableTablesSQL.ToString();
            DatabaseType = (DatabaseType)Properties.Settings.Default.DatabaseType;
        }
    }
}
