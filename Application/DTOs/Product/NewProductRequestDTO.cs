using Domain.ProductNS;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Product
{
    public class NewProductRequestDTO
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public IEnumerable<String> ProductCategory { get; set; }
        public string ProductShortDescription { get; set; }
        public string Price { get; set; }

        public int StockQuantity { get; set; }
        public int UserId { get; set; }
        public IFormFile MainImage { get; set; }
        public List<IFormFile> GalleryImages { get; set; }
        public string StatusOfTheProduct { get; set; }
    }
}
