using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NovenaLibrary.Presenter.ColumnItems;
using NovenaLibrary.Utilities;
using NovenaLibrary.Config;

namespace NovenaLibrary.View.ColumnItems
{
    public partial class ColumnItemsView : Form, IColumnItemsView
    {
        private AppConfig _appConfig;
        private WorkbookPropertiesConfig _workbookPropertiesConfig;
        private string _returnFilter;
        private ISqlCreatorView _parentForm;

        public ColumnItemsView(AppConfig appConfig, WorkbookPropertiesConfig workbookPropertiesConfig, ISqlCreatorView parentForm)
        {
            InitializeComponent();
            _appConfig = appConfig;
            _workbookPropertiesConfig = workbookPropertiesConfig;
            _parentForm = parentForm;
            IColumnItemsPresenter presenter = new ColumnItemsPresenter(
                this,
                parentForm.SelectedCriteria.Column,
                parentForm.AvailableTablesText,
                new DatabaseConnectionFactory().CreateDbConnection(appConfig),
                new SqlGeneratorFactory().CreateSqlGenerator(appConfig.DatabaseType));
            presenter.Initialize();

            // Makes first item in drop down box the selected item, so that blank choice disappears and "" will not be passed to presenter for limit.
            cbox_paging_limit.SelectedIndex = 0; 
        }

        public bool Ascending
        {
            get { return radBut_asc.Checked; }
        }

        public bool AscendingButtonEnabled
        {
            get { return radBut_asc.Enabled; }
            set { radBut_asc.Enabled = value; }
        }

        public BindingList<string> AvailableItems
        {
            get { return (BindingList<string>)lbox_available_members.DataSource; }
            set { lbox_available_members.DataSource = value; }
        }

        public bool Descending
        {
            get { return radBut_desc.Checked; }
        }

        public bool DescendingButtonEnabled
        {
            get { return radBut_desc.Enabled; }
            set { radBut_desc.Enabled = value; }
        }

        public bool FindButtonEnabled
        {
            get { return but_find_members.Enabled; }
            set { but_find_members.Enabled = value; }
        }

        public BindingList<string> HighlightedAvailableItems
        {
            get { return Utility.ConvertSelectedObjectCollectionToList(lbox_available_members.SelectedItems); }
        }

        public int HighlightedSelectedItemIndex
        {
            get { return lbox_available_members.SelectedIndex; }
        }

        public bool NextButtonEnabled
        {
            get { return but_next_page.Enabled; }
            set { but_next_page.Enabled = value; }
        }

        public string PageSize
        {
            get { return cbox_paging_limit.Text; }
        }

        public bool PriorButtonEnabled
        {
            get { return but_prior_page.Enabled; }
            set { but_prior_page.Enabled = value; }
        }

        public string ReturnFilter
        {
            get { return _returnFilter; }
            set { _returnFilter = value; }
        }

        public string SearchCriteria
        {
            get { return txtBox_search.Text; }
        }

        public BindingList<string> SelectedItems
        {
            get { return Utility.ConvertSelectedObjectCollectionToList(lbox_selected_members.SelectedItems); }
            set { lbox_selected_members.DataSource = value; }
        }

        public void Attach(IColumnItemsPresenterCallbacks presenter)
        {
            but_find_members.Click += (sender, e) => presenter.OnFind();
            but_prior_page.Click += (sender, e) => presenter.OnPrior();
            but_next_page.Click += (sender, e) => presenter.OnNext();
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
