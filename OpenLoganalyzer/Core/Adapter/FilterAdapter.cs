using OpenLoganalyzer.Core.Interfaces.Adapter;
using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OpenLoganalyzer.Core.Adapter
{
    class FilterAdapter : IFilterAdapter
    {
        private readonly IFilter filter;
        private readonly TextBlock controlToUpdate;

        public FilterAdapter(IFilter filter, TextBlock controlToUpdate)
        {
            this.filter = filter;
            this.controlToUpdate = controlToUpdate;
        }

        public string GetName()
        {
            return filter.Name;
        }

        public void SetName(string newName)
        {
            filter.RenameFilter(newName);
            controlToUpdate.Text = newName;
        }
    }
}
