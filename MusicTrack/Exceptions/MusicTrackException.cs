namespace MusicTrack.Exceptions
{
    public class MusicTrackException : Exception, IMusicTrackException
    {
        public int ErrorCode { get; set; }

        public virtual string Service { get; set; } = "MusicTrackService";

        public MusicTrackException()
        { }

        public MusicTrackException(string message)
            : base(message)
        {
            ErrorCode = Errors.ApplicationError;
        }
    }
}
