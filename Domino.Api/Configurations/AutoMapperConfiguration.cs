using AutoMapper;
using Domino.Api.Core.Dtos;
using Domino.Api.Core.Dtos.User;
using Domino.Api.Core.Entities;

namespace Domino.Api.Configurations;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<UserTable, UserResponseDto>().ReverseMap();
        CreateMap<CreateUserRequestDto, UserTable>().ReverseMap();
    }
}
