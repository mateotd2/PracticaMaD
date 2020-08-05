using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.FavoritesDao
{
    public class FavoritesDaoEntityFramework : GenericDaoEntityFramework<Favorite, Int64>, IFavoritesDao
    {
        public List<SportEvent> FindFavoritesEventsByUserId(long userId)
        {
            DbSet<SportEvent> SportEvent = Context.Set<SportEvent>();

            List<SportEvent> result =
                (from s in SportEvent
                 where s.Favorites.Any(u => u.fv_owner == userId)
                 select s).ToList();

            return result;
        }

        public Favorite FindFavoriteByUserIdAndEventId(long userId, long eventId)
        {
            Favorite favorite = null;
            DbSet<Favorite> favorites = Context.Set<Favorite>();

            var result =
                (from fv in favorites
                 where fv.fv_event == eventId && fv.fv_owner == userId
                 select fv);
            favorite = result.FirstOrDefault();

            //if (favorite == null)
            //{
            //    throw new InstanceNotFoundException(eventId, typeof(Category).FullName);
            //}

            return favorite;
        }
    }
}