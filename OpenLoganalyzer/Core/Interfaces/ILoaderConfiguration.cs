using OpenLoganalyzer.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Interfaces
{
    public interface ILoaderConfiguration
    {
        /// <summary>
        /// Get the base type of this configuration
        /// </summary>
        LoaderTypeEnum LoaderType { get; }

        /// <summary>
        /// The all the filters of this configuration
        /// </summary>
        Dictionary<string, string> Filters { get; }

        /// <summary>
        /// Get an addition setting by name
        /// </summary>
        /// <param name="settingName">The name of the setting to load</param>
        /// <returns>string</returns>
        string GetAdditionalSetting(string settingName);

        /// <summary>
        /// Load the current configuration
        /// </summary>
        /// <param name="pathToFile">The path to the requested file</param>
        /// <returns></returns>
        bool Load(string pathToFile);

        /// <summary>
        /// Save the current configuration
        /// </summary>
        /// <param name="pathToSaveFile">The path to the file to save into</param>
        /// <returns></returns>
        bool Save(string pathToSaveFile);
    }
}
