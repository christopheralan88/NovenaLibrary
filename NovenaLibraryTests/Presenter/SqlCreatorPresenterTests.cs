using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using NovenaLibrary.View;
using NovenaLibrary.Config;
using System.ComponentModel;
using NovenaLibrary.Repositories;
using System.Data;
using Npgsql;

namespace NovenaLibrary.Presenter.Tests
{
    [TestClass]
    public class SqlCreatorPresenterTests
    {
        public ISqlCreatorView view;
        public SqlCreatorPresenter presenter;
        public AppConfig appConfig;
        public WorkbookPropertiesConfig workbookPropertiesConfig;
        public IDatabaseConnection dbConnection;

        [TestInitialize]
        public void run_before_each_test_method()
        {
            appConfig = new AppConfig();
            appConfig.Username = "username";
            appConfig.Password = "password";
            appConfig.ConnectionString = "fake connection string";
            appConfig.DatabaseType = DatabaseType.PostgreSQL;

            workbookPropertiesConfig = new WorkbookPropertiesConfig();

            view = MockRepository.GenerateStub<ISqlCreatorView>();
            view.AppConfig = appConfig;
            view.WorkbookPropertiesConfig = workbookPropertiesConfig;

            dbConnection = MockRepository.GenerateStub<IDatabaseConnection>();
            
            presenter = new SqlCreatorPresenter(view, dbConnection);
        }

        [TestMethod]
        public void Initialize_CallsViewsAttachMethod()
        {
            view.Expect(x => x.Attach(presenter));

            presenter.Initialize();

            view.VerifyAllExpectations();
        }

        [TestMethod]
        public void OnAddRowTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void OnAddSelectedColumn_AddingMulitpleColumns()
        {
            view.SelectedColumns = new BindingList<string>(); // so that property is not null
            BindingList<string> selectedAvailableColumns = new BindingList<string>() { "column1", "column2" };
            view.Stub(x => x.HighlightedAvailableColumns).Return(selectedAvailableColumns);

            presenter.OnAddSelectedColumn();

            Assert.IsTrue(view.SelectedColumns.Count == selectedAvailableColumns.Count);
        }

        [TestMethod]
        public void OnAddSelectedColumn_AddingNoColumns()
        {
            view.SelectedColumns = new BindingList<string>() { "column1", "column2" }; // so that property is not null
            BindingList<string> selectedAvailableColumns = new BindingList<string>();
            view.Stub(x => x.HighlightedAvailableColumns).Return(selectedAvailableColumns);

            presenter.OnAddSelectedColumn();

            Assert.IsTrue(view.SelectedColumns.Count == 2);
        }

        [TestMethod]
        public void OnCancelTest()
        {
            Assert.Fail();
        }
   
        [TestMethod]
        public void OnCBoxTableIndexChanged_AvailableColumnsAreUpdatedWithOneColumn()
        {
            view.AvailableTablesText = "table1";
            view.AvailableColumns = new BindingList<string>();
            dbConnection.Stub(x => x.getSchema("table1")).Return(DataTableBuilder());

            presenter.OnCBoxTableIndexChanged();

            Assert.IsTrue(view.AvailableColumns.Count > 0);
        }

        [TestMethod]
        public void OnColumnItemsClickTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void OnDeleteRowTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void OnMoveSelectedColumnDown_ColumnIsNotLastColumn()
        {
            var selectedColumnIndex = 1;
            view.SelectedColumns = new BindingList<string>() { "column1", "column2", "column3" };
            view.Stub(x => x.HighlightedSelectedColumnIndex).Return(selectedColumnIndex);
            view.Stub(x => x.HighlightedSelectedColumn).Return(view.SelectedColumns[selectedColumnIndex]);

            presenter.OnMoveSelectedColumnDown();

            Assert.IsTrue(view.SelectedColumns[0] == "column1");
            Assert.IsTrue(view.SelectedColumns[1] == "column3");
            Assert.IsTrue(view.SelectedColumns[2] == "column2");
            Assert.IsTrue(view.SelectedColumns.Count == 3);
        }

        [TestMethod]
        public void OnMoveSelectedColumnDown_ColumnIsLastColumn()
        {
            var selectedColumnIndex = 2;
            view.SelectedColumns = new BindingList<string>() { "column1", "column2", "column3" };
            view.Stub(x => x.HighlightedSelectedColumnIndex).Return(selectedColumnIndex);
            view.Stub(x => x.HighlightedSelectedColumn).Return(view.SelectedColumns[selectedColumnIndex]);

            presenter.OnMoveSelectedColumnDown();

            Assert.IsTrue(view.SelectedColumns[0] == "column1");
            Assert.IsTrue(view.SelectedColumns[1] == "column2");
            Assert.IsTrue(view.SelectedColumns[2] == "column3");
            Assert.IsTrue(view.SelectedColumns.Count == 3);
        }

        [TestMethod]
        public void OnMoveSelectedColumnUp_ColumnIsNotFirstColumn()
        {
            var selectedColumnIndex = 1;
            view.SelectedColumns = new BindingList<string>() { "column1", "column2", "column3" };
            view.Stub(x => x.HighlightedSelectedColumnIndex).Return(selectedColumnIndex);
            view.Stub(x => x.HighlightedSelectedColumn).Return(view.SelectedColumns[selectedColumnIndex]);

            presenter.OnMoveSelectedColumnUp();

            Assert.IsTrue(view.SelectedColumns[0] == "column2");
            Assert.IsTrue(view.SelectedColumns[1] == "column1");
            Assert.IsTrue(view.SelectedColumns[2] == "column3");
            Assert.IsTrue(view.SelectedColumns.Count == 3);
        }

        [TestMethod]
        public void OnMoveSelectedColumnUp_ColumnIsFirstColumn()
        {
            var selectedColumnIndex = 0;
            view.SelectedColumns = new BindingList<string>() { "column1", "column2", "column3" };
            view.Stub(x => x.HighlightedSelectedColumnIndex).Return(selectedColumnIndex);
            view.Stub(x => x.HighlightedSelectedColumn).Return(view.SelectedColumns[selectedColumnIndex]);

            presenter.OnMoveSelectedColumnUp();

            Assert.IsTrue(view.SelectedColumns[0] == "column1");
            Assert.IsTrue(view.SelectedColumns[1] == "column2");
            Assert.IsTrue(view.SelectedColumns[2] == "column3");
            Assert.IsTrue(view.SelectedColumns.Count == 3);
        }

        [TestMethod]
        public void OnOkTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void OnRemoveSelectedColumn_RemovingOneColumn()
        {
            view.SelectedColumns = new BindingList<string>() { "column1", "column2", "column3" };
            view.Stub(x => x.HighlightedSelectedColumnIndex).Return(1);

            presenter.OnRemoveSelectedColumn();

            Assert.IsTrue(view.SelectedColumns[0] == "column1");
            Assert.IsTrue(view.SelectedColumns[1] == "column3");
            Assert.IsTrue(view.SelectedColumns.Count == 2);
        }

        [TestMethod]
        public void OnLoadTest()
        {
            Assert.Fail();
        }

        private DataTable DataTableBuilder()
        {
            //IDatabaseConnection dbConnection = new DatabaseConnection(new NpgsqlConnection(appConfig.ConnectionString), new NpgsqlCommand());
            //return dbConnection.getSchema("county_spending");
            DataTable dt = new DataTable();
            dt.Columns.Add("COLUMN_NAME");
            dt.Columns.Add("col2");
            dt.Columns.Add("col3");
            dt.Columns.Add("col4");
            dt.Columns.Add("col5");
            dt.Columns.Add("col6");
            dt.Columns.Add("col7");
            dt.Columns.Add("DATA_TYPE");
            dt.Rows.Add(new object[] { "fund", null, null, null, null, null, null, "text" });

            return dt;
        }
    }
}