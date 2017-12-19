using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.View.ConfigurationEditor;
using NovenaLibrary.Config;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using NovenaLibrary.Utilities;

namespace NovenaLibrary.Presenter.ConfigurationEditor
{
    public class ConfigurationEditorPresenter : IConfigurationEditorPresenter, IConfigurationEditorPresenterCallbacks
    {
        private IConfigurationEditorView _view;
        private Dictionary<string, string> _dbConnectionsDict;

        public ConfigurationEditorPresenter(IConfigurationEditorView view)
        {
            _view = view;

            // create dict object
            var connectionsDict = new Dictionary<string, string>();

            // parse key and value from each list item and add to dict
            foreach (var conn in _view.DatabaseConnections)
            {
                var key = conn.Substring(0, conn.IndexOf("::"));
                var value = conn.Substring(conn.IndexOf("::") + 2);
                connectionsDict.Add(key, value);
            }

            _dbConnectionsDict = connectionsDict;
        }

        public object UI
        {
            get { return _view; }
        }

        public void Initialize()
        {
            _view.Attach(this);
        }

        public void OnAddCancel()
        {
            _view.CloseForm();
        }

        public void OnAddDatabaseTypeSelectedIndexChange()
        {
            // get database type image
            var image = GetDatabaseTypeImage(_view.AddDatabaseType);

            // set picture box on Add Connection tab to image
            _view.AddPictureBox = image;
        }

        public void OnAddSave()
        {
            var nickname = _view.AddNickname;
            var connection = _view.AddDatabaseType + "||" + _view.AddConnectionString;

            try
            {
                // If nickname is saved successfully
                _dbConnectionsDict.Add(nickname, connection);
                _view.AvailableConnectionsNicknames = Utility.ConvertDictKeysToBindingList(_dbConnectionsDict.Keys);
                MessageBox.Show("Connection saved successfully!", "Success", MessageBoxButtons.OK);
            }
            catch (ArgumentNullException)
            {
                // If nickname is blank
                MessageBox.Show("You must specify a nickname for the connection.", "No Nickname", MessageBoxButtons.OK);
            }
            catch (ArgumentException)
            {
                // If nickname is not unique
                MessageBox.Show("That nickname already exists.  The nickname must be unique.", "Dupliate Nickname", MessageBoxButtons.OK);
            }
        }

        public void OnAvailableConnectionSelectedIndexChanged()
        {
            var nickname = _view.HighlightedConnectionNickname;

            var databaseType = ParseDatabaseType(_dbConnectionsDict[nickname]);
            var connectionString = ParseConnectionString(_dbConnectionsDict[nickname]);
            var databaseTypeImage = GetDatabaseTypeImage(databaseType);

            _view.EditDatabaseType = databaseType;
            _view.EditConnectionString = connectionString;
            _view.EditPictureBox = databaseTypeImage;
        }

        public void OnEditCancel()
        {
            _view.CloseForm();
        }

        public void OnEditConnectionsTab()
        {
            // Reload database connections in case there were any additions from when form was loaded.
            _view.AvailableConnectionsNicknames = Utility.ConvertDictKeysToBindingList(_dbConnectionsDict.Keys);
        }

        public void OnEditDatabaseTypeChange()
        {
            // get database type image
            var image = GetDatabaseTypeImage(_view.AddDatabaseType);

            // set picture box on Add Connection tab to image
            _view.EditPictureBox = image;
        }

        public void OnEditDelete()
        {
            bool successfullyRemoved = _dbConnectionsDict.Remove(_view.HighlightedConnectionNickname);

            if (!successfullyRemoved)
            {
                MessageBox.Show("There was an problem preventing the connection from being removed", "Error", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Connection deleted successfully!", "Success", MessageBoxButtons.OK);
                _view.AvailableConnectionsNicknames = Utility.ConvertDictKeysToBindingList(_dbConnectionsDict.Keys);
            }
        }

        public void OnEditLoadDatabaseConnection()
        {
            // Set view's DatabaseConnections list property
            var list = Utility.ConvertListToDict(_dbConnectionsDict);
            _view.DatabaseConnections = list;

            // set view's Default Connection Nickname property
            _view.DefaultConnectionNickname = _view.HighlightedConnectionNickname;

            // set view's Default DatabaseType property
            DatabaseType databaseType;
            Enum.TryParse(_view.EditDatabaseType, true, out databaseType);
            _view.DefaultConnectionDatabaseType = databaseType;

            // set view's Default Connection String property
            _view.DefaultConnectionString = _view.EditConnectionString;

            // close form
            _view.CloseFormWithOKDialogResult();
        }

        public void OnEditSave()
        {
            var nickname = _view.HighlightedConnectionNickname;
            _dbConnectionsDict[nickname] = _view.EditDatabaseType + "||" + _view.EditConnectionString;
            MessageBox.Show("Connection updated successfully!", "Success", MessageBoxButtons.OK);
        }

        public void OnLoad()
        {
            // Set listbox of available connections to binding list of connection names (keys in dict above)
            _view.AvailableConnectionsNicknames = Utility.ConvertDictKeysToBindingList(_dbConnectionsDict.Keys);
        }

        private Image GetDatabaseTypeImage(string dbType)
        {
            try
            {
                DatabaseType dbTypeEnum;
                Enum.TryParse(dbType.ToLower().Replace(" ", ""), true, out dbTypeEnum);

                if (dbTypeEnum.Equals(DatabaseType.MsAccess)) return Properties.Resources.msaccess;
                if (dbTypeEnum.Equals(DatabaseType.MsSqlServer)) return Properties.Resources.mssqlserver;
                if (dbTypeEnum.Equals(DatabaseType.MySql)) return Properties.Resources.mysql;
                if (dbTypeEnum.Equals(DatabaseType.Oracle)) return Properties.Resources.oracle;
                if (dbTypeEnum.Equals(DatabaseType.PostgreSQL)) return Properties.Resources.postgresql;
                if (dbTypeEnum.Equals(DatabaseType.Redshift)) return Properties.Resources.redshift;
                if (dbTypeEnum.Equals(DatabaseType.Sqlite)) return Properties.Resources.sqlite;
                //else
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string ParseDatabaseType(string connectionProperties)
        {
            var endIndex = connectionProperties.IndexOf("||");
            return connectionProperties.Substring(0, endIndex);
        }

        private string ParseConnectionString(string connectionProperties)
        {
            var startIndex = connectionProperties.IndexOf("||") + 2;
            return connectionProperties.Substring(startIndex);
        }

    }
}
