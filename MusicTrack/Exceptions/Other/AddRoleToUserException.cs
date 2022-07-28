namespace MusicTrack.Exceptions.Other
{
    public class AddRoleToUserException : MusicTrackException, IMusicTrackException
    {
        public AddRoleToUserException()
           : base("Error adding user role to user")
        {
        }
    }
}
