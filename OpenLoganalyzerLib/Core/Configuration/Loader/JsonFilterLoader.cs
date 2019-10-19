using Newtonsoft.Json;
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
            IFilter returnValue = default;
            string fileName = folderPath + name + ".json";
            fileName = fileName.Replace(" ", "_");
            if (!File.Exists(fileName))
            {
                return returnValue;
            }
            JsonSerializer jsonSerializer = new JsonSerializer();
            using (StreamReader reader = new StreamReader(fileName))
            {
                try
                {
                    JsonFilterContainer jsonFilterContainer = (JsonFilterContainer)jsonSerializer.Deserialize(reader, typeof(JsonFilterContainer));
                    returnValue = jsonFilterContainer.GetFilter();
                }
                catch (Exception)
                {
                    return returnValue;
                }
            }

            return returnValue;
        }

        public List<string> GetAvailableFilterNames()
        {
            List<string> returnFiterNames = new List<string>();
            if (!Directory.Exists(folderPath))
            {
                return returnFiterNames;
            }
            List<string> fileNames = Directory.GetFiles(folderPath).ToList<string>();

            fileNames = fileNames.FindAll(name =>
                {
                    FileInfo fi = new FileInfo(name);
                    return fi.Extension == ".json";
                }
            );
            foreach (string fileName in fileNames)
            {
                FileInfo fi = new FileInfo(fileName);
                string realName = fi.Name.Replace(fi.Extension, "");
                IFilter filter = LoadFilterByName(realName);
                if (filter != null)
                {
                    if (filter.Name != "")
                    {
                        returnFiterNames.Add(filter.Name);
                    }
                }
                
            }

            return returnFiterNames;
        }
    }
}

