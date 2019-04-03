using Newtonsoft.Json;
using OpenLoganalyzer.Core.Events;
using OpenLoganalyzer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Settings
{
    public class SettingsManager : ISettingsManager
    {
        private readonly string filePath;
        public string FilePath => filePath;

        public event EventHandler Error;

        public SettingsManager(string filePath)
        {
            this.filePath = filePath;
        }

        public ISettings Load()
        {
            ISettings settings = null;

            if (!File.Exists(filePath))
            {
                EventHandler handler = Error;
                
                ErrorEvent error = new ErrorEvent(new FileNotFoundException(), Enum.Severity.Warning, 0);
                if (handler != null)
                {
                    handler.Invoke(this, error);
                }
                return settings;
            } 
            using (StreamReader reader = new StreamReader(filePath))
            {
                string data = reader.ReadToEnd();

                try
                {
                    settings = JsonConvert.DeserializeObject<Settings>(data);
                }
                catch (Exception ex)
                {
                    EventHandler handler = Error;
                    ErrorEvent error = new ErrorEvent(ex, Enum.Severity.Warning, 0); 
                    if (handler != null)
                    {
                        handler.Invoke(this, error);
                    }
                    
                }
                
            }
            
            return settings;
        }

        public bool Save(ISettings settings)
        {
            string data = JsonConvert.SerializeObject(settings);
            FileInfo fileInfo = new FileInfo(filePath);
            if (!Directory.Exists(fileInfo.DirectoryName))
            {
                Directory.CreateDirectory(fileInfo.DirectoryName);
            }

            File.WriteAllText(filePath, data);

            return true;
        }
    }
}
