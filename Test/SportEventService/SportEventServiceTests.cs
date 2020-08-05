using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOComment;
using Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOFavorite;
using Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOSportEvent;
using Es.Udc.DotNet.PracticaMaD.Model.FavoritesDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Model.SportEventService.Tests
{
    [TestClass]
    public class SportEventServiceTest
    {
        private static IKernel kernel;

        private static ISportEventService sportEventService;

        private static IUserService userService;

        private static IFavoritesDao FavoritesDao;

        private static ICommentDao commentDao;

        private static IUserProfileDao userProfileDao;

        private static ITagDao TagDao;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_USER_ID = -1;

        private const long SPORT_EVENT = 1;

        private TransactionScope transactionScope;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        //
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            sportEventService = kernel.Get<ISportEventService>();
            FavoritesDao = kernel.Get<IFavoritesDao>();
            userService = kernel.Get<IUserService>();
            FavoritesDao = kernel.Get<IFavoritesDao>();
            commentDao = kernel.Get<ICommentDao>();
            TagDao = kernel.Get<ITagDao>();
        }

        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        [TestMethod()]
        public void AddCommentAndFindCommentsTest()
        {
            UserProfileDetails userProfileDetails = new UserProfileDetails("first name", "last name", "email@udc.es", "es", "ES");
            long userId = userService.RegisterUser("user1", "password", userProfileDetails);
            long newCommentId = sportEventService.AddComment("user1", 1, "description", null);

            DTOComment expectedComment = new DTOComment(newCommentId, "user1", 1, "description", DateTime.Now.Date, null);
            List<DTOComment> obtainedComments = sportEventService.FindComments(1, 0, 1);
            int numberOfComments = obtainedComments.Count();

            bool commentExist = commentDao.Exists(newCommentId);
            Assert.IsTrue(commentExist);
            Assert.AreEqual(1, numberOfComments);

            foreach (var comment in obtainedComments)
            {
                Assert.AreEqual(expectedComment.commentId, comment.commentId, "Comment found does not correspond with the original one.");
                Assert.AreEqual(expectedComment.comment_description, comment.comment_description, "Comment found does not correspond with the original one.");
                Assert.AreEqual(expectedComment.eventId, comment.eventId, "Comment found does not correspond with the original one.");
                Assert.AreEqual(expectedComment.loginName, comment.loginName, "Comment found does not correspond with the original one.");
            }
        }

        [TestMethod()]
        public void FindEventsTest()
        {
            //Se inicializa en base de datos un evento con nombre: "Event name"
            List<DTOSportEvent> events8 = sportEventService.FindEvents("", 0, 10, "category1").result;
            List<DTOSportEvent> events7 = sportEventService.FindEvents(null, 0, 10).result;
            List<DTOSportEvent> events = sportEventService.FindEvents("name", 0, 10).result;
            List<DTOSportEvent> events2 = sportEventService.FindEvents("Event", 0, 10).result;
            List<DTOSportEvent> events3 = sportEventService.FindEvents("event", 0, 10).result;// en minuscula
            List<DTOSportEvent> events4 = sportEventService.FindEvents(" XXX", 0, 10).result; //Ningun evento
            List<DTOSportEvent> events5 = sportEventService.FindEvents("pArtiDo", 0, 10).result;// debe devolver solo 10
            List<DTOSportEvent> events6 = sportEventService.FindEvents("pArtiDo", 0, 20).result; // devuelve 20
            DTOSportEvent expected = events.First();

            Assert.AreEqual(0, events4.Count);//Ningun evento
            Assert.AreEqual(1, events.Count);//Solo debe haber un elemento
            Assert.AreEqual(10, events5.Count);//10 eventos (maximo)
            Assert.AreEqual(16, events6.Count);
            Assert.AreEqual(0, events8.Count);
            Assert.AreEqual(0, events7.Count);
            Assert.IsTrue(expected.eventId == 1);
            Assert.AreEqual(events.First(), events2.First());
            Assert.AreEqual(events.First(), events3.First());
            Assert.IsTrue(events4.Count == 0);
            Assert.IsTrue(events.Count == 1);
            Assert.IsTrue(events5.Count == 10);
        }

        [TestMethod()]
        public void UpdateTagsTest()
        {
            UserProfileDetails userProfileDetails = new UserProfileDetails("first name", "last name", "email@udc.es", "es", "ES");
            long userId = userService.RegisterUser("user1", "password", userProfileDetails);
            long newCommentId = sportEventService.AddComment("user1", 1, "description", null);
            DTOComment expectedComment = new DTOComment(newCommentId, "user1", 1, "description", DateTime.Now.Date, null);
            List<DTOComment> obtainedComments = sportEventService.FindComments(1, 0, 1);
            int numberOfComments = obtainedComments.Count();

            bool commentExist = commentDao.Exists(newCommentId);
            Assert.IsTrue(commentExist);
            Assert.AreEqual(1, numberOfComments);

            foreach (var comment in obtainedComments)
            {
                Assert.AreEqual(expectedComment.commentId, comment.commentId, "Comment found does not correspond with the original one.");
                Assert.AreEqual(expectedComment.comment_description, comment.comment_description, "Comment found does not correspond with the original one.");
                Assert.AreEqual(expectedComment.eventId, comment.eventId, "Comment found does not correspond with the original one.");
                Assert.AreEqual(expectedComment.loginName, comment.loginName, "Comment found does not correspond with the original one.");
            }

            //Prueba de actualizacion de tags
            List<String> tagsPrueba = new List<string>();
            tagsPrueba.Add("tag1");
            tagsPrueba.Add("tag2");

            sportEventService.UpdateTagComment("user1", newCommentId, tagsPrueba);

            List<DTOComment> listOfCommentsFromEvent = sportEventService.FindComments(1, 0, 1);
            DTOComment obtainedComment = listOfCommentsFromEvent.First();
            List<string> obtainedTagsFromComment = obtainedComment.tags;
            foreach (var tag in tagsPrueba)
            {
                Assert.IsTrue(obtainedTagsFromComment.Contains(tag));
            }
        }

        [TestMethod()]
        public void GetCategoriesTest()
        {
            List<string> categories = sportEventService.GetCategories();

            Assert.IsTrue(categories.Contains("category1"));
            Assert.IsTrue(categories.Contains("category2"));
        }

        [TestMethod()]
        public void GetAndUpdateCommentTest()
        {
            string oldComment = "comentario que ya estaba";
            string newComment = "nuevo comentario cambiado";

            UserProfileDetails userProfileDetails = new UserProfileDetails("first name", "last name", "email@udc.es", "es", "ES");
            long userId = userService.RegisterUser("user1", "password", userProfileDetails);
            long commentId = sportEventService.AddComment("user1", 1, oldComment, null);

            DTOComment comment = sportEventService.getComment(commentId);

            Assert.IsTrue(oldComment.Equals(comment.comment_description));

            sportEventService.UpdateComment(commentId, newComment);

            comment = sportEventService.getComment(commentId);

            Assert.IsTrue(newComment.Equals(comment.comment_description));
        }

        [TestMethod()]
        public void FavoritesTest()
        {
            UserProfileDetails userProfileDetails = new UserProfileDetails("first name", "last name", "email@udc.es", "es", "ES");
            long userId = userService.RegisterUser("user1", "password", userProfileDetails);

            List<DTOSportEvent> events = sportEventService.FindEvents("Event", 0, 10).result;
            DTOSportEvent evento = null;
            foreach (var ev in events)
            {
                evento = ev;
            }

            sportEventService.AddToFavorites("user1", evento.eventId, "name1", "comment1");
            List<DTOFavorite> favoritos = sportEventService.ListFavorites("user1");
            Assert.IsTrue(favoritos.Count == 1);

            sportEventService.DeleteFromFavorites("user1", evento.eventId);
            favoritos = sportEventService.ListFavorites("user1");
            Assert.IsTrue(favoritos.Count == 0);

            List<DTOSportEvent> diezEventos = sportEventService.FindEvents("pArtiDo", 0, 10).result;// debe devolver 2 eventos
            foreach (var ev in diezEventos)
            {
                sportEventService.AddToFavorites("user1", ev.eventId, "name2", "comment2");
            }

            favoritos = sportEventService.ListFavorites("user1");
            int numeroEventos = 10;
            Assert.IsTrue(favoritos.Count == numeroEventos);

            foreach (var ev in diezEventos)
            {
                sportEventService.DeleteFromFavorites("user1", ev.eventId);
                favoritos = sportEventService.ListFavorites("user1");
                numeroEventos--;
                Assert.IsTrue(favoritos.Count == numeroEventos);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void AddDuplicatedFavoriteTest()
        {
            UserProfileDetails userProfileDetails = new UserProfileDetails("first name", "last name", "email@udc.es", "es", "ES");
            long userId = userService.RegisterUser("user1", "password", userProfileDetails);

            List<DTOSportEvent> events = sportEventService.FindEvents("Event", 0, 10).result;
            DTOSportEvent evento = null;
            foreach (var ev in events)
            {
                evento = ev;
            }

            sportEventService.AddToFavorites("user1", evento.eventId, "name1", "comment1");
            sportEventService.AddToFavorites("user1", evento.eventId, "name1", "comment1");
            List<DTOFavorite> favoritos = sportEventService.ListFavorites("user1");
            Assert.IsTrue(favoritos.Count == 1);
            UserProfile user = userProfileDao.FindByLoginName("user1");

            sportEventService.DeleteFromFavorites("user1", evento.eventId);
            favoritos = sportEventService.ListFavorites("user1");
            Assert.IsTrue(favoritos.Count == 0);

            List<DTOSportEvent> diezEventos = sportEventService.FindEvents("pArtiDo", 0, 10).result;// debe devolver 2 eventos
            foreach (var ev in diezEventos)
            {
                sportEventService.AddToFavorites("user1", ev.eventId, "name2", "comment2");
            }

            favoritos = sportEventService.ListFavorites("user1");
            int numeroEventos = 10;
            Assert.IsTrue(favoritos.Count == numeroEventos);

            foreach (var ev in diezEventos)
            {
                sportEventService.DeleteFromFavorites("user1", ev.eventId);
                favoritos = sportEventService.ListFavorites("user1");
                numeroEventos--;
                Assert.IsTrue(favoritos.Count == numeroEventos);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(UserNotFoundException))]
        public void ListFavoritesIncorrectUserLoginTest()
        {
            UserProfileDetails userProfileDetails = new UserProfileDetails("first name", "last name", "email@udc.es", "es", "ES");
            long userId = userService.RegisterUser("userLogin1", "password", userProfileDetails);

            List<DTOSportEvent> events = sportEventService.FindEvents("Event", 0, 10).result;
            DTOSportEvent evento = null;
            foreach (var ev in events)
            {
                evento = ev;
            }

            sportEventService.AddToFavorites("userLogin1", evento.eventId, "name1", "comment1");
            List<DTOFavorite> favoritos = sportEventService.ListFavorites("incorrectUserLogin");
        }
    }
}