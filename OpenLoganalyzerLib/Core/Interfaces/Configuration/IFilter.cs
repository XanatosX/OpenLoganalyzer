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

        bool AddFilter(ILogLineFilter logLineFilter);

        bool AddFilter(ILogLineFilter logLineFilter, bool overwrite);

        bool RemoveFilterLineByName(string name);


    }
}
