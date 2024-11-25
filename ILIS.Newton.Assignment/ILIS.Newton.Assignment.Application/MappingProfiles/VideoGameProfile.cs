using AutoMapper;
using ILIS.Newton.Assignment.Application.Models.Dto;
using ILIS.Newton.Assignment.Entities.Entities;

namespace ILIS.Newton.Assignment.Application.MappingProfiles
{
    public class VideoGameProfile : Profile
    {
        public VideoGameProfile()
        {
            CreateMap<VideoGame, VideoGameDto>();

            CreateMap<VideoGameDto, VideoGame>();
        }
    }
}