using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Configuration.Loader
{
    internal class JsonFilterContainer
    {
        public string Name;

        public List<JsonFilterLine> LogLineTypes;

        public JsonFilterContainer()
        {
            Name = "";
            LogLineTypes = new List<JsonFilterLine>();
        }

        public virtual IFilter GetFilter()
        {
            IFilter filter = new Filter(Name);
            foreach (JsonFilterLine logLineFilter in LogLineTypes)
            {
                filter.AddFilter(logLineFilter.GetLogLineFilter());
            }
            return filter;
        }
    }
}
