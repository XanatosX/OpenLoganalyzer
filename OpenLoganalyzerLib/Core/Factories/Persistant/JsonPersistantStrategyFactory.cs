using OpenLoganalyzerLib.Core.Configuration.Loader;
using OpenLoganalyzerLib.Core.Configuration.Saver;
using OpenLoganalyzerLib.Core.Interfaces.Factories;
using OpenLoganalyzerLib.Core.Interfaces.Persistant.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Factories.Persistant
{
    class JsonPersistantStrategyFactory : IPersistentStrategyFactory
    {
        private readonly string folderPath;

        public JsonPersistantStrategyFactory(string folderPath)
        {
            this.folderPath = folderPath;
        }

        public IFilterLoader GetFilterLoader()
        {
            return new JsonFilterLoader(folderPath);
        }

        public IFilterSaver GetFilterSaver()
        {
            return new JsonFilterSaver(folderPath);
        }
    }
}
