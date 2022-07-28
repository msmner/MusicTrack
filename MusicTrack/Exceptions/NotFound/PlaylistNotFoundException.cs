namespace MusicTrack.Exceptions.NotFound
{
    public class PlaylistNotFoundException : MusicTrackException, IMusicTrackException
    {
        public PlaylistNotFoundException()
           : base("Playlist not found")
        {
        }
    }
}
