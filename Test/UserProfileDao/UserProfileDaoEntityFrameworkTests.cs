using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class IUserProfileDaoEntityFrameworkTest
    {
        private static IKernel kernel;
        private TestContext testContextInstance;
        private static IUserProfileDao userProfileDao;
        private static UserProfile userProfile;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_USER_ID = -1;

        private TransactionScope transaction;

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

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            userProfileDao = kernel.Get<IUserProfileDao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transaction = new TransactionScope();

            userProfile = new UserProfile();
            userProfile.loginName = "jsmith";
            userProfile.enPassword = "password";
            userProfile.firstName = "John";
            userProfile.lastName = "Smith";
            userProfile.email = "jsmith@acme.com";
            userProfile.language = "en";
            userProfile.country = "US";

            userProfileDao.Create(userProfile);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion Additional test attributes

        /// <summary>
        ///A test for FindByLoginName
        ///</summary>
        [TestMethod()]
        public void DAO_FindByLoginNameTest()
        {
            try
            {
                UserProfile actual = userProfileDao.FindByLoginName("jsmith");

                Assert.AreEqual(userProfile, actual, "User found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}