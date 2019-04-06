using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OpenLoganalyzer.Core.Extensions
{
    public static class StringExtension
    {
        public static string GetTranslatedString(this string target, string key)
        {
            target = Application.Current.FindResource("key") as string;
            if (target == null)
            {
                target = string.Empty;
            }

            return target;
        }
    }
}
