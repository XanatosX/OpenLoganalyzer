using OpenLoganalyzerLib.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces
{
    public interface ILoaderConfiguration
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
        /// All the filter names
        /// </summary>
        List<string> FilterNames { get; }

        /// <summary>
        /// Get an addition setting by name
        /// </summary>
        /// <param name="settingName">The name of the setting to load</param>
        /// <returns>string</returns>
        string GetAdditionalSetting(string settingName);

        /// <summary>
        /// Get an additional setting by enum
        /// </summary>
        /// <param name="additionalSettings"></param>
        /// <returns></returns>
        string GetAdditionalSetting(AdditionalSettingsEnum additionalSettings);
    }
}
