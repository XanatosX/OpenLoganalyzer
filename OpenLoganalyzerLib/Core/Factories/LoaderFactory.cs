using OpenLoganalyzerLib.Core.Enum;
using OpenLoganalyzerLib.Core.Interfaces;
using OpenLoganalyzerLib.Core.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Factories
{
    public class LoaderFactory : ILoaderFactory
    {
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
                    returnValue = new FileLoader();
                    break;
                default:
                    break;
            }

            returnValue.SetConfiguration(configuration);

            return returnValue;
        }
          
    }
}
