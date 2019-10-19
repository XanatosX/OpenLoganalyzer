using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Configuration
{
    public class FilterLine : ILogLineFilter
    {
        public string Name => name;
        private readonly string name;

        public List<IFilterColumn> FilterColumns => filterColumns;
        private List<IFilterColumn> filterColumns;

        public FilterLine(string name)
        {
            filterColumns = new List<IFilterColumn>();
            this.name = name;
        }

        public void AddColumn(IFilterColumn columnToAdd)
        {
            IFilterColumn filterExists = filterColumns.Find(filter => filter.Type == columnToAdd.Type);
            if (filterExists != null)
            {
                return;
            }

            filterColumns.Add(columnToAdd);
        }

        public bool IsValid(string logLine)
        {
            foreach (IFilterColumn column in filterColumns)
            {
                if (!column.IsValid(logLine))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
