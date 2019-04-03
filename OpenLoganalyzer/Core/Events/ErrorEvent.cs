using OpenLoganalyzer.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzer.Core.Events
{
    public class ErrorEvent : EventArgs
    {
        private readonly string additionalMessage;
        public string AdditionalMessage => additionalMessage;

        private readonly Exception exception;
        public Exception Exception => exception;

        private readonly int exitCode;
        public int ExitCode => exitCode;

        private readonly Severity severity;
        public Severity Severity => severity;

        private readonly DateTime dateTime;
        public DateTime DateTime => dateTime;


        public ErrorEvent(Exception exception, Severity severity, int exitCode)
        {
            this.exception = exception;
            this.severity = Severity;
            this.exitCode = exitCode;

            dateTime = DateTime.Now;
            additionalMessage = "";
        }

        public ErrorEvent(Exception exception, string additionalMessage, Severity severity, int exitCode)
        {
            this.exception = exception;
            this.additionalMessage = additionalMessage;
            this.severity = Severity;
            this.exitCode = exitCode;

            dateTime = DateTime.Now;
        }

    }
}
