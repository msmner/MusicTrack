namespace MusicTrack.Exceptions
{
    public class MusicTrackException : Exception, IMusicTrackException
    {
        public virtual string Service { get; set; } = "MusicTrackService";

        public new string? StackTrace { get; set; } = null!;

        public MusicTrackException()
        { }

        public MusicTrackException(string message)
            : base(message)
        {
        }
    }
}
