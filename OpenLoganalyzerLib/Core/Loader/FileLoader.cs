using OpenLoganalyzerLib.Core.Enum;
using OpenLoganalyzerLib.Core.Interfaces;
using OpenLoganalyzerLib.Core.Loglines;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Loader
{
    public class FileLoader : ILoader
    {
        private ILoaderConfiguration configuration;

        /// <summary>
        /// Set the configuration
        /// </summary>
        /// <param name="configuration">The configuration to set</param>
        public void SetConfiguration(ILoaderConfiguration newConfiguration)
        {
            configuration = newConfiguration;
        }

        /// <summary>
        /// Load the data
        /// </summary>
        /// <returns>A list with the log lines</returns>
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
                                thrownBy = getMatch(matches);
                                break;
                            case "Message":
                                message = getMatch(matches);
                                break;
                            case "Severity":
                                severity = getMatch(matches);
                                break;
                            case "Datetime":
                                string format = configuration.GetAdditionalSetting(AdditionalSettingsEnum.DateTimeFormat);
                                if (format == "")
                                {
                                    continue;
                                }

                                DateTime.TryParseExact(getMatch(matches), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out time);
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

        /// <summary>
        /// This method will get all the matches and return the string
        /// </summary>
        /// <param name="matchCollection"></param>
        /// <returns></returns>
        private string getMatch(MatchCollection matchCollection)
        {
            string returnString = "";

            Match realMatch = matchCollection[0];

            returnString = realMatch.Value.Trim();

            if (realMatch.Groups.Count == 2)
            {
                Group currentGroup = realMatch.Groups[1];
                returnString = currentGroup.Value.Trim();
            }

            return returnString;
        }
    }
}
