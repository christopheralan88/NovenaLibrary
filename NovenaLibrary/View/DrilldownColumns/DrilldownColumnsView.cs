using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NovenaLibrary.Config;
using NovenaLibrary.Presenter.DrilldownColumns;
using NovenaLibrary.Utilities;

namespace NovenaLibrary.View.DrilldownColumns
{
    public partial class DrilldownColumns : Form, IDrilldownColumnsView
    {
        private AppConfig _appConfig;
        private WorkbookPropertiesConfig _workbookPropertiesConfig;


        public DrilldownColumns(AppConfig appConfig, WorkbookPropertiesConfig workbookPropertiesConfig)
        {
            InitializeComponent();
            _appConfig = appConfig;
            _workbookPropertiesConfig = workbookPropertiesConfig;
            IDrilldownColumnsPresenter presenter = new DrilldownColumnsPresenter(
                this,
                new DatabaseConnectionFactory().CreateDbConnection(appConfig.DatabaseType, appConfig.ConnectionString));
            presenter.Initialize();
        }

        public BindingList<string> AvailableColumns
        {
            get { return (BindingList<string>)lbox_available_columns.DataSource; }
            set { lbox_available_columns.DataSource = value; }
        }

        public BindingList<string> HighlightedAvailableColumns
        {
            get { return Utility.ConvertSelectedObjectCollectionToList(lbox_available_columns.SelectedItems); }
        }

        public int HighlightedSelectedColumnIndex
        {
            get { return lbox_selected_columns.SelectedIndex; }
        }

        public BindingList<string> SelectedColumns
        {
            get { return (BindingList<string>)lbox_selected_columns.DataSource; }
            set { lbox_selected_columns.DataSource = value; }
        }

        public WorkbookPropertiesConfig WorkbookPropertiesConfig
        {
            get { return _workbookPropertiesConfig; }
            set { _workbookPropertiesConfig = value; }
        }

        public void Attach(IDrilldownColumnsPresenterCallbacks presenter)
        {
            but_add.Click += (sender, e) => presenter.OnAdd();
            but_remove.Click += (sender, e) => presenter.OnRemove();
            but_ok.Click += (sender, e) => presenter.OnOk();
            but_cancel.Click += (sender, e) => presenter.OnCancel();
        }

        public void CloseForm()
        {
            Close();
        }
    }
}
