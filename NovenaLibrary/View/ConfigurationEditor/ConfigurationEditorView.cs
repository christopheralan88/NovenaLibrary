using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NovenaLibrary.Presenter.ConfigurationEditor;
using QueryBuilder.Config;

namespace NovenaLibrary.View.ConfigurationEditor
{
    public partial class ConfigurationEditorView : Form, IConfigurationEditorView
    {
        private IList<string> _dbConnections;
        private string _defaultConnectionNickname;
        private DatabaseType _defaultConnectionDatabaseType;
        private string _defaultConnectionString;

        public ConfigurationEditorView(IList<string> dbConnections)
        {
            InitializeComponent();
            _dbConnections = dbConnections;
            var presenter = new ConfigurationEditorPresenter(this);
            presenter.Initialize();
        }

        public string AddConnectionString
        {
            get { return txtbox_conn_string_add.Text; }
            set { txtbox_conn_string_add.Text = value; }
        }

        public string AddDatabaseType
        {
            get { return cbox_db_type_add.Text; }
            set { cbox_db_type_add.Text = value; }
        }

        public string AddNickname
        {
            get { return txtbox_conn_nickname.Text; }
            set { txtbox_conn_nickname.Text = value; }
        }

        public Image AddPictureBox
        {
            get { return picBox_add.Image; }
            set
            {
                picBox_add.Image = value;
                picBox_add.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        public BindingList<string> AvailableConnectionsNicknames
        {
            get { return (BindingList<string>)lbox_available_connections.DataSource; }
            set { lbox_available_connections.DataSource = value; }
        }

        public IList<string> DatabaseConnections
        {
            get { return _dbConnections; }
            set { _dbConnections = value; }
        }

        public string EditConnectionString
        {
            get { return txtbox_conn_string_edit.Text; }
            set { txtbox_conn_string_edit.Text = value; }
        }

        public string EditDatabaseType
        {
            get { return cbox_db_type_edit.Text; }
            set { cbox_db_type_edit.Text = value; }
        }

        public Image EditPictureBox
        {
            get { return picBox_edit.Image; }
            set
            {
                picBox_edit.Image = value;
                picBox_edit.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        public string HighlightedConnectionNickname
        {
            get { return lbox_available_connections.SelectedItem.ToString(); }
        }

        public string DefaultConnectionNickname
        {
            get { return _defaultConnectionNickname; }
            set { _defaultConnectionNickname = value; }
        }

        public DatabaseType DefaultConnectionDatabaseType
        {
            get { return _defaultConnectionDatabaseType; }
            set { _defaultConnectionDatabaseType = value; }
        }

        public string DefaultConnectionString
        {
            get { return _defaultConnectionString; }
            set { _defaultConnectionString = value; }
        }

        public void Attach(IConfigurationEditorPresenterCallbacks presenter)
        {
            this.Load += (sender, e) => presenter.OnLoad();
            cbox_db_type_add.SelectedIndexChanged += (sender, e) => presenter.OnAddDatabaseTypeSelectedIndexChange();
            but_save_add.Click += (sender, e) => presenter.OnAddSave();
            but_cancel_add.Click += (sender, e) => presenter.OnAddCancel();
            tab_edit_connection.Enter += (sender, e) => presenter.OnEditConnectionsTab();
            cbox_db_type_edit.SelectedIndexChanged += (sender, e) => presenter.OnEditDatabaseTypeChange();
            but_save_edit.Click += (sender, e) => presenter.OnEditSave();
            but_delete.Click += (sender, e) => presenter.OnEditDelete();
            but_cancel_edit.Click += (sender, e) => presenter.OnEditCancel();
            but_load_connection.Click += (sender, e) => presenter.OnEditLoadDatabaseConnection();
            lbox_available_connections.SelectedIndexChanged += (sender, e) => presenter.OnAvailableConnectionSelectedIndexChanged();
            but_addTestConnection.Click += (sender, e) => presenter.OnAddTestConnectionClick();
            but_editTestConnection.Click += (sender, e) => presenter.OnEditTestConnectionClick();
        }

        public void CloseForm()
        {
            Close();
        }

        public void CloseFormWithOKDialogResult()
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
