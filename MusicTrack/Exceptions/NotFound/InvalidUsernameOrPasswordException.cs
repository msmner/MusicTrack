namespace MusicTrack.Exceptions.NotFound
{
    public class InvalidUsernameOrPasswordException : MusicTrackException, IMusicTrackException
    {
        public InvalidUsernameOrPasswordException()
           : base("Invalid username or password")
        {
        }
    }
}
