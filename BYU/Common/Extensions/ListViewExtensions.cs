using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Common.Extensions
{
    public static class ListViewExtensions
    {
        public static void SelectAndScrollIntoView(this ListView listView, object item)
        {
            listView.SelectedItem = item;
            listView.ScrollIntoView(item);
        }
    }
}
