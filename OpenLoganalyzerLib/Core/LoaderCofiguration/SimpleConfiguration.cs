using OpenLoganalyzerLib.Core.Enum;
using OpenLoganalyzerLib.Core.Interfaces.Loader;
using System.Collections.Generic;
using System.Linq;

namespace OpenLoganalyzerLib.Core.LoaderCofiguration
{
    public class SimpleConfiguration : ILoaderConfiguration
    {
        /// <summary>
        /// The type of the loader to use
        /// </summary>
        private readonly LoaderTypeEnum loaderType;
        public LoaderTypeEnum LoaderType => loaderType;

        /// <summary>
        /// All the filters for the logfile to check
        /// </summary>
        private readonly Dictionary<string, string> filters;
        public Dictionary<string, string> Filters => filters;

        /// <summary>
        /// The names of the filters
        /// </summary>
        private readonly List<string> filterNames;
        public List<string> FilterNames => filterNames;

        /// <summary>
        /// All the additional settings for the container
        /// </summary>
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
