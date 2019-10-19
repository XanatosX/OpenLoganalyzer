using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces.LogAnalyzing
{
    public interface ILogManager
    {
        void Init(IFilter filterToUse);

        ILogLine GetLogLine(string logLine);

        Enumerable<ILogLine> GetLogLines(Enumerable<string> logLines);
    }
}
