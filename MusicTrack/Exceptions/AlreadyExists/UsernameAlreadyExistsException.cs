namespace MusicTrack.Exceptions.AlreadyExists
{
    public class UsernameAlreadyExistsException : MusicTrackException, IMusicTrackException
    {
        public UsernameAlreadyExistsException()
           : base("Username is taken")
        {
            ErrorCode = Errors.UsernameAlreadyExists;
        }
    }
}
