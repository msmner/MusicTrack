namespace MusicTrack.Exceptions.NotFound
{
    public class AlbumNotFoundException : MusicTrackException, IMusicTrackException
    {
        public AlbumNotFoundException()
           : base("Album not found")
        {
            ErrorCode = Errors.AlbumNotFound;
        }
    }
}
