using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.Presenter.DrilldownColumns
{
    public interface IDrilldownColumnsPresenterCallbacks
    {
        void OnLoad();
        void OnAdd();
        void OnRemove();
        void OnOk();
        void OnCancel();
    }
}
