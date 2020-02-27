using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces.Data
{
    public interface ILoader
    {
        /// <summary>
        /// This method is used to initialize the loader and get him the needed data
        /// </summary>
        /// <param name="loaderInformation">The data needed for loading</param>
        void Init(string loaderInformation);

        /// <summary>
        /// This method will load the file content 
        /// </summary>
        /// <returns>Will return a list of strings</returns>
        IEnumerable<string> Load();
    }
}
