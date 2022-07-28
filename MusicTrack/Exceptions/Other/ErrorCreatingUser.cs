namespace MusicTrack.Exceptions.Other
{
    public class ErrorCreatingUser : MusicTrackException, IMusicTrackException
    {
        public ErrorCreatingUser()
           : base("Got errors creating the user. Check console for more information")
        {
        }
    }
}
