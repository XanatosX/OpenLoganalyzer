using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Configuration.Loader
{
    internal class JsonFilterColumn
    {
        public string Type;

        public List<string> PossibleRegex;

        public JsonFilterColumn()
        {
            PossibleRegex = new List<string>();
        }

        public IFilterColumn GetFilterColumn()
        {
            IFilterColumn column = new FilterColumn(Type);
            column.AddMultipleRegex(PossibleRegex);

            return column;
        }
    }
}
