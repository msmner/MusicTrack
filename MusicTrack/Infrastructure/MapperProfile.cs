using MusicTrack.Dtos;
using MusicTrack.Models;

namespace MusicTrack.Infrastructure
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<Track, TrackDto>();
            CreateMap<Album, AlbumDto>().ForMember(x => x.Tracks, opt => opt.MapFrom(y => y.Tracks.Select(z => z.Name)));
            CreateMap<PlayList, CreatePlaylistDto>();
            CreateMap<PlayList, GetPlaylistDto>();
        }
    }
}
