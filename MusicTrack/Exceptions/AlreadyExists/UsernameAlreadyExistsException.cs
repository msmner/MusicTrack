namespace MusicTrack.Exceptions.AlreadyExists
{
    public class UsernameAlreadyExistsException : MusicTrackException, IMusicTrackException
    {
        public UsernameAlreadyExistsException()
           : base("Username or password is incorrect")
        {
        }
    }
}
