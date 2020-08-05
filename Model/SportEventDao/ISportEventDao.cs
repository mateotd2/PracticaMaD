using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.SportEventDao
{
    public interface ISportEventDao : IGenericDao<SportEvent, Int64>
    {
        /// <summary>
        /// Finds the by keywords and category(if is not null)
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="category">The category (can be null).</param>
        /// <returns>List of Events</returns>
        List<SportEvent> FindByKeywords(string keywords, int startIndex, int count, long category = 0);
    }
}