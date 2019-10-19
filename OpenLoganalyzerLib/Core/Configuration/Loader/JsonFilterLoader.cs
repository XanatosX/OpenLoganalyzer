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
    }
}

