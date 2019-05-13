﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Helper
{
    public class Screen
    {
        readonly int width;
        public int Width => width;

        readonly int height;
        public int Height => height;

        public Screen()
        {
            width = (int)System.Windows.SystemParameters.WorkArea.Width;
            height = (int)System.Windows.SystemParameters.WorkArea.Height;
        }
    }
}