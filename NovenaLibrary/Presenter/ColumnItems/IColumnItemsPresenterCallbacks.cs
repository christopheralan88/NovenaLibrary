using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.Presenter.ColumnItems
{
    public interface IColumnItemsPresenterCallbacks
    {
        void OnFind();
        void OnPrior();
        void OnNext();
        void OnAdd();
        void OnRemove();
        void OnOk();
        void OnCancel();
    }
}
