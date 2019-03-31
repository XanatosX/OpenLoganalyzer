using OpenLoganalyzerLib.Core.Interfaces.Loglines;
using System.Collections.Generic;

namespace OpenLoganalyzerLib.Core.Interfaces.Loader
{
    public interface ILoader
    {
        /// <summary>
        /// Set the configuration
        /// </summary>
        /// <param name="configuration"></param>
        void SetConfiguration(ILoaderConfiguration newConfiguration);

        /// <summary>
        /// Load the data
        /// </summary>
        /// <returns>A list with the log lines</returns>
        List<ILogLine> Load();
    }
}
