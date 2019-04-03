﻿using OpenLoganalyzer.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OpenLoganalyzer.Core.Style
{
    public class ThemeManager
    {
        private readonly List<StyleDict> styles;
        public List<StyleDict> Styles => styles;

        public ThemeManager()
        {
            styles = new List<StyleDict>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("StyleNames.txt"));

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (reader.Peek() >0)
                    {
                        string currentLine = reader.ReadLine();
                        StyleDict style = new StyleDict(Enum.StyleEnum.buildin, currentLine);
                        styles.Add(style);
                    }
                    
                }
            }
        }

        public void ScanFolder(string FolderPath)
        {

        }

        public StyleDict GetThemeByName(string Name)
        {
            StyleDict dict = null;

            dict = styles.Find(obj => obj.Name == Name);

            return dict;
        }
    }
}
