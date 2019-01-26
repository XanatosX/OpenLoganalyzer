﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces
{
    public interface ILoaderConfigurationLoader
    {
        event EventHandler LoadingError;

        ILoaderConfiguration Load(string pathToFile);

        bool Save(ILoaderConfiguration configuration, string filePath);
    }
}
