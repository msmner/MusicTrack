namespace MusicTrack.Exceptions.Other
{
    public class TrackAlreadyIncludedInPlaylistException : MusicTrackException, IMusicTrackException
    {
        public TrackAlreadyIncludedInPlaylistException()
           : base("Track is already included in the playlist")
        {
        }
    }
}
