using System.Collections.Generic;
using NovenaLibrary.Presenter.SqlCreator;
using System.ComponentModel;
using NovenaLibrary.Config;
using System.Data;
using QueryBuilder.SqlGenerators;

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
        Dictionary<string, DataTable> SQLResult { get; set; } // make readonly
        bool GroupBy { get; }
        long? Limit { get; }
        Criteria SelectedCriteria { get; set; }
        void CloseForm();
    }
}
