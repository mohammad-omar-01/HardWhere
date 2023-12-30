using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Product
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile imageFile);
        public bool DeleteImage(string imageFileName);
    }
}
