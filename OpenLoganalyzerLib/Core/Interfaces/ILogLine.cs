using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Interfaces
{
    public interface ILogLine
    {
        /// <summary>
        /// Line got thrown by
        /// </summary>
        string ThrownBy { get; }

        /// <summary>
        /// Severity of the logline
        /// </summary>
        DateTime LogTime { get; }

        /// <summary>
        /// The message of the logline
        /// </summary>
        string Message { get; }

        /// <summary>
        /// The severity of the message
        /// </summary>
        string Severity { get; }

        /// <summary>
        /// Get additional filters
        /// </summary>
        List<string> AdditionalFilters { get; }

        /// <summary>
        /// Get an additional content
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        string GetAdditionalContent(string filter);
    }
}
