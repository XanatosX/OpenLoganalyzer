using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Interfaces
{
    public interface ISettings
    {
        Dictionary<string, string> Settings { get; }

        void AddSetting(string settingName, string settingValue);

        string GetSetting(string settingsName);
    }
}
