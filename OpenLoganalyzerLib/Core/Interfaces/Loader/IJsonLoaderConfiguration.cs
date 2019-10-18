using OpenLoganalyzerLib.Core.Enum;
using System.Collections.Generic;

namespace OpenLoganalyzerLib.Core.Interfaces.Loader
{
    public interface IJsonLoaderConfiguration
    {
        /// <summary>
        /// All the filters of this configuration
        /// </summary>
        Dictionary<string, string> Filters { get; }

        /// <summary>
        /// All the additional settings for the container
        /// </summary>
        Dictionary<string, string> AdditionalSettingContainer { get; }

        /// <summary>
        /// This function will return you the Loader configuration ready to use
        /// </summary>
        /// <returns>ILoaderConfiguration</returns>
        ILoaderConfiguration GetLoaderConfiguration();
    }
}
