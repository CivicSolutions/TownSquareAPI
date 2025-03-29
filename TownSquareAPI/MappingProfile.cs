using AutoMapper;
using TownSquareAPI.DTOs.Community;
using TownSquareAPI.Models;

namespace TownSquareAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Community

            CreateMap<CommunityRequestDTO, Community>();

            CreateMap<Community, CommunityResponseDTO>();
        }
    }
}