using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NovenaLibrary.Presenter;
using NovenaLibrary.Config;
using NovenaLibrary.Exceptions;
using NovenaLibrary.Utilities;

namespace NovenaLibrary.View
{
    public partial class SqlCreatorView : Form, ISqlCreatorView
    {
        private AppConfig _appConfig;
        private WorkbookPropertiesConfig _workbookPropertiesConfig;
        private DataTable _sqlResult;

        public SqlCreatorView(AppConfig appConfig, WorkbookPropertiesConfig workbookPropertiesConfig)
        {
            InitializeComponent();
            _appConfig = appConfig;
            _workbookPropertiesConfig = workbookPropertiesConfig;
            ISqlCreatorPresenter presenter = new SqlCreatorPresenter(
                this, 
                new DatabaseConnectionFactory().CreateDbConnection(appConfig),
                new SqlGeneratorFactory().CreateSqlGenerator(AppConfig.DatabaseType));
            presenter.Initialize();
            HighlightedAvailableColumns.AllowNew = true;
        }

        public DataTable SQLResult
        {
            get { return _sqlResult; }
            set
            {
                if (_sqlResult == null)
                {
                    _sqlResult = value;
                }
                else
                {
                    throw new PropertyAlreadySetException();
                }
            }
        }

        public AppConfig AppConfig
        {
            get { return _appConfig; }
            set { _appConfig = value; }
        }

        public WorkbookPropertiesConfig WorkbookPropertiesConfig
        {
            get { return _workbookPropertiesConfig; }
            set { _workbookPropertiesConfig = value; }
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

        public BindingList<string> AvailableTables
        {
            get { return (BindingList<string>)cbox_table.DataSource; }
            set { cbox_table.DataSource = value; }
        }

        public string AvailableTablesText
        {
            get { return cbox_table.Text; }
            set { cbox_table.Text = value; }
        }

        public BindingList<string> SelectedColumns
        {
            get { return (BindingList<string>)lbox_selected_columns.DataSource; }
            set { lbox_selected_columns.DataSource = value; }
        }

        public int HighlightedSelectedColumnIndex
        {
            get { return lbox_selected_columns.SelectedIndex; }
        }

        public string HighlightedSelectedColumn
        {
            get { return lbox_selected_columns.SelectedItem.ToString(); }
        }

        public BindingList<string> AvailableColumnDGV
        {
            get { return (BindingList<string>)Column.DataSource; }
            set { Column.DataSource = value; }
        }

        public BindingList<Criteria> Criteria
        {
            get { return (BindingList<Criteria>)datagrid_criteria.DataSource; }
            set { datagrid_criteria.DataSource = value; }
        }

        public int? HighlightedCriteriaIndex
        {
            get { return datagrid_criteria.SelectedRows[0].Index; }
        }

        public bool GroupBy { get { return ckbox_groupBy.Checked; } }

        public string Limit { get { return txt_box_limit.Text; } }

        public Criteria SelectedCriteria
        {
            get
            {
                var index = datagrid_criteria.SelectedCells[0].RowIndex;
                var dataSourceList = (BindingList<Criteria>)datagrid_criteria.DataSource;
                return dataSourceList[index];
            }
        }

        public void CloseForm()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public void Attach(ISqlCreatorPresenterCallbacks callback)
        {
            cbox_table.SelectedIndexChanged += (sender, e) => callback.OnCBoxTableIndexChanged();
            but_add.Click += (sender, e) => callback.OnAddSelectedColumn();
            but_remove.Click += (sender, e) => callback.OnRemoveSelectedColumn();
            but_up.Click += (sender, e) => callback.OnMoveSelectedColumnUp();
            but_down.Click += (sender, e) => callback.OnMoveSelectedColumnDown();
            but_column_items.Click += (sender, e) => callback.OnColumnItemsClick();
            but_add_row.Click += (sender, e) => callback.OnAddRow();
            but_delete_row.Click += (sender, e) => callback.OnDeleteRow();
            but_ok.Click += (sender, e) => callback.OnOk();
            this.Load += (sender, e) => callback.OnLoad();
        }

        //private BindingList<string> ConvertSelectedObjectCollectionToList(ListBox.SelectedObjectCollection collection)
        //{
        //    var list = new BindingList<string>();
        //    foreach (var item in collection)
        //    {
        //        list.Add(item.ToString());
        //    }
        //    return list;
        //}
    }
}
