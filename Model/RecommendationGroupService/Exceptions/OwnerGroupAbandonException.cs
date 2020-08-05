using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Exceptions
{
    /// <summary>
    /// Public <c>UserNotInGroup</c> which captures the error with the Id of the group. with the
    /// login of the user.
    /// </summary>
    [Serializable]
    public class OwnerGroupAbandonException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerGroupAbandonException"/> class.
        /// </summary>
        /// <param name="groupId"><c>groupId</c> that causes the error.</param>
        /// <param name="userLogin"><c>userLogin</c> that causes the error.</param>
        public OwnerGroupAbandonException(long groupId, string userLogin)
                : base(" exception => groupId = " + groupId + " userLogin = " + userLogin)
        {
            this.groupId = groupId;
            this.userLogin = userLogin;
        }

        /// <summary>
        /// Stores the Group Id of the exception
        /// </summary>
        /// <value>The Id of the group.</value>
        public long groupId { get; private set; }

        /// <summary>
        /// Stores the User login name of the exception
        /// </summary>
        /// <value>The name of the login.</value>
        public string userLogin { get; private set; }
    }
}