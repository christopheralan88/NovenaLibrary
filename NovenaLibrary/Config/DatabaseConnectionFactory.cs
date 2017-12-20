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

        public DatabaseConnection CreateDbConnection(DatabaseType databaseType, string connectionString)
        {
            //var databaseType = appConfig.DatabaseType;
            //var connectionString = appConfig.ConnectionString;

            try
            {
                if (databaseType.Equals(DatabaseType.PostgreSQL))
                {
                    return new DatabaseConnection(new NpgsqlConnection(connectionString), new NpgsqlCommand());
                }
                else if (databaseType.Equals(DatabaseType.MySql))
                {
                    return new DatabaseConnection(new MySqlConnection(connectionString), new MySqlCommand());
                }
                else if (databaseType.Equals(DatabaseType.Oracle))
                {
                    return new DatabaseConnection(new OracleConnection(connectionString), new OracleCommand());
                }
                if (databaseType.Equals(DatabaseType.MsSqlServer))
                {
                    return new DatabaseConnection(new SqlConnection(connectionString), new SqlCommand());
                }
                else if (databaseType.Equals(DatabaseType.Sqlite))
                {
                    return new DatabaseConnection(new SQLiteConnection(connectionString), new SQLiteCommand());
                }
                else if (databaseType.Equals(DatabaseType.Redshift))
                {
                    return new DatabaseConnection(new OdbcConnection(connectionString), new OdbcCommand());
                }
                else if (databaseType.Equals(DatabaseType.MsAccess))
                {
                    return new DatabaseConnection(new OleDbConnection(connectionString), new OleDbCommand());
                }
                else
                {
                    //thrown if database type setting is not null, but is not an integer that has a corresponding DatabaseType enum value.
                    throw new DatabaseTypeNotRecognizedException("Database type is not recognized");
                }
            }
            catch (Exception ex)
            {
                //thrown if database type setting is null or not able to be found.
                //throw new DatabaseTypeNotRecognizedException("Database type is not recognized");
                throw;
            }
        }
    }
}
