using OpenLoganalyzerLib.Core.Enum;
using OpenLoganalyzerLib.Core.Interfaces.Loader;
using OpenLoganalyzerLib.Core.Loader;


namespace OpenLoganalyzerLib.Core.Factories
{
    public class LoaderFactory : ILoaderFactory
    {
        /// <summary>
        /// This function will get you the correct loader for the configuration provided
        /// </summary>
        /// <param name="configuration">The configuration to use</param>
        /// <returns>This will return you an ILoader to load your configuration</returns>
        public ILoader GetLoader(ILoaderConfiguration configuration)
        {
            ILoader returnValue = null;

            if (configuration == null)
            {
                return returnValue;
            }

            switch (configuration.LoaderType)
            {
                case LoaderTypeEnum.FileLoader:
                    //returnValue = new FileLoader();
                    break;
                default:
                    break;
            }

            //returnValue.SetConfiguration(configuration);

            return returnValue;
        }
          
    }
}
