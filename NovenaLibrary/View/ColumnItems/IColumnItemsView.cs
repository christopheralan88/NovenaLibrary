using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Presenter.ColumnItems;
using System.ComponentModel;

namespace NovenaLibrary.View.ColumnItems
{
    public interface IColumnItemsView : IView<IColumnItemsPresenterCallbacks>
    {
        void CloseForm();
        string SearchCriteria { get; }
        bool Ascending { get; }
        bool Descending { get; }
        string PageSize { get; }
        BindingList<string> AvailableItems { get; set; }
        BindingList<string> SelectedItems { get; set; }
        BindingList<string> HighlightedAvailableItems { get; }
        int HighlightedSelectedItemIndex { get; }
        bool AscendingButtonEnabled { get; set; }
        bool DescendingButtonEnabled { get; set; }
        bool FindButtonEnabled { get; set; }
        bool PriorButtonEnabled { get; set; }
        bool NextButtonEnabled { get; set; }
        string ReturnFilter { get; set; }
    }
}
