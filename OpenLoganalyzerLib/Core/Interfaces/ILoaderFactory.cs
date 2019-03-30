using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces
{
    public interface ILoaderFactory
    {
        /// <summary>
        /// This function will get you the correct loader for the configuration provided
        /// </summary>
        /// <param name="configuration">The configuration to use</param>
        /// <returns>This will return you an ILoader to load your configuration</returns>
        ILoader GetLoader(ILoaderConfiguration configuration);
    }
}
