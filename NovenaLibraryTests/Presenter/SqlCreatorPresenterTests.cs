using Microsoft.VisualStudio.TestTools.UnitTesting;
using NovenaLibrary.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using NovenaLibrary.View;
using NovenaLibrary.Config;
using System.ComponentModel;
using System.Windows.Forms;

namespace NovenaLibrary.Presenter.Tests
{
    [TestClass]
    public class SqlCreatorPresenterTests
    {
        public ISqlCreatorView view;
        public SqlCreatorPresenter presenter;
        public AppConfig appConfig;
        public WorkbookPropertiesConfig workbookPropertiesConfig;

        [TestInitialize]
        public void run_before_each_test_method()
        {
            appConfig = new AppConfig();
            appConfig.Username = "chris";
            appConfig.Password = "novena-dev";
            appConfig.ConnectionString = "Server=novena-dev.csggfzanp0wj.us-west-2.rds.amazonaws.com;Port=5432;Database=novena_dev;Username={0};Password={1};SSL Mode=Prefer;Trust Server Certificate=true;CommandTimeout=180";
            appConfig.DatabaseType = DatabaseType.PostgreSQL;

            workbookPropertiesConfig = new WorkbookPropertiesConfig();

            view = MockRepository.GenerateStub<ISqlCreatorView>();
            view.AppConfig = appConfig;
            view.WorkbookPropertiesConfig = workbookPropertiesConfig;
            
            presenter = new SqlCreatorPresenter(view);

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
        public void OnCBoxTableIndexChangedTest()
        {
            Assert.Fail();
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
        public void OnMoveSelectedColumnDownTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void OnMoveSelectedColumnUpTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void OnOkTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void OnRemoveSelectedColumnTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void OnLoadTest()
        {
            Assert.Fail();
        }
    }
}