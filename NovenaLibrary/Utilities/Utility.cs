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

        public static string Stringify(IList<string> list, char separator)
        {
            if (list.Count == 0) return null;
            if (list.Count == 1)
            {
                return list[0];
            }
            else
            {
                string result = "";
                foreach (var item in list)
                {
                    result += item + separator;
                }
                return result.Substring(0, result.Length - 1); //chop off trailing ","
            }
        }

        public static BindingList<string> ConvertDictKeysToBindingList(ICollection<string> collection)
        {
            var bindingList = new BindingList<string>();
            foreach (string item in collection)
            {
                bindingList.Add(item);
            }

            bindingList.OrderBy(x => x);

            return bindingList;
        }

        public static IList<string> ConvertListToDict(IDictionary<string, string> dict)
        {
            var list = new List<string>();
            foreach (KeyValuePair<string, string> pair in dict)
            {
                list.Add(pair.Key + "::" + pair.Value);
            }

            return list;
        }

    }
}
