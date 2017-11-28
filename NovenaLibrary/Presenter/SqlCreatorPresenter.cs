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

namespace NovenaLibrary.Presenter
{
    public class SqlCreatorPresenter : ISqlCreatorPresenter, ISqlCreatorPresenterCallbacks
    {
        private ISqlCreatorView _view;
        private DatabaseConnection dbConnection;

        public SqlCreatorPresenter(ISqlCreatorView view)
        {
            _view = view;
            dbConnection = new DatabaseConnectionFactory().createDbConnection(_view.AppConfig);
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
            throw new NotImplementedException();
        }

        public void OnMoveSelectedColumnDown()
        {
            throw new NotImplementedException();
        }

        public void OnMoveSelectedColumnUp()
        {
            throw new NotImplementedException();
        }

        public void OnOk()
        {
            throw new NotImplementedException();
        }

        public void OnRemoveSelectedColumn()
        {
            throw new NotImplementedException();
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

            // set available columns list box
            var dtAvailableColumns = dbConnection.getSchema(_view.AvailableTablesText);

            var availableColumnsList = (from row in dtAvailableColumns.AsEnumerable()
                                        select row.Field<string>("COLUMN_NAME")).ToList();

            _view.AvailableColumns = new BindingList<string>(availableColumnsList);

            // set selected columns list box values to selected columns
            _view.SelectedColumns = new BindingList<string>(_view.WorkbookPropertiesConfig.selectedColumns);

            // set Column combo box in dgv to available columns bindinglist

            // set criteria for dgv
        }
    }
}
