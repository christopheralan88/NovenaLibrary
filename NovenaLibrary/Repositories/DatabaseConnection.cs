using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using NovenaLibrary.Config;

namespace NovenaLibrary.Repositories
{
    public class DatabaseConnection
    {
        private DbConnection dbConnection;
        private IDbCommand dbCommand;
        private AppConfig appConfig;


        public DatabaseConnection(DbConnection dbConnection, IDbCommand dbCommand, AppConfig appConfig)
        {
            this.dbConnection = dbConnection;
            this.dbCommand = dbCommand;
            this.appConfig = appConfig;
        }

        public DataTable query(string sql)
        {
            try
            {
                dbConnection.ConnectionString = appConfig.ConnectionString;
                dbConnection.Open();

                dbCommand.CommandText = sql;
                dbCommand.Connection = dbConnection;

                var dataReader = dbCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dataReader);

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public void userSignIn()
        {
            try
            {
                dbConnection.ConnectionString = appConfig.ConnectionString;
                dbConnection.Open();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public DataTable getSchema(string tableName)
        {
            try
            {
                dbConnection.ConnectionString = appConfig.ConnectionString;
                dbConnection.Open();

                return dbConnection.GetSchema("Columns", new string[4] { null, null, tableName, null });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }

    }

}
