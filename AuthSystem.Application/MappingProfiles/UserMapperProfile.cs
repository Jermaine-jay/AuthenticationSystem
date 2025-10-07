using AuthSystem.Application.Dtos.Request;
using AuthSystem.Domain.Entities;
using AutoMapper;

namespace AuthSystem.Infrastructure.Application
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {

            CreateMap<User, UserViewModel>();

            CreateMap<RegistrationRequest, User>();
        }
    }
}
