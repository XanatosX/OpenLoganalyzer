using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces.LogAnalyzing
{
    public interface ILogLine
    {
        Dictionary<ILogLineFilter, string> FilteredLogLine { get; }

        void Init(string logLine, IFilter filter);
    }
}
