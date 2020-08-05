using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.DTO
{
    public class DTOGroupsUser
    {
        public long group_usersId { get; set; }
        public string gr_name { get; set; }

        public DTOGroupsUser(long group_usersId, string gr_name)
        {
            this.group_usersId = group_usersId;
            this.gr_name = gr_name;
        }

        public override bool Equals(object obj)
        {
            return obj is DTOGroupsUser user &&
                   group_usersId == user.group_usersId &&
                   gr_name == user.gr_name;
        }

        public override int GetHashCode()
        {
            int hashCode = 608038676;
            hashCode = hashCode * -1521134295 + group_usersId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(gr_name);
            return hashCode;
        }
    }
}