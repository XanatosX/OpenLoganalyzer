﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces
{
    public interface ILoader
    {
        /// <summary>
        /// Set the configuration
        /// </summary>
        /// <param name="configuration"></param>
        void SetConfiguration(ILoaderConfiguration newConfiguration);

        /// <summary>
        /// Load the data
        /// </summary>
        /// <returns>A list with the log lines</returns>
        List<ILogLine> Load();
    }
}