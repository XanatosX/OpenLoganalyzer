using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces
{
    public interface ILoaderConfigurationLoader
    {
        /// <summary>
        /// The event trigger if something went wrong while loading the file
        /// </summary>
        event EventHandler LoadingError;

        /// <summary>
        /// This method will allow you to load the configuration
        /// </summary>
        /// <param name="pathToFile">The path to the file to load</param>
        /// <returns></returns>
        ILoaderConfiguration Load(string pathToFile);

        /// <summary>
        /// This class will allow you to save the configuration
        /// </summary>
        /// <param name="configuration">The configuration interface to save</param>
        /// <param name="filePath">The path to save the file to</param>
        /// <returns></returns>
        bool Save(ILoaderConfiguration configuration, string filePath);
    }
}
