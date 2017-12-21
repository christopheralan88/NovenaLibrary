using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using NovenaLibrary.View;
using NovenaLibrary.Config;
using System.ComponentModel;
using NovenaLibrary.Repositories;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using NovenaLibrary.SqlGenerators;
using NovenaLibrary.Presenter.SqlCreator;
using NovenaLibrary.View.SqlCreator;

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
        public BaseSqlGenerator sqlGenerator;

        [TestInitialize]
        public void run_before_each_test_method()
        {
            User user = new User("username", "password", false);

            appConfig = new AppConfig("fake connection string", "sql", DatabaseType.PostgreSQL);
            //appConfig.User.Username = "username";
            //appConfig.Password = "password";
            //appConfig.ConnectionString = "fake connection string";
            //appConfig.DatabaseType = DatabaseType.PostgreSQL;

            workbookPropertiesConfig = new WorkbookPropertiesConfig();

            view = MockRepository.GenerateMock<ISqlCreatorView>();
            view.AppConfig = appConfig;
            view.WorkbookPropertiesConfig = workbookPropertiesConfig;

            dbConnection = MockRepository.GenerateStub<IDatabaseConnection>();

            sqlGenerator = MockRepository.GenerateStub<BaseSqlGenerator>();
            
            presenter = new SqlCreatorPresenter(view, dbConnection, sqlGenerator);
        }

        [TestMethod]
        public void Initialize_CallsViewsAttachMethod()
        {
            view.Expect(x => x.Attach(presenter));

            presenter.Initialize();

            view.VerifyAllExpectations();
        }

        [TestMethod]
        public void OnAddRow_NoEqualCriteria()
        {
            var criteria1 = new Criteria("And", "", "column1", "=", "abc", "", false);
            var criteria2 = new Criteria("And", "", "column2", "Like", "def%", "", false);
            var criteria3 = new Criteria("And", "", "column3", "Not Like", "%gef", "", false);
            BindingList<Criteria> criteria = new BindingList<Criteria>();
            criteria.Add(criteria1);
            criteria.Add(criteria2);
            criteria.Add(criteria3);
            view.Criteria = criteria;

            presenter.OnAddRow();

            Assert.IsTrue(view.Criteria[0] == criteria1);
            Assert.IsTrue(view.Criteria[1] == criteria2);
            Assert.IsTrue(view.Criteria[2] == criteria3);
            Assert.IsTrue(view.Criteria[3].Equals(new Criteria()));
            Assert.IsTrue(view.Criteria.Count == 4);
        }

        [TestMethod]
        public void OnAddRow_ExistingEqualCriteria()
        {
            var criteria1 = new Criteria("And", "", "column1", "=", "abc", "", false);
            var criteria2 = new Criteria("And", "", "column2", "Like", "def%", "", false);
            var criteria3 = new Criteria();
            BindingList<Criteria> criteria = new BindingList<Criteria>();
            criteria.Add(criteria1);
            criteria.Add(criteria2);
            criteria.Add(criteria3);
            view.Criteria = criteria;

            presenter.OnAddRow();

            Assert.IsTrue(view.Criteria[0] == criteria1);
            Assert.IsTrue(view.Criteria[1] == criteria2);
            Assert.IsTrue(view.Criteria[2] == criteria3);
            Assert.IsTrue(view.Criteria[3].Equals(new Criteria()));
            Assert.IsTrue(view.Criteria.Count == 4);
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
        public void OnCBoxTableIndexChanged_AvailableColumnsAreUpdatedWithOneColumn()
        {
            view.AvailableTablesText = "table1";
            view.AvailableColumns = new BindingList<string>();
            dbConnection.Stub(x => x.getSchema("table1")).Return(MultiColumnDataTableBuilder());

            presenter.OnCBoxTableIndexChanged();

            Assert.IsTrue(view.AvailableColumns.Count > 0);
        }

        [TestMethod]
        public void OnColumnItemsClickTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void OnDeleteRow_RowIsSelected()
        {
            var criteria1 = new Criteria("And", "", "column1", "=", "abc", "", false);
            var criteria2 = new Criteria("And", "", "column2", "Like", "def%", "", false);
            var criteria3 = new Criteria("And", "", "column3", "Not Like", "%gef", "", false);
            BindingList<Criteria> criteria = new BindingList<Criteria>();
            criteria.Add(criteria1);
            criteria.Add(criteria2);
            criteria.Add(criteria3);
            view.Criteria = criteria;
            view.Stub(x => x.HighlightedCriteriaIndex).Return(1);

            presenter.OnDeleteRow();

            Assert.IsTrue(view.Criteria[0] == criteria1);
            Assert.IsTrue(view.Criteria[1] == criteria3);
            Assert.IsTrue(view.Criteria.Count == 2);
        }

        [TestMethod]
        public void OnDeleteRow_NoRowSelected()
        {
            var criteria1 = new Criteria("And", "", "column1", "=", "abc", "", false);
            var criteria2 = new Criteria("And", "", "column2", "Like", "def%", "", false);
            var criteria3 = new Criteria("And", "", "column3", "Not Like", "%gef", "", false);
            BindingList<Criteria> criteria = new BindingList<Criteria>();
            criteria.Add(criteria1);
            criteria.Add(criteria2);
            criteria.Add(criteria3);
            view.Criteria = criteria;
            view.Stub(x => x.HighlightedCriteriaIndex).Return(null);

            presenter.OnDeleteRow();

            Assert.IsTrue(view.Criteria.Count == 3);
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
        public void OnOk()
        {
            List<string> columns = new List<string>() { "column1", "column2", "column3" };
            BindingList<string> columnsBindingList = new BindingList<string>(columns);
            string table = "table1";
            var criteria1 = new Criteria("And", "", "column1", "=", "abc", "", false);
            var criteria2 = new Criteria("And", "", "column2", "Like", "def%", "", false);
            var criteria3 = new Criteria("And", "", "column3", "Not Like", "%gef", "", false);
            List<Criteria> criteria = new List<Criteria>();
            criteria.Add(criteria1);
            criteria.Add(criteria2);
            criteria.Add(criteria3);
            BindingList<Criteria> criteriaBindingList = new BindingList<Criteria>(criteria);
            view.Stub(x => x.SelectedColumns).Return(columnsBindingList);
            view.Stub(x => x.AvailableTablesText).Return(table);
            view.Stub(x => x.Criteria).Return(criteriaBindingList);
            view.Stub(x => x.GroupBy).Return(false);
            view.Stub(x => x.Limit).Return(null);
            DataTable dt = MultiColumnDataTableBuilder();
            //view.Stub(x => x.SQLResult).SetPropertyWithArgument(dt);
            dbConnection.Stub(x => x.getSchema(table)).Return(dt);
            Query query = new Query("test").SetTableSchema(dt).SetColumns(columns).SetTable(table).SetCriteria(criteria).SetGroupBy(false).SetLimit(null);
            //sqlGenerator.Stub(x => x.CreateSql(tableSchema: dt, columns: columns, table: table, criteria: criteria, groupBy: false, limit: null)).Return("SELECT * FROM table1");
            sqlGenerator.Stub(x => x.CreateSql(query)).Return("SELECT * FROM table1");
            dbConnection.Stub(x => x.query("SELECT * FROM table1")).Return(MultiColumnDataTableBuilder());

            presenter.OnCBoxTableIndexChanged(); // this is the only method that updates the presenter's tableSchema field.
            presenter.OnOk();

            Assert.IsTrue(view.SQLResult != null);
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
        public void OnLoad_QueriesReturnDataTables()
        {
            var sql = "SELECT table_name FROM information_schema.table_privileges WHERE grantee = 'username' AND privilege_type = 'SELECT';";
            dbConnection.Stub(x => x.query(sql)).Return(SingleColumnDataTableBuilder());
            List<string> tableList = new List<string>() { "column1", "column2", "column3" };
            workbookPropertiesConfig.selectedTable = "table1";
            dbConnection.Stub(x => x.getSchema(workbookPropertiesConfig.selectedTable)).Return(MultiColumnDataTableBuilder());
            workbookPropertiesConfig.selectedColumns = new List<string>() { "column1", "column3" };
            var criteria1 = new Criteria("And", "", "column1", "=", "abc", "", false);
            var criteria2 = new Criteria("And", "", "column2", "Like", "def%", "", false);
            var criteria3 = new Criteria("And", "", "column3", "Not Like", "%gef", "", false);
            List<Criteria> criteria = new List<Criteria>();
            criteria.Add(criteria1);
            criteria.Add(criteria2);
            criteria.Add(criteria3);
            workbookPropertiesConfig.criteria = criteria;

            presenter.OnLoad();

            Assert.IsTrue(view.AvailableTables.Count == tableList.Count);
            Assert.IsTrue(view.AvailableTables[0] == tableList[0]);
            Assert.IsTrue(view.AvailableTables[1] == tableList[1]);
            Assert.IsTrue(view.AvailableTables[2] == tableList[2]);

            Assert.IsTrue(view.AvailableTablesText == workbookPropertiesConfig.selectedTable);

            Assert.IsTrue(view.AvailableColumns.Count == 1);
            Assert.IsTrue(view.AvailableColumnDGV.Count == 1);

            Assert.IsTrue(view.SelectedColumns.Count == workbookPropertiesConfig.selectedColumns.Count);
            Assert.IsTrue(view.AvailableTables[0] == tableList[0]);
            Assert.IsTrue(view.AvailableTables[1] == tableList[1]);

            Assert.IsTrue(view.Criteria.Count == workbookPropertiesConfig.criteria.Count);
        }

        private DataTable MultiColumnDataTableBuilder()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("COLUMN_NAME");
            dt.Columns.Add("col2");
            dt.Columns.Add("col3");
            dt.Columns.Add("col4");
            dt.Columns.Add("col5");
            dt.Columns.Add("col6");
            dt.Columns.Add("col7");
            dt.Columns.Add("DATA_TYPE");
            dt.Rows.Add(new object[] { "column1", null, null, null, null, null, null, "text" });
            dt.Rows.Add(new object[] { "column2", null, null, null, null, null, null, "text" });
            dt.Rows.Add(new object[] { "column3", null, null, null, null, null, null, "int2" });

            return dt;
        }

        private DataTable SingleColumnDataTableBuilder()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("table_name");
            dt.Rows.Add(new object[] { "column1" });
            dt.Rows.Add(new object[] { "column2" });
            dt.Rows.Add(new object[] { "column3" });

            return dt;
        }
    }
}