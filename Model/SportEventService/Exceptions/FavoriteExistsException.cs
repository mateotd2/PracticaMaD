using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.SportEventService.Exceptions
{
    /// <summary>
    /// Public <c>GroupNotFoundException</c> which captures the error this event favorite already exists
    /// </summary>
    [Serializable]
    public class FavoriteExistsException : Exception
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="FavoriteExistsException"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="eventId">The event identifier.</param>
        public FavoriteExistsException(string userName, long eventId)
        : base("Favorite event already added to favorites")
        {
            this.userName = userName;
            this.eventId = eventId;
        }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string userName { get; private set; }
        /// <summary>
        /// Gets the event identifier.
        /// </summary>
        /// <value>The event identifier.</value>
        public long eventId { get; private set; }

    }
}
