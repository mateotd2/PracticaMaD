using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Exceptions
{
    /// <summary>
    /// Public <c>OnlyOwnerCanDelete</c> which captures the error group's id is not valid with the
    /// Id of the group.
    /// </summary>
    [Serializable]
    public class OnlyOwnerCanDeleteException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnlyOwnerCanDeleteException"/> class.
        /// </summary>
        /// <param name="userId"><c>userId</c> that causes the error.</param>
        public OnlyOwnerCanDeleteException(long userId)
            : base("Incorrect userId exception => userId = " + userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// Stores the Group Id of the exception
        /// </summary>
        /// <value>The Id of the group.</value>
        public long userId { get; private set; }
    }
}