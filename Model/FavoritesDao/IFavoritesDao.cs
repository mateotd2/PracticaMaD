using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.FavoritesDao
{
    public interface IFavoritesDao : IGenericDao<Favorite, Int64>

    {
        /// <summary>
        /// Finds the favorites by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of favorites sports events of user.</returns>
        List<SportEvent> FindFavoritesEventsByUserId(long userId);

        Favorite FindFavoriteByUserIdAndEventId(long userId, long eventId);
    }
}