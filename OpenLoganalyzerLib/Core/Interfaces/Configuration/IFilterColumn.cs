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

        void RenameColumn(string newName);

        void Reset();

        void AddNewRegex(string regex);

        void RemoveRegex(string regex);

        void AddMultipleRegex(List<string> regexList);

        void AddMultipleRegex(string[] regexList);

        bool IsValid(string logLine);

        string GetMatchingPart(string logLine);
    }
}
