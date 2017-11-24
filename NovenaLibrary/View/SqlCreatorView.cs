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

namespace NovenaLibrary.View
{
    public partial class SqlCreatorView : Form, ISqlCreatorView
    {
        public SqlCreatorView()
        {
            InitializeComponent();
            ISqlCreatorPresenter presenter = new SqlCreatorPresenter(this);
            presenter.Initialize();
        }

        public List<string> AvailableColumns
        {
            get { return (List<string>)lbox_available_columns.DataSource; }
            set { lbox_available_columns.DataSource = value; }
        }

        public List<string> AvailableTables
        {
            get { return (List<string>)cbox_table.DataSource; }
            set { cbox_table.DataSource = value; }
        }

        public string AvailableTablesText
        {
            get { return cbox_table.Text; }
            set { cbox_table.Text = value; }
        }

        public List<string> SelectedColumns
        {
            get { return (List<string>)lbox_selected_columns.DataSource; }
            set { lbox_selected_columns.DataSource = value; }
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
        }
    }
}
