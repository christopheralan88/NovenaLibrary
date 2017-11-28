using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Repositories;
using Npgsql;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.Odbc;
using System.Data.OleDb;
using NovenaLibrary.Exceptions;

namespace NovenaLibrary.Config
{
    public class DatabaseConnectionFactory
    {
        public DatabaseConnectionFactory() { }

        public DatabaseConnection createDbConnection(AppConfig appConfig)
        {
            var databaseType = appConfig.DatabaseType;

            try
            {
                if (databaseType.Equals(DatabaseType.PostgreSQL))
                {
                    return new DatabaseConnection(new NpgsqlConnection(), new NpgsqlCommand(), appConfig);
                }
                else if (databaseType.Equals(DatabaseType.MySql))
                {
                    return new DatabaseConnection(new MySqlConnection(), new MySqlCommand(), appConfig);
                }
                else if (databaseType.Equals(DatabaseType.Oracle))
                {
                    return new DatabaseConnection(new OracleConnection(), new OracleCommand(), appConfig);
                }
                else if (databaseType.Equals(DatabaseType.MsSqlServer))
                {
                    return new DatabaseConnection(new SqlConnection(), new SqlCommand(), appConfig);
                }
                else if (databaseType.Equals(DatabaseType.Sqlite))
                {
                    return new DatabaseConnection(new SQLiteConnection(), new SQLiteCommand(), appConfig);
                }
                else if (databaseType.Equals(DatabaseType.Redshift))
                {
                    return new DatabaseConnection(new OdbcConnection(), new OdbcCommand(), appConfig);
                }
                else if (databaseType.Equals(DatabaseType.MsAccess))
                {
                    return new DatabaseConnection(new OleDbConnection(), new OleDbCommand(), appConfig);
                }
                else
                {
                    //thrown if database type setting is not null, but is not an integer that has a corresponding DatabaseType enum value.
                    throw new DatabaseTypeNotRecognizedException("Database type is not recognized");
                }
            }
            catch (Exception)
            {
                //thrown if database type setting is null or not able to be found.
                throw new DatabaseTypeNotRecognizedException("Database type is not recognized");
            }
        }
    }
}
