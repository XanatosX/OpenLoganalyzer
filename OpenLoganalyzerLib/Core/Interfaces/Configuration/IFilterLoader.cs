using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces.Configuration
{
    public interface IFilterLoader
    {
        IFilter LoadFilterByName(string name);

        List<string> GetAvailableFilterNames();
    }
}
