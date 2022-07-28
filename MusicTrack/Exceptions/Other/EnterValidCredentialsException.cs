namespace MusicTrack.Exceptions.Other
{
    public class EnterValidCredentialsException : MusicTrackException, IMusicTrackException
    {
        public EnterValidCredentialsException()
           : base("Enter valid username and password")
        {
        }
    }
}
