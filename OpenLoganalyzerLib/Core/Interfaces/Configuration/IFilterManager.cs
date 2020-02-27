using OpenLoganalyzerLib.Core.Interfaces.Factories;
using OpenLoganalyzerLib.Core.Interfaces.Persistant.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces.Configuration
{
    public interface IFilterManager
    {
        /// <summary>
        /// Set the strategy of the filter manager
        /// </summary>
        /// <param name="persistentStrategyFactory">The factory to use for creating getting the strategy</param>
        void SetStrategy(IPersistentStrategyFactory persistentStrategyFactory);

        /// <summary>
        /// Set the strategy of the filter manager
        /// </summary>
        /// <param name="filterLoader">The loader to use</param>
        /// <param name="filterSaver">The saver to use</param>
        void SetStrategy(IFilterLoader filterLoader, IFilterSaver filterSaver);

        bool SaveFilter(IFilter filterToSave);

        void DeleteFilter(IFilter filterToRemove);
        
        IFilter GetFilter(string name);

        List<string> GetAllFilterNames();

        List<IFilter> GetAllFilters();
    }
}
