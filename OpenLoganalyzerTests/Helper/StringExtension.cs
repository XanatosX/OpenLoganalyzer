using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerTests.Helper
{
    public static class StringExtension
    {
        
        /// <summary>
        /// 
        /// </summary>
        public static string CreateTestPath(this string fileName)
        {
            return Path.GetTempFileName();
        }
    }
}
