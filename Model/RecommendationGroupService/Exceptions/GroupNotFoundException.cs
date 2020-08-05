using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Exceptions
{
    /// <summary>
    /// Public <c>GroupNotFoundException</c> which captures the error group's id is not valid with
    /// the Id of the group.
    /// </summary>
    [Serializable]
    public class GroupNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupNotFoundException"/> class.
        /// </summary>
        /// <param name="groupId"><c>groupId</c> that causes the error.</param>
        public GroupNotFoundException(long groupId)
            : base("Incorrect groupId exception => groupId = " + groupId)
        {
            this.groupId = groupId;
        }

        /// <summary>
        /// Stores the Group Id of the exception
        /// </summary>
        /// <value>The Id of the group.</value>
        public long groupId { get; private set; }
    }
}