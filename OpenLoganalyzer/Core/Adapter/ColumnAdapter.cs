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
    class ColumnAdapter : IFilterAdapter
    {
        private readonly IFilterColumn filterColumn;
        private readonly TextBlock blockToUpdate;

        public ColumnAdapter(IFilterColumn filterColumn, TextBlock blockToUpdate)
        {
            this.filterColumn = filterColumn;
            this.blockToUpdate = blockToUpdate;
        }

        public string GetName()
        {
            return filterColumn.Type;
        }

        public void SetName(string newName)
        {
            filterColumn.RenameColumn(newName);
            blockToUpdate.Text = newName;
        }
    }
}
