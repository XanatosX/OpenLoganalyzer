using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Configuration.Loader
{
    public class JsonFilterLoader : IFilterLoader
    {
        private readonly string folderPath;

        public JsonFilterLoader(string folderPath)
        {
            this.folderPath = folderPath;
            if (!this.folderPath.EndsWith("\\"))
            {
                this.folderPath += "\\";
            }
        }

        public IFilter LoadFilterByName(string name)
        {
            string fileName = folderPath + name + ".json";
            if (!File.Exists(fileName))
            {
                return default;
            }

            //@TODO: Write the logic to load the filters again after saving them!!!
            return default;
        }
    }
}

