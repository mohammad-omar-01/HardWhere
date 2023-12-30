using Application.DTOs.Product;
using Application.DTOsNS;
using AutoMapper;
using Domain.Enums;
using Domain.ProductNS;

namespace Application
{
    public class ApplicationMapper
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewProductRequestDTO, Product>()
                    .ForMember(
                        destinationMember => destinationMember.ProductImage,
                        opt => opt.Ignore()
                    )
                    .ForMember(
                        dest => dest.RawDescription,
                        opt => opt.MapFrom(src => src.ProductDescription)
                    )
                    .ForMember(
                        destinationMember => destinationMember.GalleryImages,
                        opt => opt.Ignore()
                    )
                    .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserId))
                    .ForMember(
                        dest => dest.Description,
                        opt => opt.MapFrom(src => src.ProductDescription)
                    )
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
                    .ForMember(
                        dest => dest.ShortDescription,
                        opt => opt.MapFrom(src => src.ProductShortDescription)
                    )
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                    .ForMember(
                        dest => dest.StockQuantity,
                        opt => opt.MapFrom(src => src.StockQuantity)
                    )
                    .ForMember(
                        dest => dest.RawPrice,
                        opt => opt.MapFrom(src => src.Price.ToString())
                    )
                    .ForMember(
                        dest => dest.SalePrice,
                        opt => opt.MapFrom(src => src.Price.ToString())
                    )
                    .ForMember(
                        dest => dest.Status,
                        opt => opt.MapFrom(src => src.StatusOfTheProduct)
                    );
                ;
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
