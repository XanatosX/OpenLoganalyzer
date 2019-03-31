using OpenLoganalyzerLib.Core.Enum;
using OpenLoganalyzerLib.Core.Interfaces.Loader;
using System.Collections.Generic;

namespace OpenLoganalyzerLib.Core.LoaderCofiguration
{
    public class JsonSimpleConfiguration : IJsonLoaderConfiguration
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
        /// All the additional settings for the container
        /// </summary>
        private readonly Dictionary<string, string> additionalSettingContainer;
        public Dictionary<string, string> AdditionalSettingContainer => additionalSettingContainer;

        /// <summary>
        /// Empty constructur for the Json config class
        /// </summary>
        public JsonSimpleConfiguration()
        {
            loaderType = LoaderTypeEnum.FileLoader;
            filters = new Dictionary<string, string>();
            additionalSettingContainer = new Dictionary<string, string>();
        }

        /// <summary>
        /// Constructor to parse the simple configuration to the json format
        /// </summary>
        /// <param name="loaderTypeEnum">The loader type to use for loading</param>
        /// <param name="newFilters">The fitlers to save</param>
        /// <param name="additionalSettings">The settings to save</param>
        public JsonSimpleConfiguration(LoaderTypeEnum loaderTypeEnum,
            Dictionary<string, string> newFilters,
            Dictionary<string, string> additionalSettings)
        {
            loaderType = loaderTypeEnum;
            filters = newFilters;
            additionalSettingContainer = additionalSettings;
        }

        /// <summary>
        /// Convert this class to a configuration class
        /// </summary>
        /// <returns>SimpleConfiguration class</returns>
        public ILoaderConfiguration GetLoaderConfiguration()
        {
            return new SimpleConfiguration(loaderType, filters, additionalSettingContainer);
        }
    }
}
