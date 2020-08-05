using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public class CommentDaoEntityFramework :
        GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        public List<Comment> FindCommentsByEventIdOrderByDate(long eventId, int startIndex, int count)
        {
            #region Using Linq.

            DbSet<Comment> comments = Context.Set<Comment>();

            List<Comment> result =
                (from a in comments
                 where a.eventId == eventId
                 orderby a.publishDate descending
                 select a).Skip(startIndex).Take(count).ToList();

            #endregion Using Linq.

            return result;
        }
    }
}