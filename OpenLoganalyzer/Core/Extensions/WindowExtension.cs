using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzer.Core.Style;
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
                currentWindow.Style = currentWindow.Resources["WindowStyle"] as System.Windows.Style;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool ChangeStyle(this Window currentWindow, ResourceDictionary resourceDictionary)
        {
            currentWindow.Resources.MergedDictionaries.Clear();
            try
            {
                currentWindow.Resources.MergedDictionaries.Add(resourceDictionary);
                currentWindow.Style = currentWindow.Resources["WindowStyle"] as System.Windows.Style;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool ChangeStyle(this Window currentWindow, ISettings settings, ThemeManager styleManager)
        {
            string theme = settings.GetSetting("theme");
            StyleDict styleToUse = null;
            if (string.IsNullOrEmpty(theme))
            {
                styleToUse = styleManager.Styles.First();
                theme = styleToUse.Name;
                settings.AddSetting("theme", styleToUse.Name);
            }
            if (styleToUse == null)
            {
                styleToUse = styleManager.GetThemeByName(theme);
            }
            if (styleToUse == null)
            {
                styleToUse = styleManager.Styles.First();
                settings.AddSetting("theme", styleToUse.Name);
            }

            return currentWindow.ChangeStyle(styleToUse.GetDictionary());
        }
    }
}
