using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.DTO;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService
{
    internal class DTORecommendationComparer : IEqualityComparer<DTORecommendation>
    {
        public bool Equals(DTORecommendation x, DTORecommendation y)
        {
            return x.recommendationId == y.recommendationId;
        }

        public int GetHashCode(DTORecommendation obj)
        {
            return (int)obj.recommendationId;
        }
    }
}