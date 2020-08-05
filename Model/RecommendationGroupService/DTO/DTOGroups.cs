using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.DTO
{
    public class DTOGroups
    {
        public long group_usersId { get; set; }
        public string gr_name { get; set; }
        public string gr_description { get; set; }
        public int gr_amount_users { get; set; }
        public int gr_amount_recommendation { get; set; }

        public DTOGroups(long group_usersId, string gr_name, string gr_description, int gr_amount_users, int gr_amount_recommendation)
        {
            this.group_usersId = group_usersId;
            this.gr_description = gr_description;
            this.gr_name = gr_name;
            this.gr_amount_users = gr_amount_users;
            this.gr_amount_recommendation = gr_amount_recommendation;
        }

        public override bool Equals(object obj)
        {
            return obj is DTOGroups groups &&
                   group_usersId == groups.group_usersId &&
                   gr_name == groups.gr_name &&
                   gr_description == groups.gr_description &&
                   gr_amount_users == groups.gr_amount_users &&
                   gr_amount_recommendation == groups.gr_amount_recommendation;
        }

        public override int GetHashCode()
        {
            int hashCode = 421076664;
            hashCode = hashCode * -1521134295 + group_usersId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(gr_name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(gr_description);
            hashCode = hashCode * -1521134295 + gr_amount_users.GetHashCode();
            hashCode = hashCode * -1521134295 + gr_amount_recommendation.GetHashCode();
            return hashCode;
        }
    }
}