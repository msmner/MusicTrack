namespace MusicTrack.Exceptions.Other
{
    public class PlaylistDurationExceededException : MusicTrackException, IMusicTrackException
    {
        public PlaylistDurationExceededException()
           : base("Duration of playlist cannot exceed 2 hours")
        {
            ErrorCode = Errors.PlaylistDurationExceeded;
        }
    }
}
