using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Events
{
    /// <summary>
    /// This class is a base event for this library
    /// </summary>
    public class BaseEvent : EventArgs
    {
        private readonly Exception exception;
        public Exception Exception => exception;

        public BaseEvent(Exception newException)
        {
            exception = newException;
        }
    }
}
