using OpenLoganalyzerLib.Core.Interfaces.Loglines;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenLoganalyzerLib.Core.Loglines
{
    public class SimpleLogline : ILogLine
    {
        /// <summary>
        /// The variable to save the additional content in
        /// </summary>
        private readonly Dictionary<string, string> additionalContent;

        /// <summary>
        /// Line got thrown by
        /// </summary>
        private readonly string thrownBy;
        public string ThrownBy => thrownBy;

        /// <summary>
        /// Severity of the logline
        /// </summary>
        private readonly DateTime logTime;
        public DateTime LogTime => logTime;

        /// <summary>
        /// The message of the logline
        /// </summary>
        private readonly string message;
        public string Message => message;

        /// <summary>
        /// The severity of the message
        /// </summary>
        private readonly string severity;
        public string Severity => severity;

        /// <summary>
        /// Get additional filters
        /// </summary>
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
