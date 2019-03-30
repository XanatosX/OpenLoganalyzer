using OpenLoganalyzerLib.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces
{
    public interface IJsonLoaderConfiguration
    {
        /// <summary>
        /// Get the base type of this configuration
        /// </summary>
        LoaderTypeEnum LoaderType { get; }

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
