using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Presenter.SqlCreator;
using System.ComponentModel;
using NovenaLibrary.Config;
using System.Data;

namespace NovenaLibrary.View.SqlCreator
{
    public interface ISqlCreatorView : IView<ISqlCreatorPresenterCallbacks>
    {
        BindingList<string> AvailableTables { get; set; }
        string AvailableTablesText { get; set; }
        BindingList<string> AvailableColumns { get; set; }
        BindingList<string> SelectedColumns { get; set; }
        BindingList<string> HighlightedAvailableColumns { get; }
        AppConfig AppConfig { get; set; }
        WorkbookPropertiesConfig WorkbookPropertiesConfig { get; set; }
        int HighlightedSelectedColumnIndex { get; }
        string HighlightedSelectedColumn { get; }
        BindingList<string> AvailableColumnDGV { get; set; }
        BindingList<Criteria> Criteria { get; set; }
        int? HighlightedCriteriaIndex { get; }
        DataTable SQLResult { get; set; } // make readonly
        bool GroupBy { get; }
        string Limit { get; }
        Criteria SelectedCriteria { get; }
        void CloseForm();
        // add dgv eventually, but it is a complex type
    }
}
