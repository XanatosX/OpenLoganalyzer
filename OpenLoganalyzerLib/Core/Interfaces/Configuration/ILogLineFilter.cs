using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces.Configuration
{
    public interface ILogLineFilter
    {
        string Name { get; }

        List<IFilterColumn> FilterColumns { get; }

        void AddColumn(IFilterColumn columnToAdd);

        IFilterColumn GetColumnByName(string name);

        void RemoveColumnByType(string type);

        void RenameColumn(string newName);

        bool IsValid(string logLine);
    }
}
