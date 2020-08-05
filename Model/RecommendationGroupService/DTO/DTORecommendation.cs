using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.DTO
{
    public class DTORecommendation
    {
        public long recommendationId { get; set; }
        public long eventId { get; set; }
        public string eventName { get; set; }
        public string login_user { get; set; }

        //public System.DateTime publishDate { get; set; }
        public string recommendation_text { get; set; }

        public DTORecommendation(long recommendationId, string eventName, string login_user,
            long eventId, string recommendation_text)
        {
            this.recommendationId = recommendationId;
            this.login_user = login_user;
            this.eventName = eventName;
            this.eventId = eventId;
            this.recommendation_text = recommendation_text;
            //this.publishDate = publishDate;
        }

        public override bool Equals(object obj)
        {
            return obj is DTORecommendation recommendation &&
                   recommendationId == recommendation.recommendationId &&
                   eventId == recommendation.eventId &&
                   eventName == recommendation.eventName &&
                   login_user == recommendation.login_user &&
                   recommendation_text == recommendation.recommendation_text;
        }

        public override int GetHashCode()
        {
            int hashCode = -28314151;
            hashCode = hashCode * -1521134295 + recommendationId.GetHashCode();
            hashCode = hashCode * -1521134295 + eventId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(eventName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(login_user);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(recommendation_text);
            return hashCode;
        }
    }
}