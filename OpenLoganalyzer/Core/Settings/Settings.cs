using OpenLoganalyzer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Settings
{
    public class Settings : SettingsInterface
    {
        private readonly Dictionary<string, string> settings;
        Dictionary<string, string> SettingsInterface.Settings => settings;

        public Settings()
        {
            settings = new Dictionary<string, string>();
        }

        public void AddSetting(string settingName, string settingValue)
        {
            settings.Add(settingName, settingValue);
        }

        public string GetSetting(string settingsName)
        {
            string returnValue = String.Empty;
            if (settings.ContainsValue(settingsName)) {
                returnValue = settings[settingsName];
            }

           return returnValue;
        }
    }
}
