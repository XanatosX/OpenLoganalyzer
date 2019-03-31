using OpenLoganalyzerLib.Core.Events;
using System;
using Newtonsoft.Json;
using System.IO;
using OpenLoganalyzerLib.Core.Interfaces.Loader;

namespace OpenLoganalyzerLib.Core.LoaderCofiguration
{
    public class SimpleConfigurationLoader : ILoaderConfigurationLoader
    {
        /// <summary>
        /// The event trigger if something went wrong while loading the file
        /// </summary>
        public event EventHandler LoadingError;

        /// <summary>
        /// This method will allow you to load the configuration
        /// </summary>
        /// <param name="pathToFile">The path to the file to load</param>
        /// <returns></returns>
        public ILoaderConfiguration Load(string pathToFile)
        {
            ILoaderConfiguration returnConfiguration = null;

            if (!File.Exists(pathToFile))
            {
                return returnConfiguration;
            }

            try
            {
                string content = File.ReadAllText(pathToFile);
                JsonSimpleConfiguration container = JsonConvert.DeserializeObject<JsonSimpleConfiguration>(content);
                returnConfiguration = container.GetLoaderConfiguration();
            }
            catch (Exception ex)
            {
                EventHandler handler = LoadingError;
                if (handler != null)
                {
                    EventArgs loadingError = new ErrorLoadingEvent(ex);
                    handler.Invoke(this, loadingError);
                }
            }

            return returnConfiguration;
        }

        /// <summary>
        /// This method will allow you to save the configuration
        /// </summary>
        /// <param name="configuration">The configuration interface to save</param>
        /// <param name="filePath">The path to save the file to</param>
        /// <returns>Saving was successful or not</returns>
        public bool Save(ILoaderConfiguration configuration, string filePath)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                jsonSerializer.Serialize(writer, configuration.GetSaveableObject());
            }

            return true;
        }
    }
}
