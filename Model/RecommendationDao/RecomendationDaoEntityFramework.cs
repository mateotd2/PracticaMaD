using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao
{
    public class RecomendationDaoEntityFramework : GenericDaoEntityFramework<Recommendation, long>, IRecommendationDao
    {
        public List<Recommendation> FindRecommendationsByGroupId(long groupId)
        {
            #region Using Linq.

            DbSet<Recommendation> recommendations = Context.Set<Recommendation>();
            DbSet<GroupUsers> groups = Context.Set<GroupUsers>();

            List<Recommendation> result =
                (from r in recommendations
                 where r.GroupUsers.Any(g => g.group_usersId == groupId)
                 orderby r.publishDate descending
                 select r).ToList();

            #endregion Using Linq.

            return result;
        }
    }
}