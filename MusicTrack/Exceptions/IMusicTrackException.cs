namespace MusicTrack.Exceptions
{
    public interface IMusicTrackException
    {
        string Message { get; }

        string Service { get; }

        public string? StackTrace { get; set; }
    }
}
