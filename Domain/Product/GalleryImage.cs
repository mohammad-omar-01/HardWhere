using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ProductNS
{
    public class GalleryImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GalleryImageId { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public string SourceUrl { get; set; }
        public Product Product { get; set; }
    }
}
