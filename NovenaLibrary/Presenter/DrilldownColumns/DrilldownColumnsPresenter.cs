using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Repositories;
using NovenaLibrary.View.DrilldownColumns;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using NovenaLibrary.Utilities;

namespace NovenaLibrary.Presenter.DrilldownColumns
{
    public class DrilldownColumnsPresenter : IDrilldownColumnsPresenter, IDrilldownColumnsPresenterCallbacks
    {
        private IDrilldownColumnsView _view;
        private IDatabaseConnection _dbConnection;

        public DrilldownColumnsPresenter(IDrilldownColumnsView view, IDatabaseConnection dbConnection)
        {
            _view = view;
            _dbConnection = dbConnection;
        }

        public object UI
        {
            get { return _view; }
        }

        public void Initialize()
        {
            _view.Attach(this);
        }

        public void OnAdd()
        {
            foreach (var column in _view.HighlightedAvailableColumns)
            {
                _view.SelectedColumns.Add(column);
            }
        }

        public void OnCancel()
        {
            _view.CloseForm();
        }

        public void OnLoad()
        {
            // populate detail table's available columns
            DataTable dt;
            try
            {
                dt = _dbConnection.getSchema(_view.WorkbookPropertiesConfig.SelectedTable + "_detail");

                var availableColumnsList = (from row in dt.AsEnumerable()
                                            select row.Field<string>("COLUMN_NAME")).ToList();

                _view.AvailableColumns = new BindingList<string>(availableColumnsList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
                return;
            }

            // populate selected drilldown columns
            var selectedColumnList = _view.WorkbookPropertiesConfig.DrilldownSql.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            _view.SelectedColumns = new BindingList<string>(selectedColumnList);
        }

        public void OnOk()
        {
            var drilldownColumns = Utility.Stringify(_view.SelectedColumns, ',');

            if (drilldownColumns == null)
            {
                MessageBox.Show("At least one column must be selected.", "No Columns Selected", MessageBoxButtons.OK);
                return;
            }
            else
            {
                _view.WorkbookPropertiesConfig.DrilldownSql = drilldownColumns;
            }
        }

        public void OnRemove()
        {
            _view.SelectedColumns.RemoveAt(_view.HighlightedSelectedColumnIndex);
        }
    }
}
