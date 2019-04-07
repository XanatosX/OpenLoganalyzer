using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzer.Core.Notification;
using OpenLoganalyzer.Core.Style;
using OpenLoganalyzer.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OpenLoganalyzer.Core.Receiever
{
    public class PopupReceiever
    {
        public void ShowPopup(ISettings settings, ThemeManager themeManager, PopupData popupData)
        {
            Window popup = new PopupWindow(settings, themeManager, popupData);
            popup.Show();
        }
    }
}
