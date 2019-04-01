using OpenLoganalyzer.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OpenLoganalyzer.Core.Style
{
    public class StyleDict
    {
        private readonly StyleEnum styleType;
        public StyleEnum StyleType => styleType;

        private readonly string path;
        public string Path => path;

        private readonly string name;
        public string Name => name;

        public StyleDict(StyleEnum type, string filePath)
        {
            styleType = type;
            path = filePath;
            string fileName = filePath.Split('/').Last();
            name = fileName.Split('.').First();
        }

        public ResourceDictionary GetDictionary()
        {
            ResourceDictionary dict = null;
            switch (styleType)
            {
                case StyleEnum.buildin:
                    Uri uri = new Uri(path, UriKind.Relative);
                    dict = new ResourceDictionary() { Source = uri };
                    break;
                case StyleEnum.file:
                    break;
                default:
                    break;
            }
            return dict;
        }
    }
}
