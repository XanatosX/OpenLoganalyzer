using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Interfaces
{
    public interface ILogLine
    {
        /// <summary>
        /// Line got thrown by
        /// </summary>
        string ThrowedBy { get; }

        /// <summary>
        /// Severity of the logline
        /// </summary>
        DateTime LogTime { get; }

        /// <summary>
        /// The Message of the logline
        /// </summary>
        string Message { get; }
    }
}
