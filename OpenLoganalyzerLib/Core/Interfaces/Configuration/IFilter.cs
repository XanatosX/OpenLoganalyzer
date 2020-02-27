using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces.Configuration
{
    public interface IFilter
    {
        string Name { get; }

        List<ILogLineFilter> LogLineTypes { get; }

        void RenameFilter(string newName);

        bool AddFilter(ILogLineFilter logLineFilter);

        bool AddFilter(ILogLineFilter logLineFilter, bool overwrite);

        ILogLineFilter GetLogLineFilterByName(string name);

        bool RemoveFilterLineByName(string name);


    }
}
