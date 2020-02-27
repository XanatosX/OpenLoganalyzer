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
        private string name;

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

        public bool RemoveFilterLineByName(string name)
        {
            ILogLineFilter filterToRemove = logLineTypes.Find(filter => filter.Name == name);
            if (filterToRemove != null)
            {
                return logLineTypes.Remove(filterToRemove);
            }

            return true;
        }

        public void RenameFilter(string newName)
        {
            name = newName;
        }

        public ILogLineFilter GetLogLineFilterByName(string name)
        {
            return LogLineTypes.Find((logLineType) =>
            {
                return logLineType.Name == name;
            });
        }
    }
}
