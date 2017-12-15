using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Presenter.DrilldownColumns;
using System.ComponentModel;
using NovenaLibrary.Config;

namespace NovenaLibrary.View.DrilldownColumns
{
    public interface IDrilldownColumnsView : IView<IDrilldownColumnsPresenterCallbacks>
    {
        BindingList<string> AvailableColumns { get; set; }
        BindingList<string> SelectedColumns { get; set; }
        BindingList<string> HighlightedAvailableColumns { get; }
        WorkbookPropertiesConfig WorkbookPropertiesConfig { get; set; }
        int HighlightedSelectedColumnIndex { get; }
        void CloseForm();
    }
}
