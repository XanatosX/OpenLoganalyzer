using OpenLoganalyzer.Core.Enum;
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
            FileInfo info = new FileInfo(filePath);
            name = info.Name.Replace(info.Extension, "");
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
                    dict = LoadFromDisc();
                    break;
                default:
                    break;
            }
            return dict;
        }

        private ResourceDictionary LoadFromDisc()
        {
            ResourceDictionary returnDict = null;

            FileInfo info = new FileInfo(path);
            using (StreamReader reader = new StreamReader(info.FullName))
            {
                XmlReader xmlReader = XmlReader.Create(reader);
                try
                {
                    returnDict = (ResourceDictionary)XamlReader.Load(xmlReader);
                }
                catch (Exception)
                {
                    //@TODO some error handling!
                }
            }

            return returnDict;
        }
    }
}
