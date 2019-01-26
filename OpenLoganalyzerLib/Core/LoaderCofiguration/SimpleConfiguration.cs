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

        public SimpleConfiguration(LoaderTypeEnum loaderTypeEnum, Dictionary<string, string> newFilters, Dictionary<string, string> additionalSettings)
        {
            loaderType = loaderTypeEnum;
            filters = newFilters;
            filterNames = filters.Keys.ToList();
            additionalSettingContainer = additionalSettings;
        }

        public string GetAdditionalSetting(string settingName)
        {
            string returnValue = "";

            if (additionalSettingContainer.ContainsKey(settingName))
            {
                returnValue = additionalSettingContainer[settingName];
            }

            return returnValue;
        }

        public string GetAdditionalSetting(AdditionalSettingsEnum additionalSettings)
        {
            return GetAdditionalSetting(additionalSettings.ToString());
        }
    }
}
