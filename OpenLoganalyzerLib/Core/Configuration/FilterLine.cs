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

        public string Regex => regex;
        private readonly string regex;

        public string Type => type;
        private readonly string type;

        public FilterLine(string name, string regex, string type)
        {
            this.name = name;
            this.regex = regex;
            this.type = type;
        }
    }
}
