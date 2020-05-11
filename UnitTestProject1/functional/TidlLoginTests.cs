using core;
using core.logger;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace tests.functional
{
    [TestFixture]
    class TidlLoginTests
    {

        private TidlLogin tidlLogin;


        public TidlLoginTests()
        {
        }

        [SetUp]
        public void SetUp()
        {
            tidlLogin = new TidlLogin();
            var client = tidlLogin.GetOpenTidlClient();
            Assert.IsNotNull(client.Configuration.Token);
            Logger.Log.Info($"Tidl client token is {client.Configuration.Token}");
        }

        [Test]
        public async Task LoginTest()
        {
            var result = await tidlLogin.LoginWithDefaultCredentials();
            Assert.IsNull(result.TidlErrorCode);
            Logger.Log.Info("Login test with default credentials successfully passed");
        }

        [Test]
        public async Task NegativeLoginTest()
        {
            var wrongCredentials = new NetworkCredential(tidlLogin.NetworkCredential.UserName, @"Etoro!103");
            var loginResult = await tidlLogin.Login(wrongCredentials);
            Assert.AreEqual(loginResult.TidlErrorCode, (int)HttpStatusCode.Unauthorized);
            Assert.AreEqual(loginResult.TidlErrorMessage, "Username or password is wrong");
            Logger.Log.Info(
                $"Login test with incorrect credentials received {loginResult.TidlErrorCode} error as expected");
        }

    }
}
