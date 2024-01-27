using Application.DTOs.Cart;
using Application.DTOs.Notfication;
using Application.DTOs.Order;
using Application.DTOs.ProductDTO;
using Application.DTOsNS;
using AutoMapper;
using Domain.CartNS;
using Domain.Enums;
using Domain.NotficationNS;
using Domain.OrderNS;
using Domain.ProductNS;
using Domain.UserNS;

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

                cfg.CreateMap<ProductRequestDTO, ProductImage>()
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

                cfg.CreateMap<ProductRequestDTO, ProductImage>()
                    .ForMember(dest => dest.SourceUrl, opt => opt.MapFrom(src => src.productImage))
                    .ForMember(
                        dest => dest.SourceUrl,
                        opt => opt.MapFrom(src => src.productImage.sourceUrl)
                    );

                cfg.CreateMap<ProductImage, ProductRequestDTO>()
                    .ForMember(dest => dest.productImage, opt => opt.MapFrom(src => src));

                cfg.CreateMap<OrderDTO, Order>()
                    .ForMember(dest => dest.customerId, opt => opt.MapFrom(src => src.userId))
                    .ForMember(dest => dest.total, opt => opt.MapFrom(src => src.totalInt))
                    .ForMember(
                        dest => dest.shippingTotal,
                        opt => opt.MapFrom(src => src.shipptingTotal)
                    );

                cfg.CreateMap<Address, AddressDTO>();
                cfg.CreateMap<AddressDTO, Address>();
                cfg.CreateMap<NotficationDTO, Notfication>().ReverseMap();
                cfg.CreateMap<AddressReturnResultDTO, Address>();
                cfg.CreateMap<Address, AddressReturnResultDTO>();
                cfg.CreateMap<User, UserDTOForAdress>();
                cfg.CreateMap<UserDTOForAdress, User>();

                cfg.CreateMap<Address, AddressReturnResultDTO>();
                cfg.CreateMap<Order, OrderDtoReturnResult>()
                    .ForPath(dest => dest.result.userId, opt => opt.MapFrom(src => src.customerId))
                    .ForPath(dest => dest.result.totalInt, opt => opt.MapFrom(src => src.total))
                    .ForPath(
                        dest => dest.result.shipptingTotal,
                        opt => opt.MapFrom(src => src.shippingTotal)
                    )
                    .ForPath(dest => dest.result.contents, opt => opt.MapFrom(src => src.contentes))
                    .ForPath(
                        dest => dest.result.shippingAddressId,
                        opt => opt.MapFrom(src => src.ShippingAdressId)
                    )
                    .ForPath(
                        dest => dest.result.billingAddressId,
                        opt => opt.MapFrom(src => src.BillingAddressId)
                    )
                    .ForPath(
                        dest => dest.result.total,
                        opt => opt.MapFrom(src => src.total.ToString())
                    )
                    .ForPath(dest => dest.OrderId, opt => opt.MapFrom(src => src.orderId))
                    .ForPath(dest => dest.shipping, opt => opt.MapFrom(src => src.ShippingAddress))
                    .ForPath(dest => dest.billing, opt => opt.MapFrom(src => src.BillingAdress))
                    .ForPath(
                        dest => dest.OrderDate,
                        opt => opt.MapFrom(src => src.orderDate.ToShortDateString())
                    )
                    .ForPath(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.orderStatus))
                    .ReverseMap();

                cfg.CreateMap<Order, OrderDTO>()
                    .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.customerId))
                    .ForMember(dest => dest.totalInt, opt => opt.MapFrom(src => src.total))
                    .ForMember(
                        dest => dest.shipptingTotal,
                        opt => opt.MapFrom(src => src.shippingTotal)
                    )
                    .ForPath(dest => dest.contents, opt => opt.MapFrom(src => src.contentes))
                    .ForPath(
                        dest => dest.shippingAddressId,
                        opt => opt.MapFrom(src => src.ShippingAdressId)
                    )
                    .ForPath(
                        dest => dest.billingAddressId,
                        opt => opt.MapFrom(src => src.BillingAddressId)
                    )
                    .ReverseMap();

                cfg.CreateMap<OrderItem, OrderContents>()
                    .ForMember(
                        dest => dest.OrderProductName,
                        opt => opt.MapFrom(src => src.product.name)
                    )
                    .ForMember(dest => dest.slug, opt => opt.MapFrom(src => src.product.slug))
                    .ForMember(
                        dest => dest.ProductId,
                        opt => opt.MapFrom(src => src.product.productId)
                    )
                    .ForMember(
                        dest => dest.price,
                        opt => opt.MapFrom(src => double.Parse(src.product.priceRegular))
                    )
                    .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.quantity))
                    .ForMember(
                        dest => dest.ProductImage,
                        opt => opt.MapFrom(src => src.product.productImage.sourceUrl)
                    )
                    .ForMember(dest => dest.slug, opt => opt.MapFrom(src => src.product.slug))
                    .ReverseMap()
                    .ForPath(a => a.product.name, opt => opt.MapFrom(src => src.OrderProductName))
                    .ForPath(
                        a => a.product.productImage.sourceUrl,
                        opt => opt.MapFrom(src => src.ProductImage)
                    )
                    .ForPath(a => a.product.productId, opt => opt.MapFrom(src => src.ProductId))
                    .ForPath(
                        a => a.product.priceRegular,
                        opt => opt.MapFrom(src => src.price.ToString())
                    )
                    .ForPath(
                        a => a.product.price,
                        opt => opt.MapFrom(src => "₪" + src.price.ToString())
                    )
                    .ForPath(a => a.product.slug, opt => opt.MapFrom(src => src.slug))
                    .ForPath(a => a.quantity, opt => opt.MapFrom(src => src.quantity))
                    .ForPath(
                        a => a.totalPriceForTheseProducts,
                        opt => opt.MapFrom(src => (src.price * src.quantity))
                    )
                    .ForPath(a => a.priceRegular, opt => opt.MapFrom(src => src.price.ToString()));
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
