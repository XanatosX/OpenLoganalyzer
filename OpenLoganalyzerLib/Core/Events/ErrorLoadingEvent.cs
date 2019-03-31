using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLoganalyzerLib.Core.Events
{
    public class ErrorLoadingEvent : BaseEvent
    {
        /// <summary>
        /// Contstuctur of the Error loading event class
        /// </summary>
        /// <param name="newException">The new exception to add to the event</param>
        public ErrorLoadingEvent(Exception newException) : base(newException)
        {
        }
    }
}
