namespace MusicTrack.Exceptions.Other
{
    public class PlaylistIsEmptyException : MusicTrackException, IMusicTrackException
    {
        public PlaylistIsEmptyException()
           : base("No tracks in the playlist")
        {
        }
    }
}
