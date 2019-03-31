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
        /// <summary>
        /// The exception which was thrown by the event
        /// </summary>
        private readonly Exception exception;
        public Exception Exception => exception;

        /// <summary>
        /// Contstuctur of the base event class
        /// </summary>
        /// <param name="newException">The new exception to add to the event</param>
        public BaseEvent(Exception newException)
        {
            exception = newException;
        }
    }
}
