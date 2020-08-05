using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, Int64>
    {
        /// <summary>
        /// Finds the comments by event identifier order by date.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>List of comments from a event.</returns>
        List<Comment> FindCommentsByEventIdOrderByDate(long eventId, int startIndex, int count);
    }
}