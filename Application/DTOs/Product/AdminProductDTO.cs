using Application.DTOsNS;
using Domain.Enums;

namespace Application.DTOs.Product
{
    public class AdminProductDTO
    {
        public int databaseId { get; set; }
        public string name { get; set; } = string.Empty;
        public int? stockQuantity { get; set; }

        public string prdouctApprovalStatus { get; set; } =
            PrdouctApprovalStatus.APPROVED.ToString();

        private decimal _rawPrice;
        public string price
        {
            get { return string.Format("₪{0:N2}", _rawPrice); }
            set { _rawPrice = decimal.Parse(value); }
        }
        public ICollection<CategoeryDTOAdmin> productCategories { get; set; }
    }
}
