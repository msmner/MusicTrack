using System.Runtime.Serialization;

namespace MusicTrack.Models
{
    public enum TrackType
    {
        Recording = 0,
        [EnumMember(Value = "Live Performance")]
        LivePerformance = 1,
        [EnumMember(Value = "Film Music (OST)")]
        FilmMusic = 2,
        [EnumMember(Value = "Background music")]
        BackgroundMusic = 3,
    }
}
