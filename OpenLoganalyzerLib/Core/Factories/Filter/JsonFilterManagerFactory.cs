using OpenLoganalyzerLib.Core.Configuration.Loader;
using OpenLoganalyzerLib.Core.Configuration.Saver;
using OpenLoganalyzerLib.Core.Factories.Persistant;
using OpenLoganalyzerLib.Core.Interfaces.Configuration;
using OpenLoganalyzerLib.Core.Interfaces.Factories;
using OpenLoganalyzerLib.Core.Manager;

namespace OpenLoganalyzerLib.Core.Factories.Filter
{
    public class JsonFilterManagerFactory : IFilterFactory
    {
        private readonly IPersistentStrategyFactory strategyFactory;

        public JsonFilterManagerFactory(string folderPath)
        {
            strategyFactory = new JsonPersistantStrategyFactory(folderPath);
        }

        public IFilterManager GetFilterManager()
        {
            IFilterManager returnManager = new FilterManager();
            returnManager.SetStrategy(strategyFactory);
            return returnManager;
        }
    }
}
