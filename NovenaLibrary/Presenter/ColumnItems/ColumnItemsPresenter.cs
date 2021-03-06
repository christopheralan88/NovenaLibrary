﻿using System;
using System.Collections.Generic;
using NovenaLibrary.View.ColumnItems;
using QueryBuilder.DatabaseConnections;
using QueryBuilder.SqlGenerators;
using QueryBuilder.Exceptions;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;
using NovenaLibrary.Utilities;

namespace NovenaLibrary.Presenter.ColumnItems
{
    public class ColumnItemsPresenter : IColumnItemsPresenter, IColumnItemsPresenterCallbacks
    {
        private IColumnItemsView _view;
        private string _column;
        private string _table;
        private IDatabaseConnection _dbConnection;
        private BaseSqlGenerator _sqlGenerator;
        private long currentOffset = 0;
        private DataTable _tableSchema;

        public ColumnItemsPresenter(IColumnItemsView view, string column, string table, 
                                    IDatabaseConnection dbConnection, BaseSqlGenerator sqlGenerator)
        {
            _view = view;
            _column = column;
            _table = table;
            _dbConnection = dbConnection;
            _sqlGenerator = sqlGenerator;

            try
            {
                _tableSchema = _dbConnection.GetColumns(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
                _view.CloseForm();
            }

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
            BindingList<string> newList;
            if (_view.SelectedItems != null)
            {
                newList = _view.SelectedItems;
            }
            else
            {
                newList = new BindingList<string>();
            }
            
            foreach (var item in _view.HighlightedAvailableItems)
            {
                newList.Add(item);
            }
            _view.SelectedItems = newList;
        }

        public void OnCancel()
        {
            _view.CloseForm();
        }

        public void OnFind()
        {
            _view.FindButtonEnabled = false;
            _view.AscendingButtonEnabled = false;
            _view.DescendingButtonEnabled = false;
            _view.PriorButtonEnabled = true;
            _view.NextButtonEnabled = true;

            TogglePriorAndNextButtonsEnabled();

            GetColumnItems();
        }

        public void OnNext()
        {
            if (_view.PageSize != null) {
                currentOffset += (long)_view.PageSize;

                TogglePriorAndNextButtonsEnabled();

                GetColumnItems();
            }
        }

        public void OnOk()
        {
            _view.ReturnFilter = Utility.Stringify(_view.SelectedItems, ',');

            _view.CloseForm();
        }

        public void OnPrior()
        {
            if (_view.PageSize != null) {
                currentOffset -= (long)_view.PageSize;

                TogglePriorAndNextButtonsEnabled();

                GetColumnItems();
            }
        }

        public void OnRemove()
        {
            if (_view.SelectedItems.Count > 0)
            {
                _view.SelectedItems.RemoveAt(_view.HighlightedSelectedItemIndex);
            }
        }

        private void GetColumnItems()
        {
            var columns = new List<string>() { _column };
            var asc = _view.Ascending;
            var limit = (_view.PageSize != null) ? (long)_view.PageSize : 1000000;
            var query = new Query("column items").SetTableSchema(_tableSchema)
                                                 .SetDistinct(true)
                                                 .SetColumns(columns)
                                                 .SetTable(_table)
                                                 .SetOrderBy(true)
                                                 .SetAscending(asc)
                                                 .SetLimit(limit)
                                                 .SetOffset(currentOffset);

            try
            {
                var sql = _sqlGenerator.CreateSql(query);

                var dt = _dbConnection.Query(sql);

                BindingList<string> columnItemsList = new BindingList<string>();
                foreach (DataRow row in dt.Rows)
                {
                    columnItemsList.Add(row[0].ToString());
                }

                _view.AvailableItems = columnItemsList;
            }
            catch (BadSQLException ex)
            {
                MessageBox.Show(ex.Message, "Bad SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void TogglePriorAndNextButtonsEnabled()
        {
            if (currentOffset == 0)
            {
                _view.PriorButtonEnabled = false;
                _view.NextButtonEnabled = true;
            }
            else if (currentOffset > 0)
            {
                _view.PriorButtonEnabled = true;
                _view.NextButtonEnabled = true;
            }
        }

    }
}
