using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Product
{
    public class ProductUpdateImageDTO
    {
        public int Id { get; set; }
        public IFormFile? MainImage { get; set; }
        public List<IFormFile>? GalleryImages { get; set; }
    }
}
