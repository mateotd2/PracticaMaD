using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Exceptions
{
    /// <summary>
    /// Public <c>SportEventNotFoud</c> which captures the error sportEvent's id is not valid with
    /// the Id of the sportEvent.
    /// </summary>
    [Serializable]
    public class SportEventNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SportEventNotFoundException"/> class.
        /// </summary>
        /// <param name="eventId"><c>groupId</c> that causes the error.</param>
        public SportEventNotFoundException(long eventId)
            : base("Incorrect eventId exception => eventId = " + eventId)
        {
            this.eventId = eventId;
        }

        /// <summary>
        /// Stores the sportEvent id of the exception
        /// </summary>
        /// <value>The Id of the SportEvent.</value>
        public long eventId { get; private set; }
    }
}