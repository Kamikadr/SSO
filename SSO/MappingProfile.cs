using AutoMapper;
using SSO.ApiMessages;
using SSO.Commands;
using SSO.Messages;

namespace SSO;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequest, RegisterCommand>();
        CreateMap<LoginRequest, LoginQuery>();
    }
}