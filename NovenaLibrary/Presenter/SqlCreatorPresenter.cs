using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.View;

namespace NovenaLibrary.Presenter
{
    public class SqlCreatorPresenter : ISqlCreatorPresenter, ISqlCreatorPresenterCallbacks
    {
        private ISqlCreatorView _view;

        public SqlCreatorPresenter(ISqlCreatorView view)
        {
            _view = view;
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
            throw new NotImplementedException();
        }

        public void OnAddSelectedColumn()
        {
            throw new NotImplementedException();
        }

        public void OnCancel()
        {
            throw new NotImplementedException();
        }

        public void OnCBoxTableIndexChanged()
        {
            throw new NotImplementedException();
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
    }
}
