using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.Presenter.SqlCreator
{
    public interface ISqlCreatorPresenterCallbacks
    {
        void OnColumnItemsClick();
        void OnCBoxTableIndexChanged(); // populate Available Columns
        void OnMoveSelectedColumnDown();
        void OnMoveSelectedColumnUp();
        void OnAddRow();
        void OnDeleteRow();
        void OnRemoveSelectedColumn();
        void OnAddSelectedColumn();
        void OnOk();
        void OnLoad();
        void OnDGVDataError();
    }
}
