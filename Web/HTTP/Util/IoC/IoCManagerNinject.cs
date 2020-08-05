using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.FavoritesDao;
using Es.Udc.DotNet.PracticaMaD.Model.GroupUsersDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventDao;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventService;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Util.IoC
{
    internal class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settigns;

        public void Configure()
        {
            settigns = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settigns);

            /* UserProfileDao */
            kernel.Bind<IUserProfileDao>().
                To<UserProfileDaoEntityFramework>();

            /* CategoryDao */
            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();
            /* SportEventDao */
            kernel.Bind<ISportEventDao>().
                To<SportEventDaoEntityFramework>();

            /* CommentDao */
            kernel.Bind<ICommentDao>().
                To<CommentDaoEntityFramework>();

            /* RecommendationDao */
            kernel.Bind<IRecommendationDao>().
                To<RecomendationDaoEntityFramework>();

            /* GroupUsersDao */
            kernel.Bind<IGroupUsersDao>().
                To<GroupUsersDaoEntityFramework>();

            /* SportEventService */
            kernel.Bind<ISportEventService>().
               To<SportEventService>();

            /* UserService*/
            kernel.Bind<IUserService>().
                To<UserService>();

            /* RecommendationGroupService */
            kernel.Bind<IRecommendationGroupService>().
                To<RecommendationGroupService>();

            /*TagDao*/
            kernel.Bind<ITagDao>().
            To<TagDaoEntityFramework>();

            /*FavoritesDao*/
            kernel.Bind<IFavoritesDao>().
            To<FavoritesDaoEntityFramework>();

            /* DbContext */
            string connectionString =
                ConfigurationManager.ConnectionStrings["practica_dbEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}