using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    public interface IUserProfileDao : IGenericDao<UserProfile, Int64>
    {
        /// <summary>
        /// Finds the by user by loginName
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <returns>The user profile.</returns>
        /// <exception cref="InstanceNotFoundException"/>
        UserProfile FindByLoginName(String loginName);
    }
}