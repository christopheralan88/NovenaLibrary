using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Presenter;

namespace NovenaLibrary.View
{
    public interface ISqlCreatorView : IView<ISqlCreatorPresenterCallbacks>
    {
        List<string> AvailableTables { get; set; }
        string AvailableTablesText { get; set; }
        List<string> AvailableColumns { get; set; }
        List<string> SelectedColumns { get; set; }
        // add dgv eventuall, but it is a complex type
    }
}
