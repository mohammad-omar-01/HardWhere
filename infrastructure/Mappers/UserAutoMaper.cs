using Application.DTOs;
using AutoMapper;
using Domain.UserType;
using Domain.User;
using Application.DTOs.UserType;
using Domain.Product;
using Application.DTOs.image;

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
            CreateMap<CategoeryDTO, ProductCategory>();
            CreateMap<ProductCategory, CategoeryDTO>()
                .ForMember(dest => dest.databaseId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.CategoryImage));
            CreateMap<CategoreyImage, CategoeryImageDTO>();
            CreateMap<CategoeryImageDTO, CategoreyImage>();
        }
    }
}
