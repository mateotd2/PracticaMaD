using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.SportEventService.Exceptions
{
    /// <summary>
    /// Public <c>FavoriteNotExistsException</c> which captures the error favorite not exists.
    /// </summary>
    [Serializable]
    internal class FavoriteNotExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FavoriteExistsException"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="eventId">The event identifier.</param>
        public FavoriteNotExistsException(string userName, long eventId)
        : base("Favorite event is not in favorites.")
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