using OpenLoganalyzerLib.Core.Configuration;
using OpenLoganalyzerLib.Core.Configuration.Loader;
using OpenLoganalyzerLib.Core.Configuration.Saver;
using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Factories
{
    public class JsonFilterManagerFactory : IFilterFactory
    {
        private readonly string folderPath;

        public JsonFilterManagerFactory(string folderPath)
        {
            this.folderPath = folderPath;
        }

        public IFilterManager GetFilterManager()
        {
            IFilterManager returnManager = new FilterManager();
            returnManager.Init(new JsonFilterLoader(folderPath), new JsonFilterSaver(folderPath));
            return returnManager;
        }
    }
}
