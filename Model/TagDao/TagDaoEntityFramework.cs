using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public class TagDaoEntityFramework : GenericDaoEntityFramework<Tag, String>, ITagDao

    {
        public int CountUses(string tagName)
        {
            DbSet<Tag> tagComments = Context.Set<Tag>();

            int result =
                (from tagComment in tagComments
                 where tagComment.tagName == tagName
                 select tagComment).Count();

            return result;
        }

        public List<String> TopListTags(int amount)
        {
            DbSet<Tag> tagComments = Context.Set<Tag>();

            List<Tag> result =
                this.GetAllElements().OrderBy(c => c.Comment.Count()).ToList().GetRange(0, amount);
            List<String> topListTagName = new List<String>();
            foreach (var topTag in result)
            {
                topListTagName.Add(topTag.tagName);
            }

            return topListTagName;
        }
    }
}