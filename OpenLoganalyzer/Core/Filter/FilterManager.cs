using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzerLib.Core.Enum;
using OpenLoganalyzerLib.Core.Factories;
using OpenLoganalyzerLib.Core.Interfaces.Loader;
using OpenLoganalyzerLib.Core.LoaderCofiguration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Filter
{
    public class FilterManager
    {
        private readonly List<string> filterFiles;
        public List<string> Filters => filterFiles;

        private readonly string folder;

        private readonly ISettings settings;

        private readonly ILoaderConfigurationLoader loaderConfigurationLoader;

        public FilterManager(ISettings settings, string filterFolder)
        {
            folder = filterFolder;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            filterFiles = new List<string>();
            this.settings = settings;
            ILoaderConfigurationLoader loaderConfigurationLoader = new SimpleConfigurationLoader();
            loaderConfigurationLoader.LoadingError += LoaderConfigurationLoader_LoadingError;
        }

        public List<FileInfo> getAvailableFilters()
        {
            List<FileInfo> returnFilters = new List<FileInfo>();
             
            foreach (string fileName in Directory.GetFiles(folder))
            {
                
                returnFilters.Add(new FileInfo(fileName));
            }

            return returnFilters;
        }

        private void LoaderConfigurationLoader_LoadingError(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void AddFilter(string filterFile)
        {
            //loaderConfigurationLoader
        }


        public void AddFilter(ILoaderConfiguration loaderConfiguration)
        {
            ILoaderFactory factory = new LoaderFactory();
            factory.GetLoader(loaderConfiguration);
        }
    }
}
