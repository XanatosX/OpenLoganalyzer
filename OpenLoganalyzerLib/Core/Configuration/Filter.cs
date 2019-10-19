using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Configuration
{
    public class Filter : IFilter
    {

        public string Name => name;
        private readonly string name;

        public List<ILogLineFilter> LogLineTypes => logLineTypes;
        private readonly List<ILogLineFilter> logLineTypes;

        public Filter(string name)
        {
            this.name = name;
            logLineTypes = new List<ILogLineFilter>();
        }

        public bool AddFilter(ILogLineFilter logLineFilter)
        {
            return AddFilter(logLineFilter, false);
        }

        public bool AddFilter(ILogLineFilter logLineFilter, bool overwrite)
        {
            bool filterExists = logLineTypes.Exists(filter => filter.Name == logLineFilter.Name);
            if (overwrite || !filterExists)
            {
                logLineTypes.Add(logLineFilter);
                return true;
            }
            return false;
        }

        public bool RemoveFilterByName(string name)
        {
            ILogLineFilter filterToRemove = logLineTypes.Find(filter => filter.Name == name);
            if (filterToRemove != null)
            {
                return logLineTypes.Remove(filterToRemove);
            }

            return true;
        }


    }
}
