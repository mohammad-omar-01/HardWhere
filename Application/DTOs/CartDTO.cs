using Domain.ProductNS;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public class CartDTO
    {
        public string total { get; set; }
        public int userId { get; set; }
        public int cartId { get; set; }
        public int? totalInt { get; set; }
        public bool? isEmpty { get; set; }
        public CartContents contents { get; set; }
    }

    public class CartContents
    {
        public int? itemCount { get; set; }
        public int? productCount { get; set; }
        public List<CartItem> nodes { get; set; }
    }

    public class CartItem
    {
        public int? quantity { get; set; }
        public string key { get; set; }
        public string priceRegular { get; set; } = string.Empty;
        public ProductForCart product { get; set; }
        public int? totalPriceForTheseProducts { get; set; }
    }

    public class ProductForCart
    {
        public int productId { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string price { get; set; }
        public string priceRegular { get; set; } = "0";
        public ProductCartImage productImage { get; set; }
    }

    public class ProductCartImage
    {
        public string sourceUrl { get; set; }
    }
}
