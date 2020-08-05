using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.GroupUsersDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.DTO;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventDao;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Tests
{
    [TestClass]
    public class RecommendationGroupServiceTest
    {
        private static IKernel kernel;
        private static IRecommendationGroupService recommendationGroupService;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;
        private static ISportEventDao sportEventDao;
        private static ISportEventService sportEventService;
        private static IRecommendationDao recommendationDao;
        private static IGroupUsersDao groupUsersDao;
        private static ICategoryDao categoryDao;

        private TransactionScope transactionScope;

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
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

        #region Atributos de prueba adicionales

        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion Atributos de prueba adicionales

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            recommendationGroupService = kernel.Get<IRecommendationGroupService>();
            userService = kernel.Get<IUserService>();
            userProfileDao = kernel.Get<IUserProfileDao>();
            sportEventDao = kernel.Get<ISportEventDao>();
            recommendationDao = kernel.Get<IRecommendationDao>();
            groupUsersDao = kernel.Get<IGroupUsersDao>();
            sportEventService = kernel.Get<ISportEventService>();
            categoryDao = kernel.Get<ICategoryDao>();
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

        /// <summary>
        /// This Test method create an user with 3 groups.
        /// test createGroup and showGroups
        /// </summary>
        [TestMethod]
        public void TestCreateGroup()
        {
            //Create user1
            UserProfileDetails user1Details = new UserProfileDetails("testUserName1", "testLastName1", "user1@login.es", "en", "US");
            long userId = userService.RegisterUser("testLogin1", "passwordtest1", user1Details);

            //User1 creates group
            long group1Id = recommendationGroupService.CreateGroup("tesNameGroup1", "testDescriptionGroup1", "testLogin1");
            List<DTOGroups> AllGroups = recommendationGroupService.ShowAllGroups();
            int countAllGroups = AllGroups.Count;
            Assert.AreEqual(countAllGroups, 1);

            //User1 creates 2 groups
            long group2Id = recommendationGroupService.CreateGroup("tesNameGroup2", "testDescriptionGroup2", "testLogin1");
            AllGroups = recommendationGroupService.ShowAllGroups();
            countAllGroups = AllGroups.Count;

            Assert.AreEqual(countAllGroups, 2);

            long group3Id = recommendationGroupService.CreateGroup("tesNameGroup3", "testDescriptionGroup3", "testLogin1");
            AllGroups = recommendationGroupService.ShowAllGroups();
            countAllGroups = AllGroups.Count;
            Assert.AreEqual(countAllGroups, 3);

            //Check group information
            DTOGroups group1 = AllGroups[0];
            DTOGroups group2 = AllGroups[1];
            DTOGroups group3 = AllGroups[2];

            Assert.AreEqual(group1.group_usersId, group1Id);
            Assert.AreEqual(group2.group_usersId, group2Id);
            Assert.AreEqual(group3.group_usersId, group3Id);

            Assert.AreEqual("testDescriptionGroup1", group1.gr_description);
            Assert.AreEqual("testDescriptionGroup2", group2.gr_description);
            Assert.AreEqual("testDescriptionGroup3", group3.gr_description);

            Assert.AreEqual(group1.gr_name, "tesNameGroup1");
            Assert.AreEqual(group2.gr_name, "tesNameGroup2");
            Assert.AreEqual(group3.gr_name, "tesNameGroup3");

            ////Check user is the group owner
            //Assert.AreEqual(group1.gr_login_owner, "testLogin1");
            //Assert.AreEqual(group2.gr_login_owner, "testLogin1");
            //Assert.AreEqual(group3.gr_login_owner, "testLogin1");

            ////Check owner is into users group list
            //ICollection<UserProfile> usersGroup1 = group1.UsersOnGroup;

            //Assert.AreEqual(usersGroup1.Count, 1);
            //foreach (var usr in usersGroup1)
            //{
            //    Assert.AreEqual(usr.userId, userId);
            //    Assert.AreEqual(usr.loginName, "testLogin1");
            //    Assert.AreEqual(usr.lastName, "testLastName1");
            //}
        }

        /// <summary>
        /// Create group with ivalid User loginName
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException),
    "A User loginName not correct")]
        public void TestCreateGroupNotValidUser()
        {
            //Create user1
            UserProfileDetails user1Details = new UserProfileDetails("testUserName1", "testLastName1", "user1@login.es", "en", "US");
            long userId = userService.RegisterUser("testLogin1", "passwordtest1", user1Details);

            //Bad user creates group
            long group1Id = recommendationGroupService.CreateGroup("tesNameGroup1", "testDescriptionGroup1", "ErrorLogin");
        }

        /// <summary>
        /// Create group user 1 and user 2,3,4 join group, user 3 abandon group
        /// </summary>
        [TestMethod]
        public void TestCreateGroupVariousUsers()
        {
            //Create user1
            UserProfileDetails user1Details = new UserProfileDetails("testUserName1", "testLastName1", "user1@login.es", "en", "US");
            long userId = userService.RegisterUser("testLogin1", "passwordtest1", user1Details);
            //Create user2
            UserProfileDetails user2Details = new UserProfileDetails("testUserName2", "testLastName2", "user2@login.es", "en", "US");
            long user2Id = userService.RegisterUser("testLogin2", "passwordtest2", user2Details);
            //Create user3
            UserProfileDetails user3Details = new UserProfileDetails("testUserName3", "testLastName3", "user3@login.es", "en", "US");
            long user3Id = userService.RegisterUser("testLogin3", "passwordtest1", user3Details);
            //Create user4
            UserProfileDetails user4Details = new UserProfileDetails("testUserName4", "testLastName4", "user4@login.es", "en", "US");
            long user4Id = userService.RegisterUser("testLogin4", "passwordtest4", user4Details);

            //User1 creates group
            long group1Id = recommendationGroupService.CreateGroup("tesNameGroup1", "testDescriptionGroup1", "testLogin1");
            List<DTOGroups> AllGroups = recommendationGroupService.ShowAllGroups();

            //All Users(4) on Group1
            recommendationGroupService.AddUserToGroup("testLogin2", group1Id);
            recommendationGroupService.AddUserToGroup("testLogin3", group1Id);
            recommendationGroupService.AddUserToGroup("testLogin4", group1Id);

            //Check 4 users on group
            AllGroups = recommendationGroupService.ShowAllGroups();
            DTOGroups groupTmp = AllGroups[0];
            //ICollection<UserProfile> usersOnGroup = groupTmp.UsersOnGroup;

            //Assert.AreEqual(usersOnGroup.Count, 4);

            //Check Users info
            //List<UserProfile> userList = new List<UserProfile>(usersOnGroup);
            //UserProfile user1 = userList[0];
            //UserProfile user2 = userList[1];
            //UserProfile user3 = userList[2];
            //UserProfile user4 = userList[3];

            //Assert.AreEqual(user1.userId, userId);
            //Assert.AreEqual(user2.userId, user2Id);
            //Assert.AreEqual(user3.userId, user3Id);
            //Assert.AreEqual(user4.userId, user4Id);

            //User3 abandon Group
            recommendationGroupService.AbandonGroup("testLogin3", group1Id);

            //Check Users on group
            AllGroups = recommendationGroupService.ShowAllGroups();
            //usersOnGroup = groupTmp.UsersOnGroup;
            //Assert.AreEqual(usersOnGroup.Count, 3);

            ////Check Users info
            //userList = new List<UserProfile>(usersOnGroup);
            //user1 = userList[0];
            //user2 = userList[1];
            //user4 = userList[2];

            //Assert.AreEqual(user1.userId, userId);
            //Assert.AreEqual(user2.userId, user2Id);
            //Assert.AreEqual(user4.userId, user4Id);
        }

        /// <summary>
        /// Create group user 1 and user 2,3 join group, user 1 delete group
        /// </summary>
        [TestMethod]
        public void TestOwnerDeleteGroup()
        {
            //Create user1
            UserProfileDetails user1Details = new UserProfileDetails("testUserName1", "testLastName1", "user1@login.es", "en", "US");
            long userId = userService.RegisterUser("testLogin1", "passwordtest1", user1Details);
            //Create user2
            UserProfileDetails user2Details = new UserProfileDetails("testUserName2", "testLastName2", "user2@login.es", "en", "US");
            long user2Id = userService.RegisterUser("testLogin2", "passwordtest2", user2Details);
            //Create user3
            UserProfileDetails user3Details = new UserProfileDetails("testUserName3", "testLastName3", "user3@login.es", "en", "US");
            long user3Id = userService.RegisterUser("testLogin3", "passwordtest1", user3Details);

            //User1 creates group
            long group1Id = recommendationGroupService.CreateGroup("tesNameGroup1", "testDescriptionGroup1", "testLogin1");

            // 2 User on Group1
            recommendationGroupService.AddUserToGroup("testLogin2", group1Id);
            recommendationGroupService.AddUserToGroup("testLogin3", group1Id);

            // User 1(owner) delete Group
            recommendationGroupService.DeleteGroup("testLogin1", group1Id);

            //Check Users on Group
            List<DTOGroups> groupsList = recommendationGroupService.ShowAllGroups();

            Assert.AreEqual(groupsList.Count, 0);

            List<DTOGroupsUser> groupsUser1 = recommendationGroupService.ShowUserGroups("testLogin1");
            List<DTOGroupsUser> groupsUser2 = recommendationGroupService.ShowUserGroups("testLogin2");
            List<DTOGroupsUser> groupsUser3 = recommendationGroupService.ShowUserGroups("testLogin3");

            Assert.AreEqual(groupsUser1.Count, 0);
            Assert.AreEqual(groupsUser2.Count, 0);
            Assert.AreEqual(groupsUser3.Count, 0);
        }

        /// <summary>
        /// Create group user 1 and user 2 join group, user 2 delete group
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(OnlyOwnerCanDeleteException),
    "Only groups owner can delete")]
        public void TestNotOwnerDeleteGroup()
        {
            //Create user1
            UserProfileDetails user1Details = new UserProfileDetails("testUserName1", "testLastName1", "user1@login.es", "en", "US");
            long userId = userService.RegisterUser("testLogin1", "passwordtest1", user1Details);
            //Create user2
            UserProfileDetails user2Details = new UserProfileDetails("testUserName2", "testLastName2", "user2@login.es", "en", "US");
            long user2Id = userService.RegisterUser("testLogin2", "passwordtest2", user2Details);

            //User1 creates group
            long group1Id = recommendationGroupService.CreateGroup("tesNameGroup1", "testDescriptionGroup1", "testLogin1");

            // User 2 on Group1
            recommendationGroupService.AddUserToGroup("testLogin2", group1Id);

            // User 2 ( NotOwner ) delete Group
            recommendationGroupService.DeleteGroup("testLogin2", group1Id);
        }

        /// <summary>
        /// Create group user 1 and user 2 join group, user 1 abandone group
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(OwnerGroupAbandonException),
    "Only groups owner can delete")]
        public void TestOwnerAbandonGroup()
        {
            //Create user1
            UserProfileDetails user1Details = new UserProfileDetails("testUserName1", "testLastName1", "user1@login.es", "en", "US");
            long userId = userService.RegisterUser("testLogin1", "passwordtest1", user1Details);
            //Create user2
            UserProfileDetails user2Details = new UserProfileDetails("testUserName2", "testLastName2", "user2@login.es", "en", "US");
            long user2Id = userService.RegisterUser("testLogin2", "passwordtest2", user2Details);

            //User1 creates group
            long group1Id = recommendationGroupService.CreateGroup("tesNameGroup1", "testDescriptionGroup1", "testLogin1");

            // User 2 on Group1
            recommendationGroupService.AddUserToGroup("testLogin2", group1Id);

            // User 2 ( NotOwner ) delete Group
            recommendationGroupService.AbandonGroup("testLogin1", group1Id);
        }

        /// <summary>
        /// Create group user 1 and user 2,3 join group, user 3 abandone group
        /// </summary>
        [TestMethod]
        public void TestRecommendation()
        {
            //Create user1
            UserProfileDetails user1Details = new UserProfileDetails("testUserName1", "testLastName1", "user1@login.es", "en", "US");
            long user1Id = userService.RegisterUser("testLogin1", "passwordtest1", user1Details);
            //Create user2
            UserProfileDetails user2Details = new UserProfileDetails("testUserName2", "testLastName2", "user2@login.es", "en", "US");
            long user2Id = userService.RegisterUser("testLogin2", "passwordtest2", user2Details);

            //User1 creates group
            long group1Id = recommendationGroupService.CreateGroup("tesNameGroup1", "testDescriptionGroup1", "testLogin1");
            //User2 creates group
            long group2Id = recommendationGroupService.CreateGroup("testNameGroup2", "testDescriptionGroup2", "testLogin2");

            // User 2 on Group1
            recommendationGroupService.AddUserToGroup("testLogin2", group1Id);

            //User1 on Group2
            recommendationGroupService.AddUserToGroup("testLogin1", group2Id);

            //List with only group1Id
            List<long> group1List = new List<long>();
            group1List.Add(group1Id);

            //List with only group2Id
            List<long> group2List = new List<long>();
            group2List.Add(group2Id);

            //List of groupsIds
            List<long> groupAllIds = new List<long>();
            groupAllIds.Add(group1Id);
            groupAllIds.Add(group2Id);

            //Creating Category for sport event
            //1 = eventId1, 2=eventId2

            //User1 add recomendation event1 to group1
            long idRecommendation1 = recommendationGroupService.AddRecommendation("testLogin1", 1, group1List, "my_recommendation_text1");
            List<DTORecommendation> ListRecommendation = recommendationGroupService.ShowGroupRecommendations(group1Id);
            //Check if we insert 1
            Assert.AreEqual(1, ListRecommendation.Count);
            // User2 add recommendation event2 to group2
            long idRecommendation2 = recommendationGroupService.AddRecommendation("testLogin2", 2, group2List, "my_recommendation_text2");
            ListRecommendation = recommendationGroupService.ShowGroupRecommendations(group1Id);
            //Check if we insert 1 and if is "my_recommendation_text1"
            Assert.AreEqual(1, ListRecommendation.Count);
            Assert.IsTrue(ListRecommendation[0].recommendation_text.Equals("my_recommendation_text1"));

            //Check recommendations
            List<DTORecommendation> lista = new List<DTORecommendation>(ListRecommendation);

            Assert.AreEqual(lista[0].eventId, 1);
            //List<GroupUsers> groupsInRecommendation = new List<GroupUsers>(lista[0].GroupUsers);
            //Assert.AreEqual(groupsInRecommendation[0].group_usersId, group1Id);
            Assert.AreEqual(lista[0].recommendationId, idRecommendation1);
            Assert.AreEqual(lista[0].recommendation_text, "my_recommendation_text1");
            Assert.AreEqual(lista[0].login_user, "testLogin1");

            //User1 add recomendation on groups
            long idRecommendation3 = recommendationGroupService.AddRecommendation("testLogin1", 1, groupAllIds, "recommendations on 2 groups");

            //Get and check group1
            List<DTORecommendation> ListRecommendations = recommendationGroupService.ShowGroupRecommendations(group1Id);
            Assert.AreEqual(2, ListRecommendations.Count());
            Assert.IsTrue(ListRecommendations[0].recommendation_text.Equals("recommendations on 2 groups"));

            //Get all recommendations from user

            HashSet<DTORecommendation> dTORecommendations = recommendationGroupService.ShowUserRecommendations("testLogin1");

            Assert.AreEqual(3, dTORecommendations.Count);

            //Assert.AreEqual(lista[1].eventId, 2);
            ////groupsInRecommendation = new List<GroupUsers>(lista[1].GroupUsers);
            ////Assert.AreEqual(groupsInRecommendation[0].group_usersId, group1Id);
            //Assert.AreEqual(lista[1].recommendationId, idRecommendation2);
            //Assert.AreEqual(lista[1].recommendation_text, "my_recommendation_text2");
            //Assert.AreEqual(lista[1].login_user, "testLogin2");
        }

        //    public void TestUserRecommend()
        //    {
        //        //Create user1
        //        UserProfileDetails user1Details = new UserProfileDetails("testUserName1", "testLastName1", "user1@login.es", "en", "US");
        //        long user1Id = userService.RegisterUser("testLogin1", "passwordtest1", user1Details);
        //        //Create user2
        //        UserProfileDetails user2Details = new UserProfileDetails("testUserName2", "testLastName2", "user2@login.es", "en", "US");
        //        long user2Id = userService.RegisterUser("testLogin2", "passwordtest2", user2Details);

        //        //User1 creates group
        //        long group1Id = recommendationGroupService.CreateGroup("tesNameGroup1", "testDescriptionGroup1", "testLogin1");

        //        // User 2 on Group1
        //        recommendationGroupService.AddUserToGroup("testLogin2", group1Id);

        //        //User1 creates group
        //        long group2Id = recommendationGroupService.CreateGroup("tesNameGroup2", "testDescriptionGroup2", "testLogin1");

        //        // User 2 on Group1
        //        recommendationGroupService.AddUserToGroup("testLogin2", group1Id);

        //        //Creating Category for sport event
        //        //1 = eventId1, 2=eventId2

        //        //User1 add recomendation event1 to group
        //        long idRecommendation1 = recommendationGroupService.AddRecommendation("testLogin1", 1, group1Id, "my_recommendation_text1");
        //        List<DTORecommendation> ListRecommendation = recommendationGroupService.ShowGroupRecommendations(group1Id);
        //        //Check if we insert 1
        //        Assert.AreEqual(1, ListRecommendation.Count);
        //        // User2 add recommendation event2 to group
        //        long idRecommendation2 = recommendationGroupService.AddRecommendation("testLogin2", 2, group1Id, "my_recommendation_text2");
        //        ListRecommendation = recommendationGroupService.ShowGroupRecommendations(group1Id);
        //        //Check if we insert 2
        //        Assert.AreEqual(ListRecommendation.Count, 2);

        //    }
    }
}