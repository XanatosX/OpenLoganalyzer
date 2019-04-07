using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzer.Core.Interfaces.Command;
using OpenLoganalyzer.Core.Notification;
using OpenLoganalyzer.Core.Receiever;
using OpenLoganalyzer.Core.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Commands
{
    public class ShowPopupWindow : ICommand
    {
        private readonly ISettings settings;
        private readonly ThemeManager themeManager;
        private readonly PopupData popupData;

        private readonly PopupReceiever receiever;

        public ShowPopupWindow(ISettings settings, ThemeManager themeManager, PopupData popupData)
        {
            this.settings = settings;
            this.themeManager = themeManager;
            this.popupData = popupData;
            this.receiever = new PopupReceiever();
        }

        public Task<bool> AsyncExecute()
        {
            Task<bool> task = new Task<bool>(() => Execute());
            task.Start();
            return task;
        }

        public bool Execute()
        {
            receiever.ShowPopup(settings, themeManager, popupData);
            return true;
        }
    }
}
