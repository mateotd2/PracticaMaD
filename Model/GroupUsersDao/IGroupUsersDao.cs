using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.GroupUsersDao
{
    public interface IGroupUsersDao : IGenericDao<GroupUsers, long>
    {
        /// <summary>
        /// Obtain all the groups from a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of groups where the user is subscribed.</returns>
        List<GroupUsers> FindGroupUsersByUserId(long userId);

        ///TODO: Aqui obtengo los grupos de un usuario o los grupos que ha creado?
        ///

        bool ExistGroupName(string groupName);
    }
}