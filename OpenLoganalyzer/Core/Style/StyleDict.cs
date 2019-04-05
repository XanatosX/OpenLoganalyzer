using OpenLoganalyzer.Core.Enum;
using OpenLoganalyzer.Core.Interfaces.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xml;

namespace OpenLoganalyzer.Core.Style
{
    public class StyleDict : IStyleDict
    {
        private readonly string name;
        public string Name => name;

        private readonly ResourceDictionary dict;
        public ResourceDictionary Dictionary => dict;

        public StyleDict(string name, ResourceDictionary dict)
        {
            this.name = name;
            this.dict = dict;
        }
    }
}
