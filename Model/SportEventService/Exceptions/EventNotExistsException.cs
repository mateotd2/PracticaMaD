using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.SportEventService.Exceptions
{
    /// <summary>
    /// Public <c>EventNotExistsException</c> which captures the error Event Not Exists
    /// </summary>
    [Serializable]
    public class EventNotExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventNotExistsException"/> class.
        /// </summary>
        /// <param name="categoryName"><c>categoryId</c> that causes the error.</param>
        public EventNotExistsException(long eventId)
            : base("Incorrect eventId exception => eventId = " + eventId)
        {
            this.eventId = eventId;
        }

        /// <summary>
        /// Stores the eventId of the exception
        /// </summary>
        /// <value>The Id of the event.</value>
        public long eventId { get; private set; }
    }
}