using OpenLoganalyzerLib.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Loglines
{
    public class SimpleLogline : ILogLine
    {
        private readonly Dictionary<string, string> additionalContent;

        private readonly string thrownBy;
        public string ThrownBy => thrownBy;

        private readonly DateTime logTime;
        public DateTime LogTime => logTime;

        private readonly string message;
        public string Message => message;

        private readonly string severity;
        public string Severity => severity;

        private readonly List<string> additionalFilters;
        public List<string> AdditionalFilters => additionalFilters;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="newThrownBy">The name of the class which did throw the logline</param>
        /// <param name="newLogTime">The time the logline was throwen</param>
        /// <param name="newMessage">The message of the logline</param>
        /// <param name="newSeverity">The severity of the logline</param>
        /// <param name="newAdditionalContent">Additional content of the logline</param>
        public SimpleLogline(string newThrownBy, DateTime newLogTime, string newMessage, string newSeverity, Dictionary<string, string> newAdditionalContent)
        {
            thrownBy = newThrownBy;
            logTime = newLogTime;
            message = newMessage;
            severity = newSeverity;

            additionalContent = new Dictionary<string, string>();
            if (newAdditionalContent != null)
            {
                additionalContent = newAdditionalContent;
            }

            additionalFilters = additionalContent.Keys.ToList();
        }

        /// <summary>
        /// Get an additional content
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string GetAdditionalContent(string filter)
        {
            string returnValue = "";

            if (additionalContent.ContainsKey(filter))
            {
                returnValue = additionalContent[filter];
            }
        
            return returnValue;
        }
    }
}
