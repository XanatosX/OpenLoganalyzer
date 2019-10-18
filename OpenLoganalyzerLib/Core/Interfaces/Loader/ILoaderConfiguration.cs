using OpenLoganalyzerLib.Core.Enum;
using System.Collections.Generic;

namespace OpenLoganalyzerLib.Core.Interfaces.Loader
{
    public interface ILoaderConfiguration
    {
        /// <summary>
        /// All the filters of this configuration
        /// </summary>
        Dictionary<string, string> Filters { get; }

        /// <summary>
        /// All the filter names
        /// </summary>
        List<string> FilterNames { get; }

        /// <summary>
        /// This function will return you a object which can be saved
        /// </summary>
        /// <returns></returns>
        IJsonLoaderConfiguration GetSaveableObject();

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
        /// <returns>string</returns>
        string GetAdditionalSetting(AdditionalSettingsEnum additionalSettings);
    }
}
