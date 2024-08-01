using AutoMapper;
using SSO.Dtos;
using SSO.Entities;

namespace SSO;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}