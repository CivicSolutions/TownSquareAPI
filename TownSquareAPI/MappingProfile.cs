using AutoMapper;
using TownSquareAPI.DTOs.ApplicationUser;
using TownSquareAPI.DTOs.Community;
using TownSquareAPI.DTOs.Pin;
using TownSquareAPI.DTOs.Post;
using TownSquareAPI.Models;

namespace TownSquareAPI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Community

        CreateMap<CommunityRequestDTO, Community>();

        CreateMap<Community, CommunityResponseDTO>();

        // Pin

        CreateMap<PinRequestDTO, Pin>();

        CreateMap<Pin, PinResponseDTO>();

        // Post

        CreateMap<PostRequestDTO, Post>();

        CreateMap<Post, PostResponseDTO>();

        // User

        CreateMap<UserRequestDTO, ApplicationUser>();

        CreateMap<ApplicationUser, UserResponseDTO>();
    }
}