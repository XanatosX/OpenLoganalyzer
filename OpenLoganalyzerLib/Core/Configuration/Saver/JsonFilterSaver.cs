using Newtonsoft.Json;
using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using OpenLoganalyzerLib.Core.Interfaces.Persistant.Filter;
using System;
using System.IO;

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

        public void Delete(IFilter filterToRemove)
        {
            string filePath = GetFilePath(filterToRemove);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private string GetFilePath(IFilter filterToSave)
        {
            string realName = filterToSave.Name.Replace(" ", "_");
            return saveFolder + realName + ".json";
        }

        public bool Save(IFilter filterToSave)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();

            using (StreamWriter writer = new StreamWriter(GetFilePath(filterToSave)))
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
