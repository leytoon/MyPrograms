using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CookingBook.Utilities
{
    public static class CookingBookLanguageSelect
    {
        public static void ChangeLanuage<T>(string key, T WindowObject)where T : Window
        {
            WindowObject.DataContext = CookinBookDictionary.Instance.GetNames(key);
        }
    }
}
