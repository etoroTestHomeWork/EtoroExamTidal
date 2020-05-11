using core;
using core.logger;
using log4net;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace tests.e2e
{
    [TestFixture]
    class E2ETests
    {
        private TidlLogin tidlDTO;
        private TidlApi tidlApi;



        [SetUp]
        public void SetUp()
        {
            tidlDTO = new TidlLogin();
            SetupAsync().Wait();

        }

        public async Task SetupAsync()
        {
            tidlApi = new TidlApi((await tidlDTO.LoginWithDefaultCredentials()).OpenTidlSession);
        }

        [Test]
        public async Task E2ETest()
        {
            var userPlaylist = await tidlApi.CreateUserPlaylist($"Etoro_Playlist_{Guid.NewGuid()}"); 
            Logger.Log.Warn("trackIds hardcoded due to lack of time and loss in the Tidal API forest, " +
                "probably at the moment of checking it will be gone, but at this point, it's Miles Davis - 2178489" +
                "and it's perfect");
            var trackIds = new List<int> { 2178489 };
            await tidlApi.AddPlaylistTracks(userPlaylist.Uuid, userPlaylist.ETag, trackIds);
            var beforeDelete = await tidlApi.GetPlayListTracks(userPlaylist.Uuid);
            var response = await tidlApi.DeleteAllTracksInPlaylist(userPlaylist.Uuid);
            var afterDelete = await tidlApi.GetPlayListTracks(userPlaylist.Uuid);
            Assert.AreNotEqual(beforeDelete.TotalNumberOfItems,afterDelete.TotalNumberOfItems );
        }
    }
}
