using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovenaLibrary.Utilities
{
    public static class Utility
    {
        public static BindingList<string> ConvertSelectedObjectCollectionToList(ListBox.SelectedObjectCollection collection)
        {
            var list = new BindingList<string>();
            foreach (var item in collection)
            {
                list.Add(item.ToString());
            }

            return list;
        }
    }
}
