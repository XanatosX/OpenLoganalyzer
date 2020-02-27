using OpenLoganalyzer.Core.Interfaces.Adapter;
using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using OpenLoganalyzerLib.Core.Interfaces.LogAnalyzing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OpenLoganalyzer.Core.Adapter
{
    class LogLineAdapter : IFilterAdapter
    {
        private readonly ILogLineFilter logLineFilter;
        private readonly TextBlock textBlock;

        public LogLineAdapter(ILogLineFilter logLine, TextBlock textBlock)
        {
            this.logLineFilter = logLine;
            this.textBlock = textBlock;
        }

        public string GetName()
        {
            return logLineFilter.Name;
        }

        public void SetName(string newName)
        {
            logLineFilter.RenameColumn(newName);
            textBlock.Text = newName;
        }
    }
}
