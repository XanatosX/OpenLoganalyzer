using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces.Configuration
{
    public interface IFilterManager : IFilterSaver, IFilterLoader
    {
        void Init(IFilterLoader filterLoader, IFilterSaver filterSaver);

        List<string> GetAvailableFilterNames();
    }
}
