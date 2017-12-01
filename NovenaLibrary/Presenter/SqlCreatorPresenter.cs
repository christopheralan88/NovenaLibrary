using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.View;
using NovenaLibrary.Repositories;
using NovenaLibrary.Config;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;

namespace NovenaLibrary.Presenter
{
    public class SqlCreatorPresenter : ISqlCreatorPresenter, ISqlCreatorPresenterCallbacks
    {
        private ISqlCreatorView _view;
        private IDatabaseConnection dbConnection;
        private readonly string DELETE_ROW_MESSAGE = "You must select a row to delete";

        public SqlCreatorPresenter(ISqlCreatorView view, IDatabaseConnection dbConnection)
        {
            _view = view;
            this.dbConnection = dbConnection;
        }

        public object UI
        {
            get { return _view; }
        }

        public void Initialize()
        {
            _view.Attach(this);
        }

        public void OnAddRow()
        {
            _view.Criteria.Add(new Criteria());
        }

        public void OnAddSelectedColumn()
        {
            foreach (var column in _view.HighlightedAvailableColumns)
            {
                _view.SelectedColumns.Add(column);
            }
        }

        public void OnCancel()
        {
            throw new NotImplementedException();
        }

        public void OnCBoxTableIndexChanged()
        {
            var table = _view.AvailableTablesText;
            var dt = dbConnection.getSchema(table);

            var columnList = (from row in dt.AsEnumerable()
                              select row.Field<string>("COLUMN_NAME")).ToList();

            _view.AvailableColumns = new BindingList<string>(columnList);
        }

        public void OnColumnItemsClick()
        {
            throw new NotImplementedException();
        }

        public void OnDeleteRow()
        {
            var highlightedCriteriaIndex = _view.HighlightedCriteriaIndex;
            if (highlightedCriteriaIndex != null)
            {
                _view.Criteria.RemoveAt((int)highlightedCriteriaIndex);
            }
            else
            {
                ShowMessage(DELETE_ROW_MESSAGE);
            }
        }

        public void OnMoveSelectedColumnDown()
        {
            var currentIndex = _view.HighlightedSelectedColumnIndex;
            var currentColumn = _view.HighlightedSelectedColumn;

            // if item is not last item in bindinglist
            if (currentIndex != _view.SelectedColumns.Count - 1)
            {
                _view.SelectedColumns.Insert(currentIndex + 2, currentColumn);
               _view.SelectedColumns.RemoveAt(currentIndex);
            }
        }

        public void OnMoveSelectedColumnUp()
        {
            var currentIndex = _view.HighlightedSelectedColumnIndex;
            var currentColumn = _view.HighlightedSelectedColumn;

            // if item is not first item in bindinglist
            if (currentIndex != 0)
            {
                _view.SelectedColumns.Insert(currentIndex - 1, currentColumn);
                _view.SelectedColumns.RemoveAt(currentIndex + 1);
            }
        }

        public void OnOk()
        {
            throw new NotImplementedException();
        }

        public void OnRemoveSelectedColumn()
        {
            _view.SelectedColumns.RemoveAt(_view.HighlightedSelectedColumnIndex);
        }

        public void OnLoad()
        {
            // get avialable tables
            var sql = string.Format(AvailableTablesSql.availableTablesSql[_view.AppConfig.DatabaseType], _view.AppConfig.Username);
            var dt = dbConnection.query(sql);
            var tableList = (from row in dt.AsEnumerable()
                             select row.Field<string>("COLUMN_NAME")).ToList();
            _view.AvailableTables = new BindingList<string>(tableList);

            // set available tables' combo box text to selected table
            _view.AvailableTablesText = _view.WorkbookPropertiesConfig.selectedTable;

            // set available columns list box and Column combo box in dgv
            var dtAvailableColumns = dbConnection.getSchema(_view.AvailableTablesText);

            var availableColumnsList = (from row in dtAvailableColumns.AsEnumerable()
                                        select row.Field<string>("COLUMN_NAME")).ToList();

            _view.AvailableColumns = new BindingList<string>(availableColumnsList);
            _view.AvailableColumnDGV = new BindingList<string>(availableColumnsList);

            // set selected columns list box values to selected columns
            _view.SelectedColumns = new BindingList<string>(_view.WorkbookPropertiesConfig.selectedColumns);

            // set criteria for dgv
            _view.Criteria = new BindingList<Criteria>(_view.WorkbookPropertiesConfig.criteria);
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
