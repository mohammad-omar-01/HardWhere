using Application.DTOs;
using AutoMapper;
using Domain.User;

namespace Application.Mappers
{
    public class UserAutoMaper : Profile
    {
        public UserAutoMaper()
        {
            CreateMap<User, UserSignUpDTO>();
            CreateMap<UserSignUpDTO, User>();
        }
    }
}
