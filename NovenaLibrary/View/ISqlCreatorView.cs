using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Presenter;
using System.ComponentModel;
using NovenaLibrary.Config;

namespace NovenaLibrary.View
{
    public interface ISqlCreatorView : IView<ISqlCreatorPresenterCallbacks>
    {
        BindingList<string> AvailableTables { get; set; }
        string AvailableTablesText { get; set; }
        BindingList<string> AvailableColumns { get; set; }
        BindingList<string> SelectedColumns { get; set; }
        BindingList<string> HighlightedAvailableColumns { get; }
        AppConfig AppConfig { get; }
        WorkbookPropertiesConfig WorkbookPropertiesConfig { get; }
        // add dgv eventually, but it is a complex type
    }
}
