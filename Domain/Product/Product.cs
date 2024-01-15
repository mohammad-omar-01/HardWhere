using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.UserNS;
using Domain.Enums;

namespace Domain.ProductNS
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public PrdouctApprovalStatus prdouctApprovalStatus { get; set; }
        public string Sku { get; set; }
        public bool? OnSale { get; set; }
        public DateTime? DateAdded { get; set; }
        public string StockStatus { get; set; }
        public int? StockQuantity { get; set; }
        public string Description { get; set; }
        public string RawDescription { get; set; }
        public string ShortDescription { get; set; }
        public decimal? AverageRating { get; set; }
        public ProductStatusEnum Status { get; set; }
        public string RawPrice { get; set; }
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }
        public virtual ProductImage? ProductImage { get; set; }
        public ICollection<ProductCategory> Categories { get; set; } = new List<ProductCategory>();
        public ICollection<GalleryImage> GalleryImages { get; set; } = new List<GalleryImage>();

        [ForeignKey(nameof(User))]
        public int UserID { get; set; }
    }
}
