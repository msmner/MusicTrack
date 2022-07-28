namespace MusicTrack.Exceptions.Other
{
    public class TrackTypeNotValidException : MusicTrackException, IMusicTrackException
    {
        public TrackTypeNotValidException()
           : base("Track type not valid")
        {
        }
    }
}
