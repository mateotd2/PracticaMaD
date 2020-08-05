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

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    public class TestManager
    {
        /// <summary>
        /// Configures and populates the Ninject kernel
        /// </summary>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel()
        {
            #region Option A : configuration via sourcecode

            //NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };

            IKernel kernel = new StandardKernel();

            kernel.Bind<IUserProfileDao>().
                To<UserProfileDaoEntityFramework>();

            kernel.Bind<ISportEventDao>().
                To<SportEventDaoEntityFramework>();

            kernel.Bind<ICommentDao>().
                To<CommentDaoEntityFramework>();

            kernel.Bind<IRecommendationDao>().
                To<RecomendationDaoEntityFramework>();

            kernel.Bind<IGroupUsersDao>().
                To<GroupUsersDaoEntityFramework>();

            kernel.Bind<ITagDao>().
                To<TagDaoEntityFramework>();

            kernel.Bind<IFavoritesDao>().
                To<FavoritesDaoEntityFramework>();

            kernel.Bind<IUserService>().
                To<UserService>();

            kernel.Bind<ISportEventService>().
                To<SportEventService>();

            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();

            kernel.Bind<IRecommendationGroupService>().
                To<RecommendationGroupService>();

            string connectionString =
                ConfigurationManager.ConnectionStrings["practica_dbEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);

            //return kernel;

            #endregion Option A : configuration via sourcecode

            #region Option B: configuration via external XML configuration file

            // The kernel should automatically load extensions at startup
            //NinjectSettings settings = new NinjectSettings() { LoadExtensions = false };
            //IKernel kernel = new StandardKernel(settings, new Ninject.Extensions.Xml.XmlExtensionModule());

            //kernel.Load("Modules/ninjectConfiguration.xml");

            #endregion Option B: configuration via external XML configuration file

            return kernel;
        }

        /// <summary>
        /// Configures the Ninject kernel from an external module file.
        /// </summary>
        /// <param name="moduleFilename">The module filename.</param>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel(string moduleFilename)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(moduleFilename);

            return kernel;
        }

        public static void ClearNInjectKernel(IKernel kernel)
        {
            kernel.Dispose();
        }
    }
}