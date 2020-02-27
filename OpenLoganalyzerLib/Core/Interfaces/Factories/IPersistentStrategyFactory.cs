using OpenLoganalyzerLib.Core.Interfaces.Persistant.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces.Factories
{
    public interface IPersistentStrategyFactory
    {
        IFilterLoader GetFilterLoader();

        IFilterSaver GetFilterSaver();
    }
}
