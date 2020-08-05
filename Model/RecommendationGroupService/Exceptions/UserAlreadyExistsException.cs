using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Exceptions
{
    /// <summary>
    /// Public <c>UserAlreadyExists</c> which captures the error user's loginName is not valid with
    /// the Id of the group.
    /// </summary>
    [Serializable]
    public class UserAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="loginName"><c>groupId</c> that causes the error.</param>
        public UserAlreadyExistsException(string loginName)
            : base("Incorrect loginName exception => loginName = " + loginName)
        {
            this.loginName = loginName;
        }

        /// <summary>
        /// Stores the User LoginName of the exception
        /// </summary>
        /// <value>The loginName of the User.</value>
        public string loginName { get; private set; }
    }
}