using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Notification
{
    public class PopupData
    {
        private readonly string title;
        public string Title => title;

        private readonly string content;
        public string Content => content;

        private readonly int timeToShow;
        public int TimeToShow => timeToShow;

        public PopupData(string title, string content, int timeToShow)
        {
            this.title = title;
            this.content = content;
            this.timeToShow = timeToShow;
        }
    }
}
