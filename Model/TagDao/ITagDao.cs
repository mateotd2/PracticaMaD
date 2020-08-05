using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public interface ITagDao : IGenericDao<Tag, String>
    {
        /// <summary>
        /// Counts the uses. Usefull for the cloud Tags.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <returns>Count of uses of one tag.</returns>
        int CountUses(String tagName);

        List<String> TopListTags(int amount);
    }
}