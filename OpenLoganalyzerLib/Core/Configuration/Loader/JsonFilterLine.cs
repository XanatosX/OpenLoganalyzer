using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Configuration.Loader
{
    internal class JsonFilterLine
    {
        public string Name;
        public List<JsonFilterColumn> FilterColumns;

        public ILogLineFilter GetLogLineFilter()
        {
            FilterLine returnValue = new FilterLine(Name);
            foreach (JsonFilterColumn column in FilterColumns)
            {
                returnValue.AddColumn(column.GetFilterColumn());
            }


            return returnValue;
        }
    }
}
