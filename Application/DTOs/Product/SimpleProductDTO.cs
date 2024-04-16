using Application.DTOsNS;
using Domain.Enums;
using Domain.ProductNS;

namespace Application.DTOs.ProductDTO
{
    public class SimpleProductDTO
    {
        public int databaseId { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string sku { get; set; }
        public bool? onSale { get; set; }
        public DateTime? dateAdded { get; set; }
        public string stockStatus { get; set; }
        public int? stockQuantity { get; set; }
        public string description { get; set; }
        public string rawDescription { get; set; }
        public string shortDescription { get; set; }
        public string status { get; set; }
        public int? averageRating { get; set; }

        public string prdouctApprovalStatus { get; set; } =
            PrdouctApprovalStatus.APPROVED.ToString();

        private decimal _rawPrice;
        public string price
        {
            get { return string.Format("₪{0:N2}", _rawPrice); }
            set { _rawPrice = decimal.Parse(value); }
        }

        private decimal _rawRegularPrice;
        public string rawPrice
        {
            get { return string.Format("₪{0:N2}", _rawRegularPrice); }
            set { _rawRegularPrice = decimal.Parse(value); }
        }

        private int _rawSalePrice;
        public string SalePrice
        {
            get { return string.Format("₪{0:N2}", _rawSalePrice); }
            set
            {
                int num;
                if (Int32.TryParse(value, out num))
                {
                    _rawSalePrice = num;
                }
                else
                {
                    _rawSalePrice = 0;
                }
            }
        }
        public virtual ProductImage? image { get; set; }
        public ICollection<CategoeryDTO> productCategories { get; set; }
        public ICollection<GalleryImage> galleryImages { get; set; }
    }
}
