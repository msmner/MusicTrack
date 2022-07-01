namespace MusicTrack.Exceptions.Other
{
    public class TrackIsNotIncludedInPlaylistException : MusicTrackException, IMusicTrackException
    {
        public TrackIsNotIncludedInPlaylistException()
           : base("Track is not included in the playlist")
        {
            ErrorCode = Errors.TrackNotIncludedInPlaylist;
        }
    }
}
