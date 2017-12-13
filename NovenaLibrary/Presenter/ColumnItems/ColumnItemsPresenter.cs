using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.View.ColumnItems;
using NovenaLibrary.Repositories;
using NovenaLibrary.SqlGenerators;
using NovenaLibrary.View;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;

namespace NovenaLibrary.Presenter.ColumnItems
{
    public class ColumnItemsPresenter : IColumnItemsPresenter, IColumnItemsPresenterCallbacks
    {
        private IColumnItemsView _view;
        private string _column;
        private string _table;
        private IDatabaseConnection _dbConnection;
        private BaseSqlGenerator _sqlGenerator;
        private int currentOffset = 0;
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
                _tableSchema = _dbConnection.getSchema(table);
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
            foreach (var item in _view.HighlightedAvailableItems)
            {
                _view.SelectedItems.Add(item);
            }
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

            GetColumnItems();
        }

        public void OnNext()
        {
            currentOffset += int.Parse(_view.PageSize);

            GetColumnItems();
        }

        public void OnOk()
        {
            _view.ReturnFilter = StringifyList(_view.SelectedItems);

            _view.CloseForm();
        }

        public void OnPrior()
        {
            currentOffset -= int.Parse(_view.PageSize);

            GetColumnItems();
        }

        public void OnRemove()
        {
            _view.SelectedItems.RemoveAt(_view.HighlightedSelectedItemIndex);
        }

        private void GetColumnItems()
        {
            var columns = new List<string>() { _column };
            var asc = _view.Ascending;
            var sql = _sqlGenerator.CreateSql(tableSchema: _tableSchema, distinct: true, columns: columns,
                                              table: _table, asc: asc, limit: _view.PageSize,
                                              offset: currentOffset.ToString());

            var dt = _dbConnection.query(sql);

            BindingList<string> columnItemsList = new BindingList<string>();
            foreach (DataRow row in dt.Rows)
            {
                columnItemsList.Add(row[0].ToString());
            }

            _view.AvailableItems = columnItemsList;
        }

        private string StringifyList(IList<string> list)
        {
            if (list.Count == 0) return null;
            if (list.Count == 1)
            {
                return list[0];
            }
            else
            {
                string result = "";
                foreach (var item in list)
                {
                    result += item + ',';
                }
                return result.Substring(0, result.Length - 1); //chop off trailing ","
            }
        }
    }
}
