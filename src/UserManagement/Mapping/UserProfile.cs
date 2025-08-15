using AutoMapper;
using UserManagement.Models;
using UserManagement.Dtos.User;

namespace UserManagement.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<UserRegisterDto, User>().ReverseMap();
            CreateMap<UserUpdateDto, User>().ReverseMap();
        }
    }
}