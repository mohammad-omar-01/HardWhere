using Application.DTOs.image;
using Domain.Product;

namespace Application.DTOs
{
    public class CategoeryDTO
    {
        public int databaseId { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public CategoeryImageDTO image { get; set; }
    }
}
