using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzerLib.Core.Enum;
using OpenLoganalyzerLib.Core.Factories;
using OpenLoganalyzerLib.Core.Interfaces.Loader;
using OpenLoganalyzerLib.Core.LoaderCofiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core
{
    public class FilterManager
    {
        private readonly List<string> filterFiles;
        public List<string> Filters => filterFiles;

        private readonly SettingsInterface settings;

        private readonly ILoaderConfigurationLoader loaderConfigurationLoader;

        public FilterManager(SettingsInterface settingsManager)
        {
            filterFiles = new List<string>();
            settings = settingsManager;
            ILoaderConfigurationLoader loaderConfigurationLoader = new SimpleConfigurationLoader();
            loaderConfigurationLoader.LoadingError += LoaderConfigurationLoader_LoadingError;
            
        }

        private void LoaderConfigurationLoader_LoadingError(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void AddFilter(string filterFile)
        {
            loaderConfigurationLoader
        }


        public void AddFilter(ILoaderConfiguration loaderConfiguration)
        {
            ILoaderFactory factory = new LoaderFactory();
            factory.GetLoader(loaderConfiguration);
        }
    }
}
