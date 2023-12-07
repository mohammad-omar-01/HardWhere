using Application.DTOs;
using AutoMapper;
using Domain.UserType;
using Domain.User;
using Application.DTOs.UserType;

namespace Application.Mappers
{
    public class UserAutoMaper : Profile
    {
        public UserAutoMaper()
        {
            CreateMap<User, UserSignUpDTO>();
            CreateMap<UserSignUpDTO, User>();
            CreateMap<User, CustomerDTO>();
            CreateMap<CustomerDTO, User>();
            CreateMap<User, ViewerDTO>();
            CreateMap<ViewerDTO, User>();
        }
    }
}
