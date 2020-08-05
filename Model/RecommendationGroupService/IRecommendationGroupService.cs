using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.GroupUsersDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.DTO;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService
{
    public interface IRecommendationGroupService
    {
        [Inject]
        IRecommendationDao RecommendationDao { set; }

        [Inject]
        IGroupUsersDao GroupUsersDao { set; }

        [Inject]
        IUserProfileDao UsersProfileDao { set; }

        [Inject]
        ISportEventDao SportEventDao { set; }

        //GROUPS

        /// <summary>
        /// Adds the user to group.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <exception cref="UserNotFoundException"/>
        /// <exception cref="GroupNotFoundException"/>
        /// <exception cref="UserAlreadyInGroupException"/>
        /// <exception cref="InternalProblemException"/>
        [Transactional]
        void AddUserToGroup(string loginName, long groupId);

        /// <summary>
        /// Creates the group.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="description">The description.</param>
        /// <param name="loginName">The creator user identifier.</param>
        /// <returns>Id from created group.</returns>
        /// <exception cref="UserNotFoundException"/>
        [Transactional]
        long CreateGroup(string groupName, string description, string loginName);

        /// <summary>
        /// Deletes the user from group.
        /// </summary>
        /// <param name="loginName">The user login name.</param>
        /// <param name="groupId">The Group identifier.</param>
        /// <exception cref="UserNotFoundException"/>
        /// <exception cref="GroupNotFoundException"/>
        /// <exception cref="OwnerGroupAbandonException"/>
        [Transactional]
        void AbandonGroup(string loginName, long groupId);

        /// <summary>
        /// Deletes the user from group and a group if its Group's owner.
        /// </summary>
        /// <param name="loginName">The user identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <exception cref="UserNotFoundException"/>
        /// <exception cref="GroupNotFoundException"/>
        /// <exception cref="OnlyOwnerCanDeleteException"/>
        [Transactional]
        void DeleteGroup(string loginName, long groupId);

        /// <summary>
        /// Shows all groups.
        /// </summary>
        /// <returns>GroupUsers list.</returns>
        [Transactional]
        List<DTOGroups> ShowAllGroups();

        /// <summary>
        /// Shows the user groups.
        /// </summary>
        /// <param name="loginName">The user identifier.</param>
        /// <returns>a user's group list.</returns>
        /// <exception cref="UserNotFoundException"/>
        [Transactional]
        List<DTOGroupsUser> ShowUserGroups(string loginName);

        //RECOMMENDATIONS

        /// <summary>
        /// Adds the recommendation.
        /// </summary>
        /// <param name="loginName">The user identifier.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="recomendation_text">The recomendation text.</param>
        /// <returns>Recommendation Id.</returns>
        /// <exception cref="SportEventNotFoundException"/>
        /// <exception cref="GroupNotFoundException"/>
        /// <exception cref="UserNotFoundException"/>
        /// <exception cref="InternalProblemException"/>
        [Transactional]
        long AddRecommendation(string loginName, long eventId, List<long> groupsIds, string recomendation_text);

        /// <summary>
        /// Shows the groups recommendations.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <returns>List of recomendations from one group.</returns>
        /// <exception cref="GroupNotFoundException"/>
        /// <exception cref="SportEventNotFoundException"/>
        [Transactional]
        List<DTORecommendation> ShowGroupRecommendations(long groupId);

        /// <summary>
        /// Shows the user recommendations.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <returns>HashSet of DTORecommendations</returns>
        /// <exception cref="UserNotFoundException"/>
        HashSet<DTORecommendation> ShowUserRecommendations(string loginName);
    }
}