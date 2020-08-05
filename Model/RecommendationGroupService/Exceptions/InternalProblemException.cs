using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Exceptions
{
    /// <summary>
    /// Public <c>InternalProblemException</c> which captures the error
    /// </summary>
    [Serializable]
    public class InternalProblemException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalProblemException"/> class.
        /// </summary>
        /// <param name="exception"><c>exception</c> that causes the error.</param>
        public InternalProblemException(Exception exception)
            : base("Incorrect groupId exception => groupId = " + exception.Message)
        {
            this.exception = exception;
        }

        /// <summary>
        /// Stores the Group Id of the exception
        /// </summary>
        /// <value>The Id of the group.</value>
        public Exception exception { get; private set; }
    }
}