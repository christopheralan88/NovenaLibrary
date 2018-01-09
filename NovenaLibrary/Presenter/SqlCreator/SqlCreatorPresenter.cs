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
using NovenaLibrary.SqlGenerators;
using NovenaLibrary.View.ColumnItems;
using NovenaLibrary.View.SqlCreator;
using NovenaLibrary.Exceptions;

namespace NovenaLibrary.Presenter.SqlCreator
{
    public class SqlCreatorPresenter : ISqlCreatorPresenter, ISqlCreatorPresenterCallbacks
    {
        private ISqlCreatorView _view;
        private IDatabaseConnection dbConnection;
        private BaseSqlGenerator sqlGenerator;
        private DataTable tableSchema;
        private readonly string DELETE_ROW_MESSAGE = "You must select a row to delete";
        private readonly string QUERY_RETURNED_NO_RECORDS = "The query returned no records";

        public SqlCreatorPresenter(ISqlCreatorView view, IDatabaseConnection dbConnection, BaseSqlGenerator sqlGenerator)
        {
            _view = view;
            this.dbConnection = dbConnection;
            this.sqlGenerator = sqlGenerator;
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

        public void OnCBoxTableIndexChanged()
        {
            var table = _view.AvailableTablesText;
            tableSchema = dbConnection.getSchema(table);

            var columnList = (from row in tableSchema.AsEnumerable()
                              select row.Field<string>("COLUMN_NAME")).ToList();

            if (_view.AvailableColumns != null)
            {
                _view.AvailableColumns = null;
            }
            _view.AvailableColumns = new BindingList<string>(columnList);

            if (_view.AvailableColumnDGV != null)
            {
                _view.AvailableColumnDGV.Clear();
            }
            //foreach (var column in columnList)
            //{
            //    _view.AvailableColumnDGV.Add(column);
            //}
            _view.AvailableColumnDGV = new BindingList<string>(columnList);
            _view.AvailableColumnDGV.ResetBindings();

            if (_view.SelectedColumns != null) _view.SelectedColumns.Clear();

            if (_view.Criteria != null) _view.Criteria.Clear();
        }

        public void OnColumnItemsClick()
        {
            var columnItemsForm = new ColumnItemsView(_view.AppConfig, _view.WorkbookPropertiesConfig, _view);
            var result = columnItemsForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Get form's DataGridView's current criteria.
                var criteria = _view.SelectedCriteria;
                // Set the criteria's filter property to the Column Items Form's ReturnFilter property.
                criteria.Filter = columnItemsForm.ReturnFilter;
                // Update the form's DataGridView's current criteria with the new filter.
                _view.SelectedCriteria = criteria;
            }
        }

        public void OnDeleteRow()
        {
            if (_view.HighlightedCriteriaIndex != null)
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
        }

        public void OnMoveSelectedColumnDown()
        {
            var currentColumn = _view.HighlightedSelectedColumn;

            if (currentColumn != null)
            {
                // if item is not last item in bindinglist
                var currentIndex = _view.HighlightedSelectedColumnIndex;
                if (currentIndex != _view.SelectedColumns.Count - 1)
                {
                    _view.SelectedColumns.Insert(currentIndex + 2, currentColumn);
                    _view.SelectedColumns.RemoveAt(currentIndex);
                }
            }
        }

        public void OnMoveSelectedColumnUp()
        {
            var currentColumn = _view.HighlightedSelectedColumn;

            if (currentColumn != null)
            {
                // if item is not first item in bindinglist
                var currentIndex = _view.HighlightedSelectedColumnIndex;
                if (currentIndex != 0)
                {
                    _view.SelectedColumns.Insert(currentIndex - 1, currentColumn);
                    _view.SelectedColumns.RemoveAt(currentIndex + 1);
                }
            }
        }

        public void OnOk()
        {
            var columns = _view.SelectedColumns.ToList();
            var table = _view.AvailableTablesText;
            var criteria = _view.Criteria.ToList();
            var groupBy = _view.GroupBy;
            var limit = _view.Limit;
            var query = new Query("main").SetTableSchema(tableSchema).SetColumns(columns).SetTable(table).SetCriteria(criteria).SetGroupBy(groupBy).SetLimit(limit);
            //string sql = sqlGenerator.CreateSql(tableSchema: tableSchema, columns: columns, table: table, criteria: criteria, groupBy: groupBy, limit: limit);
            try
            {
                string sql = sqlGenerator.CreateSql(query);

                DataTable dt;
                try
                {
                    dt = dbConnection.query(sql);
                    if (dt.Rows.Count > 0)
                    {
                        _view.SQLResult.Add(query.QueryName, dt);
                        _view.WorkbookPropertiesConfig.LastMainQuery = query;
                        _view.WorkbookPropertiesConfig.SelectedTable = _view.AvailableTablesText;
                        _view.WorkbookPropertiesConfig.SelectedColumns = _view.SelectedColumns.ToList();
                        _view.WorkbookPropertiesConfig.Criteria = _view.Criteria.ToList();
                        _view.WorkbookPropertiesConfig.Limit = _view.Limit;
                        _view.CloseForm();
                    }
                    else
                    {
                        ShowMessage(QUERY_RETURNED_NO_RECORDS);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage(ex.Message);
                }
            }
            catch (BadSQLException ex)
            {
                MessageBox.Show(ex.Message, "Bad SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        public void OnRemoveSelectedColumn()
        {
            _view.SelectedColumns.RemoveAt(_view.HighlightedSelectedColumnIndex);
        }

        public void OnLoad()
        {
            // get available tables
            var sql = _view.AppConfig.AvailableTablesSql;
            //var sql = string.Format(AvailableTablesSql.availableTablesSql[_view.AppConfig.DatabaseType], _view.AppConfig.User.Username);
            var dt = dbConnection.query(sql);

            //TODO:  Handle when an empty or null datatable is returned by query() method.

            BindingList<string> tableList = new BindingList<string>();
            foreach (DataRow row in dt.Rows)
            {
                tableList.Add(row[0].ToString());
            }

            _view.AvailableTables = new BindingList<string>(tableList);

            // set available tables' combo box text to selected table
            _view.AvailableTablesText = _view.WorkbookPropertiesConfig.SelectedTable;

            // set available columns list box and Column combo box in dgv
            var dtAvailableColumns = dbConnection.getSchema(_view.AvailableTablesText);

            var availableColumnsList = (from row in dtAvailableColumns.AsEnumerable()
                                        select row.Field<string>("COLUMN_NAME")).ToList();

            _view.AvailableColumns = new BindingList<string>(availableColumnsList);
            _view.AvailableColumnDGV = new BindingList<string>(availableColumnsList);

            // set selected columns list box values to selected columns
            _view.SelectedColumns = new BindingList<string>(_view.WorkbookPropertiesConfig.SelectedColumns);

            // set criteria for dgv
            _view.Criteria = new BindingList<Criteria>(_view.WorkbookPropertiesConfig.Criteria);
        }

        public void OnDGVDataError()
        {
            
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

    }
}
