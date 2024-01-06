using Application.DTOs;
using Application.DTOs.ProductDTO;
using Application.DTOsNS;
using AutoMapper;
using Domain.CartNS;
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
                cfg.CreateMap<CartDTO, Cart>()
                    .ForMember(dest => dest.cartId, opt => opt.Ignore())
                    .ForMember(dest => dest.total, opt => opt.MapFrom(src => src.totalInt))
                    .ForMember(
                        dest => dest.contents,
                        opt => opt.MapFrom(src => src.contents.nodes)
                    );
                cfg.CreateMap<CartContents, CartProduct>();
                cfg.CreateMap<CartItem, CartProduct>()
                    .ForMember(
                        dest => dest.CartProductName,
                        opt => opt.MapFrom(src => src.product.name)
                    )
                    .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.quantity))
                    .ForMember(
                        dest => dest.price,
                        opt => opt.MapFrom(src => Convert.ToDecimal(src.product.priceRegular))
                    )
                    .ForMember(
                        dest => dest.ProductId,
                        opt => opt.MapFrom(src => src.product.productId)
                    )
                    .ForMember(
                        dest => dest.ProductImage,
                        opt => opt.MapFrom(src => src.product.productImage.sourceUrl)
                    );

                cfg.CreateMap<ProductForCart, ProductImage>()
                    .ForMember(dest => dest.SourceUrl, opt => opt.MapFrom(src => src.productImage))
                    .ForMember(
                        dest => dest.SourceUrl,
                        opt => opt.MapFrom(src => src.productImage.sourceUrl)
                    );

                cfg.CreateMap<Cart, CartDTO>()
                    .ForMember(dest => dest.total, opt => opt.MapFrom(src => src.total.ToString()))
                    .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.userId))
                    .ForMember(dest => dest.totalInt, opt => opt.MapFrom(src => src.total))
                    .ForMember(dest => dest.isEmpty, opt => opt.MapFrom(src => src.isEmpty))
                    .ForMember(dest => dest.contents, opt => opt.Ignore());
                cfg.CreateMap<CartProduct, CartContents>()
                    .ForMember(dest => dest.nodes, opt => opt.MapFrom(src => src));

                cfg.CreateMap<CartProduct, CartItem>()
                    .ForMember(
                        dest => dest.priceRegular,
                        opt => opt.MapFrom(src => src.price.ToString())
                    )
                    .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.quantity))
                    .ForMember(dest => dest.key, opt => opt.MapFrom(src => src.ProductId));
                cfg.CreateMap<CartProduct, CartItem>()
                    .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.quantity))
                    .ForMember(dest => dest.key, opt => opt.MapFrom(src => src.ProductId));

                cfg.CreateMap<ProductForCart, ProductImage>()
                    .ForMember(dest => dest.SourceUrl, opt => opt.MapFrom(src => src.productImage))
                    .ForMember(
                        dest => dest.SourceUrl,
                        opt => opt.MapFrom(src => src.productImage.sourceUrl)
                    );

                cfg.CreateMap<ProductImage, ProductForCart>()
                    .ForMember(dest => dest.productImage, opt => opt.MapFrom(src => src));
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
