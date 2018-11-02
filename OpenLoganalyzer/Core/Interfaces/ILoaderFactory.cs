﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Interfaces
{
    public interface ILoaderFactory
    {
        ILoader GetLoader(ILoaderConfiguration configuration);
    }
}
