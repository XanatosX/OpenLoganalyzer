using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using OpenLoganalyzerLib.Core.Interfaces.LogAnalyzing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.LogAnalyzing
{
    public class LogManager : ILogManager
    {
        private IFilter filter;

        public void Init(IFilter filterToUse)
        {
            filter = filterToUse;
        }

        public ILogLine GetLogLine(string logLine)
        {
            foreach (ILogLineFilter logLineFilter in filter.LogLineTypes)
            {
                if (logLineFilter.IsValid(logLine))
                {
                    Dictionary<string, string> lineDict = new Dictionary<string, string>();
                    foreach (IFilterColumn column in logLineFilter.FilterColumns)
                    {
                        if (column.IsValid(logLine))
                        {
                            lineDict.Add(column.Type, column.GetMatchingPart(logLine));
                        }
                    }
                    return new SimpleLogLine(logLineFilter.Name, lineDict);
                }
            }

            return default;
        }

        public IEnumerable<ILogLine> GetLogLines(IEnumerable<string> logLines)
        {
            foreach (string logLine in logLines)
            {
                yield return GetLogLine(logLine);
            }

            yield return default;
        }


    }
}
