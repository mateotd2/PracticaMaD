using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao
{
    public interface IRecommendationDao : IGenericDao<Recommendation, long>
    {
        /// <summary>
        /// Finds all recommendations by group identifier.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <returns>List of recommendations from a group.</returns>
        List<Recommendation> FindRecommendationsByGroupId(long groupId);
    }
}