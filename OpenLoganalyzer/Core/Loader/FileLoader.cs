using OpenLoganalyzer.Core.Enum;
using OpenLoganalyzer.Core.Interfaces;
using OpenLoganalyzer.Core.Loglines;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Loader
{
    public class FileLoader : ILoader
    {
        private ILoaderConfiguration configuration;

        public void SetConfiguration(ILoaderConfiguration newConfiguration)
        {
            configuration = newConfiguration;
        }

        public List<ILogLine> Load()
        {
            List<ILogLine> logLines = new List<ILogLine>();

            if (configuration.LoaderType != Enum.LoaderTypeEnum.FileLoader)
            {
                return logLines;
            }

            if (!File.Exists(configuration.GetAdditionalSetting(AdditionalSettingsEnum.FilePath)))
            {
                return logLines;
            }



            foreach (string line in File.ReadAllLines(configuration.GetAdditionalSetting(AdditionalSettingsEnum.FilePath)))
            {
                if (line == "")
                {
                    continue;
                }

                string thrownBy = "";
                string severity = "";
                string message = "";
                Dictionary<string, string> additionalLines = new Dictionary<string, string>();
                DateTime time = DateTime.Now;

                foreach (string filterName in configuration.FilterNames)
                {
                    string filterString = configuration.Filters[filterName];

                    MatchCollection matches = Regex.Matches(line, filterString);

                    if (matches.Count >= 1)
                    {
                        switch (filterName)
                        {
                            case "Caller":
                                thrownBy = matches[0].Value;
                                break;
                            case "Message":
                                message = matches[0].Value;
                                break;
                            case "Severity":
                                severity = matches[0].Value;
                                break;
                            case "Datetime":
                                string format = configuration.GetAdditionalSetting(AdditionalSettingsEnum.DateTimeFormat);
                                if (format == "")
                                {
                                    continue;
                                }

                                DateTime.TryParseExact(matches[0].Value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out time);
                                break;
                            default:
                                additionalLines.Add(filterName, matches[0].Value);
                                break;
                        }
                    }
                }

                ILogLine logLine = new SimpleLogline(thrownBy, time, message, severity, additionalLines);
                logLines.Add(logLine);
            }


            return logLines;
        }
    }
}
