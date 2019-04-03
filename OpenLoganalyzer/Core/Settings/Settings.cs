using OpenLoganalyzer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Settings
{
    public class Settings : ISettings
    {
        private readonly Dictionary<string, string> settings;
        public Dictionary<string, string> SettingsDict => settings;

        public Settings()
        {
            settings = new Dictionary<string, string>();
        }

        public void AddSetting(string settingName, string settingValue)
        {
            if (settings.ContainsKey(settingName))
            {
                settings[settingName] = settingValue;
                return;
            }

            settings.Add(settingName, settingValue);
        }

        public string GetSetting(string settingsName)
        {
            string returnValue = String.Empty;
            if (settings.ContainsKey(settingsName)) {
                returnValue = settings[settingsName];
            }

           return returnValue;
        }
    }
}
