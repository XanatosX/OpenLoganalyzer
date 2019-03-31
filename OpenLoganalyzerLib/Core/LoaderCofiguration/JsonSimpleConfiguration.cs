using OpenLoganalyzerLib.Core.Enum;
using OpenLoganalyzerLib.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.LoaderCofiguration
{
    public class JsonSimpleConfiguration : IJsonLoaderConfiguration
    {
        private readonly LoaderTypeEnum loaderType;
        public LoaderTypeEnum LoaderType => loaderType;

        private readonly Dictionary<string, string> filters;
        public Dictionary<string, string> Filters => filters;

        private readonly Dictionary<string, string> additionalSettingContainer;
        public Dictionary<string, string> AdditionalSettingContainer => additionalSettingContainer;

        public JsonSimpleConfiguration()
        {
            loaderType = LoaderTypeEnum.FileLoader;
            filters = new Dictionary<string, string>();
            additionalSettingContainer = new Dictionary<string, string>();
        }

        public JsonSimpleConfiguration(LoaderTypeEnum loaderTypeEnum,
            Dictionary<string, string> newFilters,
            Dictionary<string, string> additionalSettings)
        {
            loaderType = loaderTypeEnum;
            filters = newFilters;
            additionalSettingContainer = additionalSettings;
        }

        public ILoaderConfiguration GetLoaderConfiguration()
        {
            return new SimpleConfiguration(loaderType, filters, additionalSettingContainer);
        }
    }
}
