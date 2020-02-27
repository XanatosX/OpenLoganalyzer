using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System.Collections.Generic;

namespace OpenLoganalyzerLib.Core.Interfaces.Persistant.Filter
{
    public interface IFilterLoader
    {
        IFilter LoadFilterByName(string name);

        List<string> GetAvailableFilterNames();
    }
}
