using Application.DTOs.ProductDTO;
using Application.DTOs.User;
using Application.DTOs.UserType;
using Application.DTOsNS;
using Application.DTOsNS.image;
using Application.DTOsNS.UserType;
using AutoMapper;
using Domain.ProductNS;
using Domain.UserNS;
using Domain.UserType;

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
            CreateMap<User, PersonalInformationDTO>().ReverseMap();
            CreateMap<BillingAdressDTO, Address>().ReverseMap();
            CreateMap<ShippingAdressDTO, Address>().ReverseMap();
            CreateMap<ViewerDTO, User>();
            CreateMap<CategoeryDTO, ProductCategory>();
            CreateMap<ProductCategory, CategoeryDTO>()
                .ForMember(dest => dest.databaseId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.CategoryImage));
            CreateMap<CategoreyImage, CategoeryImageDTO>();
            CreateMap<CategoeryImageDTO, CategoreyImage>();

            CreateMap<Product, SimpleProductDTO>()
                .ForMember(dest => dest.databaseId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.onSale, opt => opt.MapFrom(src => src.OnSale))
                .ForMember(
                    dest => dest.dateAdded,
                    opt => opt.MapFrom(src => src.DateAdded.Value.Date)
                )
                .ForMember(dest => dest.stockStatus, opt => opt.MapFrom(src => src.StockStatus))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.stockQuantity, opt => opt.MapFrom(src => src.StockQuantity))
                .ForMember(
                    dest => dest.rawDescription,
                    opt => opt.MapFrom(src => src.RawDescription)
                )
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.RawPrice))
                .ForMember(dest => dest.rawPrice, opt => opt.MapFrom(src => src.RawPrice))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.ProductImage))
                .ForMember(
                    dest => dest.productCategories,
                    opt => opt.MapFrom(src => src.Categories)
                )
                .ForMember(
                    dest => dest.galleryImages,
                    opt => opt.MapFrom(src => src.GalleryImages)
                );
        }
    }
}
