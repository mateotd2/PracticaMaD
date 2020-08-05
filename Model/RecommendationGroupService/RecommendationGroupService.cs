using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.GroupUsersDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.DTO;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService
{
    public class RecommendationGroupService : IRecommendationGroupService
    {
        [Inject]
        public IRecommendationDao RecommendationDao { private get; set; }

        [Inject]
        public IGroupUsersDao GroupUsersDao { private get; set; }

        [Inject]
        public IUserProfileDao UsersProfileDao { private get; set; }

        [Inject]
        public ISportEventDao SportEventDao { private get; set; }

        //GROUPS

        [Transactional]
        public void AddUserToGroup(string loginName, long groupId)
        {
            UserProfile userProfile;
            GroupUsers group;
            try { userProfile = UsersProfileDao.FindByLoginName(loginName); }
            catch (InstanceNotFoundException)
            {
                throw new UserNotFoundException(loginName);
            }
            try { group = GroupUsersDao.Find(groupId); }
            catch (InstanceNotFoundException)
            {
                throw new GroupNotFoundException(groupId);
            }
            if ((group.UsersOnGroup.Contains(userProfile)) || (userProfile.SubscribedGroups.Contains(group)))
            {
                throw new UserAlreadyInGroupException(loginName);
            }
            else
            {
                try
                {
                    group.UsersOnGroup.Add(userProfile);
                    userProfile.SubscribedGroups.Add(group);
                }
                catch (NotSupportedException e)
                {
                    throw new InternalProblemException(e);
                }
            }
            GroupUsersDao.Update(group);
            UsersProfileDao.Update(userProfile);
        }

        [Transactional]
        public long CreateGroup(string groupName, string description, string loginName)
        {
            if (GroupUsersDao.ExistGroupName(groupName))
            {
                throw new GroupAlreadyExistsException(groupName);
            }

            UserProfile userProfile;
            GroupUsers group;
            try { userProfile = UsersProfileDao.FindByLoginName(loginName); }
            catch (InstanceNotFoundException)
            {
                throw new UserNotFoundException(loginName);
            }

            group = new GroupUsers
            {
                gr_description = description,
                gr_name = groupName,
                gr_owner = userProfile.userId
            };

            GroupUsersDao.Create(group);

            AddUserToGroup(loginName, group.group_usersId);

            return group.group_usersId;
        }

        [Transactional]
        public void AbandonGroup(string loginName, long groupId)
        {
            UserProfile user;
            GroupUsers group;
            try { user = UsersProfileDao.FindByLoginName(loginName); }
            catch (InstanceNotFoundException)
            {
                throw new UserNotFoundException(loginName);
            }
            try { group = GroupUsersDao.Find(groupId); }
            catch (InstanceNotFoundException)
            {
                throw new GroupNotFoundException(groupId);
            }

            if (user.userId == group.gr_owner)
            {
                throw new OwnerGroupAbandonException(groupId, loginName);
            }
            else
            {
                user.GroupUsers.Remove(group);
                group.UsersOnGroup.Remove(user);

                UsersProfileDao.Update(user);
                GroupUsersDao.Update(group);
            }
        }

        /// <summary>Deletes the user from group and a group if its Group's owner.</summary>
        /// <param name="loginName">The user identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <exception cref="UserNotFoundException"/>
        /// <exception cref="GroupNotFoundException"/>
        /// <exception cref="OnlyOwnerCanDeleteException"/>
        [Transactional]
        public void DeleteGroup(string loginName, long groupId)
        {
            UserProfile user;
            GroupUsers group;
            try { user = UsersProfileDao.FindByLoginName(loginName); }
            catch (InstanceNotFoundException)
            {
                throw new UserNotFoundException(loginName);
            }
            try { group = GroupUsersDao.Find(groupId); }
            catch (InstanceNotFoundException)
            {
                throw new GroupNotFoundException(groupId);
            }

            if (user.userId == group.gr_owner)
            {
                GroupUsersDao.Remove(group.group_usersId);
            }
            else
            {
                throw new OnlyOwnerCanDeleteException(user.userId);
            }
        }

        [Transactional]
        public List<DTOGroups> ShowAllGroups()
        {
            List<GroupUsers> groups = GroupUsersDao.GetAllElements();

            List<DTOGroups> dtoGroups = new List<DTOGroups>();

            foreach (var group in groups)
            {
                UserProfile user = UsersProfileDao.Find(group.gr_owner);
                dtoGroups.Add(new DTOGroups(group.group_usersId, group.gr_name, group.gr_description, group.UsersOnGroup.Count, group.Recommendation.Count));
            }

            return dtoGroups;
        }

        public List<DTOGroupsUser> ShowUserGroups(string loginName)
        {
            UserProfile user;
            try { user = UsersProfileDao.FindByLoginName(loginName); }
            catch (InstanceNotFoundException)
            {
                throw new UserNotFoundException(loginName);
            }
            List<GroupUsers> grupos = GroupUsersDao.FindGroupUsersByUserId(user.userId);
            List<DTOGroupsUser> dtoGroups = new List<DTOGroupsUser>();
            foreach (var group in grupos)
            {
                UserProfile userOwner = UsersProfileDao.Find(group.gr_owner);
                dtoGroups.Add(new DTOGroupsUser(group.group_usersId, group.gr_name));
            }

            return dtoGroups;

            //Decolver mejor lista de dtos.
        }

        //RECOMMENDATIONS

        [Transactional]
        public long AddRecommendation(string loginName, long eventId, List<long> groupsIds, string recomendation_text)
        {
            UserProfile user;

            GroupUsers group;
            SportEvent sportEvent;

            try { user = UsersProfileDao.FindByLoginName(loginName); }
            catch (InstanceNotFoundException)
            {
                throw new UserNotFoundException(loginName);
            }

            try { sportEvent = SportEventDao.Find(eventId); }
            catch (InstanceNotFoundException)
            {
                throw new SportEventNotFoundException(eventId);
            }

            Recommendation recommendation = new Recommendation
            {
                eventId = eventId,
                userId = user.userId,
                publishDate = DateTime.UtcNow,
                recommendation_text = recomendation_text,
            };

            foreach (long groupId in groupsIds)
            {
                try { group = GroupUsersDao.Find(groupId); }
                catch (InstanceNotFoundException)
                {
                    throw new GroupNotFoundException(groupId);
                }

                recommendation.GroupUsers.Add(group);
                group.Recommendation.Add(recommendation);
                GroupUsersDao.Update(group);
                RecommendationDao.Update(recommendation);
            }

            return recommendation.recommendationId;
        }

        public List<DTORecommendation> ShowGroupRecommendations(long groupId)
        {
            GroupUsers group = new GroupUsers();
            string eventName;

            try { group = GroupUsersDao.Find(groupId); }
            catch (InstanceNotFoundException)
            {
                throw new GroupNotFoundException(groupId);
            }
            List<DTORecommendation> dtoRecommendations = new List<DTORecommendation>();

            try
            {
                List<Recommendation> recomendations = RecommendationDao.FindRecommendationsByGroupId(groupId);

                foreach (var recommedation in recomendations)
                {
                    try { eventName = recommedation.SportEvent.ev_name; }
                    catch (InstanceNotFoundException)
                    {
                        throw new SportEventNotFoundException(groupId);
                    }
                    UserProfile user = UsersProfileDao.Find(recommedation.userId);
                    dtoRecommendations.Add(new DTORecommendation(recommedation.recommendationId, eventName, user.loginName,
                        recommedation.eventId, recommedation.recommendation_text));
                }
            }
            catch (InstanceNotFoundException)
            {
                throw new GroupNotFoundException(groupId);
            }
            return dtoRecommendations;
        }

        public HashSet<DTORecommendation> ShowUserRecommendations(string loginName)
        {
            UserProfile userProfile;
            try { userProfile = UsersProfileDao.FindByLoginName(loginName); }
            catch (InstanceNotFoundException)
            {
                throw new UserNotFoundException(loginName);
            }
            List<DTOGroupsUser> groupUsers = this.ShowUserGroups(loginName);

            HashSet<DTORecommendation> recommendations = new HashSet<DTORecommendation>(new DTORecommendationComparer());

            List<DTORecommendation> localRecommendations = new List<DTORecommendation>();

            foreach (DTOGroupsUser group in groupUsers)
            {
                localRecommendations = ShowGroupRecommendations(group.group_usersId);
                recommendations.UnionWith(localRecommendations);
            }
            return recommendations;
        }
    }
}