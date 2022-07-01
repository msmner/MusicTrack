namespace MusicTrack.Exceptions
{
    public static class Errors
    {
        ////General
        public const int ApplicationError = 1001;

        public const int TrackNotFound = 1002;
        public const int AlbumNotFound = 1003;
        public const int TrackDoesNotBelongToThisAlbum = 1004;
        public const int PlaylistNotFound = 1005;
        public const int TrackAlreadyIncludedInPlaylist = 1006;
        public const int PlaylistDurationExceeded = 1007;
        public const int TrackNotIncludedInPlaylist = 1008;
        public const int PlaylistIsEmpty = 1009;
        public const int UsernameAlreadyExists = 1010;
        public const int TrackTypeNotValid = 1011;
        public const int UserNotFound = 1012;
        public const int NeedValidCredentials = 1013;
        //public const int InvalidSession = 1403;
        //public const int AuthNoRights = 1404;
        //public const int AuthFailed = 1405;
        //public const int InvalidJwtToken = 1411;
    }
}
