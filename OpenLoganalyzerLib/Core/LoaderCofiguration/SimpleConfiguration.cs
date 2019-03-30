using Newtonsoft.Json;
using OpenLoganalyzerLib.Core.Enum;
using OpenLoganalyzerLib.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.LoaderCofiguration
{
    public class SimpleConfiguration : ILoaderConfiguration
    {

        private readonly LoaderTypeEnum loaderType;
        public LoaderTypeEnum LoaderType => loaderType;

        private readonly Dictionary<string, string> filters;
        public Dictionary<string, string> Filters => filters;

        private readonly List<string> filterNames;

        [JsonIgnore]
        public List<string> FilterNames => filterNames;

        private readonly Dictionary<string, string> additionalSettingContainer;

        public SimpleConfiguration(LoaderTypeEnum loaderTypeEnum,
            Dictionary<string, string> newFilters,
            Dictionary<string, string> additionalSettings)
        {
            loaderType = loaderTypeEnum;
            filters = newFilters;
            filterNames = filters.Keys.ToList();
            additionalSettingContainer = additionalSettings;
        }

        /// <summary>
        /// Get an additional setting by enum
        /// </summary>
        /// <param name="additionalSettings"></param>
        /// <returns>string</returns>
        public string GetAdditionalSetting(string settingName)
        {
            string returnValue = "";

            if (additionalSettingContainer.ContainsKey(settingName))
            {
                returnValue = additionalSettingContainer[settingName];
            }

            return returnValue;
        }

        /// <summary>
        /// Get an addition setting by name
        /// </summary>
        /// <param name="settingName">The name of the setting to load</param>
        /// <returns>string</returns>
        public string GetAdditionalSetting(AdditionalSettingsEnum additionalSettings)
        {
            return GetAdditionalSetting(additionalSettings.ToString());
        }

        /// <summary>
        /// This function will return you a object which can be saved
        /// </summary>
        /// <returns></returns>
        public IJsonLoaderConfiguration GetSaveableObject()
        {
            return new JsonSimpleConfiguration(LoaderType, filters, additionalSettingContainer);
        }
    }
}
