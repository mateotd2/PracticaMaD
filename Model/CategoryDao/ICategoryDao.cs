using Es.Udc.DotNet.ModelUtil.Dao;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public interface ICategoryDao : IGenericDao<Category, Int64>
    {
        /// <summary>
        /// Finds the by user by loginName
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <returns>The user profile.</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Category FindByName(string categoryName);
    }
}