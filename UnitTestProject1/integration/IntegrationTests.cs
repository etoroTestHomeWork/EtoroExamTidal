using core;
using core.logger;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace tests.integration
{
    [TestFixture]
    class IntegrationApiTests
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
        public async Task CreateUserPlaylistTest()
        {
            string title = $"Playlist_etoro_{Guid.NewGuid()}";
            var userPlayListsBefore = await tidlApi.GetUserPlaylists();
            var playlistModel = await tidlApi.CreateUserPlaylist(title);
            Assert.AreEqual(title, playlistModel.Title);
            Assert.AreNotEqual(
                userPlayListsBefore.Items.Length,
                (await tidlApi.GetUserPlaylists()).Items.Length);
            Logger.Log.Info($"Playlist with {title} title successfully created");
        }

        [Test]
        public async Task AddPlaylistTracksTest()
        {
            var title = $"Playlist_etoro_{Guid.NewGuid()}";
            var createdPlaylist = await tidlApi.CreateUserPlaylist(title);
            Assert.AreEqual(title, createdPlaylist.Title);
            Logger.Log.Warn("trackIds hardcoded due to lack of time and loss in the Tidal API forest, " +
                "probably at the moment of checking it will be gone, but at this point, it's Miles Davis - 2178489" +
                "and it's perfect");
            List<int> trackIds = new List<int> { 2178489 };
            Assert.AreNotEqual(trackIds.Count,
                (await tidlApi.GetPlaylistTracks(createdPlaylist.Uuid)).TotalNumberOfItems);
            await tidlApi.AddPlaylistTracks(createdPlaylist.Uuid, createdPlaylist.ETag, trackIds);
            Assert.AreEqual(trackIds.Count, 
                (await tidlApi.GetPlaylistTracks(createdPlaylist.Uuid)).TotalNumberOfItems);
            Logger.Log.Info($"Tracks successfully added to {title}");
        }
    }
}
