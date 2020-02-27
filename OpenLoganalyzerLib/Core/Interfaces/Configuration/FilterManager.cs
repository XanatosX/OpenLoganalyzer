using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Configuration
{
    public class FilterManager : IFilterManager
    {
        private IFilterSaver saver;

        private IFilterLoader loader;

        public void Init(IFilterLoader filterLoader, IFilterSaver filterSaver)
        {
            loader = filterLoader;
            saver = filterSaver;
        }

        public IFilter LoadFilterByName(string name)
        {
            return loader.LoadFilterByName(name);
        }

        public bool Save(IFilter filterToSave)
        {
            return saver.Save(filterToSave);
        }

        public List<string> GetAvailableFilterNames()
        {
            return loader.GetAvailableFilterNames();
        }

        public void RemoveFilter(IFilter filterToRemove)
        {
            saver.RemoveFilter(filterToRemove);
        }
    }
}
