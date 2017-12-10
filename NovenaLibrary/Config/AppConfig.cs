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
        private User _user;
        private readonly string _connectionString;
        private readonly string _availableTablesSQL;
        private readonly DatabaseType _databaseType;
        private string usernamePlaceholder = "{0}";
        private string passwordPlaceholder = "{1}";
        private CredentialsRequired _credentialsRequired;

        public enum CredentialsRequired
        {
            PasswordOnly,
            UsernameOnly,
            PasswordAndUsername,
            None
        }

        public AppConfig(string connectionString, string availableTablesSQL, DatabaseType databaseType)
        {
            //_connectionString = Properties.Settings.Default.ConnectionString.ToString();
            //_availableTablesSQL = Properties.Settings.Default.AvailableTablesSQL.ToString();
            //_databaseType = (DatabaseType)Properties.Settings.Default.DatabaseType;
            _connectionString = connectionString;
            _availableTablesSQL = availableTablesSQL;
            _databaseType = databaseType;
            SetCredentialsRequired();
        }

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public string ConnectionString
        {
            get
            {
                if (_credentialsRequired.Equals(CredentialsRequired.PasswordAndUsername))
                {
                    return string.Format(_connectionString, _user.Username, _user.Password);
                }
                else if (_credentialsRequired.Equals(CredentialsRequired.UsernameOnly))
                {
                    return string.Format(_connectionString, _user.Username);
                }
                else if (_credentialsRequired.Equals(CredentialsRequired.PasswordOnly))
                {
                    return string.Format(_connectionString, _user.Password);
                }
                else
                {
                    return _connectionString;
                }
            }
        }

        public string AvailableTablesSql
        {
            get
            {
                if (_credentialsRequired.Equals(CredentialsRequired.None))
                {
                    return _availableTablesSQL;
                }
                else
                {
                    return string.Format(_availableTablesSQL, User.Username);
                }
            }
        }

        public DatabaseType DatabaseType
        {
            get { return _databaseType; }
        }

        public CredentialsRequired GetCredentialsRequired
        {
            get { return _credentialsRequired;}
        }

        private void SetCredentialsRequired()
        {
            if (_connectionString.Contains(usernamePlaceholder) && _connectionString.Contains(passwordPlaceholder))
            {
                _credentialsRequired = CredentialsRequired.PasswordAndUsername;
            }
            else if (_connectionString.Contains(usernamePlaceholder) && !_connectionString.Contains(passwordPlaceholder))
            {
                _credentialsRequired = CredentialsRequired.PasswordOnly;
            }
            else if (!_connectionString.Contains(usernamePlaceholder) && _connectionString.Contains(passwordPlaceholder))
            {
                _credentialsRequired = CredentialsRequired.UsernameOnly;
            }
            else
            {
                _credentialsRequired = CredentialsRequired.None;
            }
        }
    }
}
