using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces.Configuration
{
    public interface IFilterColumn
    {
        string Type { get; }

        List<string> PossibleRegex { get; }

        void addNewRegex(string regex);

        void addMultipleRegex(List<string> regexList);

        void addMultipleRegex(string[] regexList);

        bool IsValid(string logLine);

        string GetMatchingPart(string logLine);
    }
}
