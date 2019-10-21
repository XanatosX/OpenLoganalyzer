using Newtonsoft.Json;
using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Configuration.Saver
{
    public class JsonFilterSaver : IFilterSaver
    {
        private readonly string saveFolder;

        public JsonFilterSaver(string folderPath)
        {
            saveFolder = folderPath;
            if (!saveFolder.EndsWith("\\"))
            {
                saveFolder += "\\";
            }
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        public bool Save(IFilter filterToSave)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            string realName = filterToSave.Name.Replace(" ", "_");
            string filePath = saveFolder + realName + ".json";

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                try
                {
                    jsonSerializer.Serialize(writer, filterToSave);
                }
                catch (Exception)
                {
                    return false;
                }
                
            }

            return true;
        }
    }
}
