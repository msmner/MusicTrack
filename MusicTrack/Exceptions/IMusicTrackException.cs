namespace MusicTrack.Exceptions
{
    public interface IMusicTrackException
    {
        string Message { get; }

        int ErrorCode { get; }

        string Service { get; }
    }
}
