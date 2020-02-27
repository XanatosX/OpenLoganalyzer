using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using OpenLoganalyzerLib.Core.Interfaces.Factories;
using OpenLoganalyzerLib.Core.Interfaces.Persistant.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Manager
{
    public class FilterManager : IFilterManager
    {
        private IFilterSaver saver;

        private IFilterLoader loader;

        public void SetStrategy(IPersistentStrategyFactory persistentStrategyFactory)
        {
            SetStrategy(
                persistentStrategyFactory.GetFilterLoader(),
                persistentStrategyFactory.GetFilterSaver()
            );
        }

        public void SetStrategy(IFilterLoader filterLoader, IFilterSaver filterSaver)
        {
            loader = filterLoader;
            saver = filterSaver;
        }

        public bool SaveFilter(IFilter filterToSave)
        {
            if (saver == null)
            {
                return false;
            }
            return saver.Save(filterToSave);
        }

        public void DeleteFilter(IFilter filterToRemove)
        {
            if (saver == null)
            {
                return;
            }
            saver.Delete(filterToRemove);
        }

        public IFilter GetFilter(string name)
        {
            if (loader == null)
            {
                return default;
            }
            return loader.LoadFilterByName(name);
        }

        public List<string> GetAllFilterNames()
        {
            if (loader == null)
            {
                return default;
            }
            return loader.GetAvailableFilterNames();
        }

        public List<IFilter> GetAllFilters()
        {
            List<IFilter> returnList = new List<IFilter>();
            foreach (string filterName in GetAllFilterNames())
            {
                IFilter filterToAdd = GetFilter(filterName);
                if (filterToAdd == null)
                {
                    continue;
                }
                returnList.Add(filterToAdd);
            }

            return returnList;
        }
    }
}
