using OpenLoganalyzer.Core.Interfaces.Style;
using OpenLoganalyzer.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xml;

namespace OpenLoganalyzer.Core.Style
{
    public class ThemeManager : IThemeManager
    {
        private readonly List<IStyleDict> styles;
        public List<IStyleDict> Styles => styles;

        public ThemeManager()
        {
            styles = new List<IStyleDict>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("StyleNames.txt"));

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (reader.Peek() > 0)
                    {
                        string currentLine = reader.ReadLine();
                        Uri uri = new Uri(currentLine, UriKind.Relative);
                        ResourceDictionary dict = new ResourceDictionary() { Source = uri };
                        if (dict != null)
                        {
                            string name = GetName(currentLine);
                            if (InList(name))
                            {
                                continue;
                            }
                            StyleDict style = new StyleDict(name, dict);
                            styles.Add(style);
                        }
                    }

                }
            }
        }

        private bool InList(string name)
        {
            return GetThemeByName(name) == null ? false : true;
        }

        private string GetName(string fullPath)
        {
            FileInfo info = new FileInfo(fullPath);
            string name = info.Name.Replace(info.Extension, "");
            return name;
        }

        public void ScanFolder(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                return;
            }

            string[] files = Directory.GetFiles(FolderPath);
            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);
                if (info.Extension == ".xaml")
                {
                    ResourceDictionary dict = LoadFromDisc(file);
                    if (dict != null)
                    {
                        string name = GetName(file);
                        if (InList(name))
                        {
                            continue;
                        }
                        styles.Add(new StyleDict(name, dict));
                    }

                }
            }
        }

        private ResourceDictionary LoadFromDisc(string filePath)
        {
            ResourceDictionary returnDict = null;

            FileInfo info = new FileInfo(filePath);
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

        public IStyleDict GetThemeByName(string Name)
        {
            return styles.Find(obj => obj.Name == Name);
        }
    }
}
