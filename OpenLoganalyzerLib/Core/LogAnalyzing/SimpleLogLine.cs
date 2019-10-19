using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using OpenLoganalyzerLib.Core.Interfaces.LogAnalyzing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.LogAnalyzing
{
    public class SimpleLogLine : ILogLine
    {
        public string Type => type;
        private string type;

        public Dictionary<string, string> FilteredLogLine => filteredLogLine;
        private readonly Dictionary<string, string> filteredLogLine;

        public SimpleLogLine(string type, Dictionary<string, string> filteredLogLine)
        {
            this.type = type;
            this.filteredLogLine = filteredLogLine;
        }

        public string GetTextForColumn(string columnName)
        {
            return filteredLogLine[columnName] ?? "";
        }
    }
}
