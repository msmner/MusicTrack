namespace MusicTrack.Exceptions.NotFound
{
    public class TrackNotFoundException : MusicTrackException, IMusicTrackException
    {
        public TrackNotFoundException()
           : base("Track not found")
        {
            ErrorCode = Errors.TrackNotFound;
        }
    }
}
