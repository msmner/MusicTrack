namespace MusicTrack.Exceptions.NotFound
{
    public class UserNotFoundException : MusicTrackException, IMusicTrackException
    {
        public UserNotFoundException()
           : base("User not found")
        {
            ErrorCode = Errors.UserNotFound;
        }
    }
}
