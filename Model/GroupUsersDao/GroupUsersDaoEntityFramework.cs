using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.GroupUsersDao
{
    public class GroupUsersDaoEntityFramework :
        GenericDaoEntityFramework<GroupUsers, long>, IGroupUsersDao
    {
        public bool ExistGroupName(string groupName)
        {
            DbSet<GroupUsers> groups = Context.Set<GroupUsers>();

            bool result =
                groups.Any(x => x.gr_name == groupName);
            return result;
        }

        public List<GroupUsers> FindGroupUsersByUserId(long userId)
        {
            #region Using Linq.

            DbSet<GroupUsers> groups = Context.Set<GroupUsers>();

            List<GroupUsers> result =
                (from g in groups
                 where g.UsersOnGroup.Any(u => u.userId == userId)
                 select g).ToList();

            #endregion Using Linq.

            return result;
        }
    }
}