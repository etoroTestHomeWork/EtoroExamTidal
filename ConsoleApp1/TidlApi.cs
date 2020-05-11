using OpenTidl.Methods;
using OpenTidl.Models;
using OpenTidl.Models.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core
{
    public class TidlApi
    {
        private readonly OpenTidlSession tidlSession;

        public TidlApi(OpenTidlSession tidlSession)
        {
            this.tidlSession = tidlSession;
        }

        public async Task<PlaylistModel> CreateUserPlaylist(string playlistTitle)
        {
            return await tidlSession.CreateUserPlaylist(playlistTitle);
        }


        public async Task<JsonList<PlaylistModel>> GetUserPlaylists()
        {
            return await tidlSession.GetUserPlaylists();
        }

        public async Task<EmptyModel> AddPlaylistTracks(string uuid, string playlistETag, IEnumerable<int> trackIds)
        {
            return await tidlSession.AddPlaylistTracks(uuid, playlistETag, trackIds);
        }

        public async Task<JsonList<TrackModel>> GetPlaylistTracks(string uuid)
        {
            return await tidlSession.GetPlaylistTracks(uuid);
        }

        public async Task<EmptyModel> DeleteAllTracksInPlaylist(string uuid)
        {
            return await tidlSession.DeletePlaylistTracks(
                uuid, 
                (await tidlSession.GetPlaylistTracks(uuid)).ETag,
                (await tidlSession.GetUserPlaylists()).Items.Select(t => t.Id).ToList());
        }

        public Task<JsonList<TrackModel>> GetPlayListTracks(string uuid)
        {
            return tidlSession.GetPlaylistTracks(uuid);
        }
    }
}
