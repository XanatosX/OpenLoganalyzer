using OpenLoganalyzer.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OpenLoganalyzer.Core.Extensions
{
    public static class WindowExtension
    {
        public static bool ChangeStyle(this Window currentWindow, string themeName)
        {
            currentWindow.Resources.MergedDictionaries.Clear();
            try
            {
                Uri uri = new Uri("/OpenLoganalyzer;component/Styles/" + themeName + ".xaml", UriKind.Relative);
                currentWindow.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = uri });
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
