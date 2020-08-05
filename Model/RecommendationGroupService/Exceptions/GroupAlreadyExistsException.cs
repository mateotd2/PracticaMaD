using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Exceptions
{
    /// <summary>
    /// Public <c>GroupAlreadyExistsException</c> which captures the error group with this name
    /// already exists the Id of the group.
    /// </summary>
    public class GroupAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="groupName"><c>groupId</c> that causes the error.</param>
        public GroupAlreadyExistsException(string groupName)
            : base("Group with that name already exist => groupName = " + groupName)
        {
            this.groupName = groupName;
        }

        /// <summary>
        /// Stores the Group name of the exception
        /// </summary>
        /// <value>The name of the group.</value>
        public string groupName { get; private set; }
    }
}