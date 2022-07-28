namespace MusicTrack.Exceptions.Other
{
    public class TrackDoesNotBelongToAlbumException : MusicTrackException, IMusicTrackException
    {
        public TrackDoesNotBelongToAlbumException()
           : base("Track does not belong to this album")
        {
        }
    }
}
