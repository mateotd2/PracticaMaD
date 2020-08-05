using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public class CategoryDaoEntityFramework :
        GenericDaoEntityFramework<Category, Int64>, ICategoryDao
    {
        public Category FindByName(string categoryName)
        {
            Category category = null;
            DbSet<Category> categories = Context.Set<Category>();

            var result =
                (from u in categories
                 where u.categoryName == categoryName
                 select u);

            category = result.FirstOrDefault();

            if (categoryName == null)
                throw new InstanceNotFoundException(categoryName,
                    typeof(Category).FullName);

            return category;
        }
    }
}