using OpenLoganalyzerLib.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace OpenLoganalyzerLib.Core.LoaderCofiguration
{
    public class SimpleConfigurationLoader : ILoaderConfigurationLoader
    {
        public ILoaderConfiguration Load(string pathToFile)
        {
            ILoaderConfiguration returnConfiguration = null;

            if (!File.Exists(pathToFile))
            {
                return returnConfiguration;
            }

            JsonSerializer jsonSerializer = new JsonSerializer();
            try
            {
                string content = File.ReadAllText(pathToFile);
                returnConfiguration = JsonConvert.DeserializeObject<SimpleConfiguration>(content);
            }
            catch (Exception ex)
            {
                
            }
            return returnConfiguration;
        }

        public bool Save(ILoaderConfiguration configuration, string filePath)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                jsonSerializer.Serialize(writer, configuration);
            }
            return true;
        }
    }
}
