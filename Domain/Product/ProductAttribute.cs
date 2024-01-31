using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ProductNS
{
    public class ProductAttribute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttributeId { get; set; }

        public int ProductId { get; set; }

        public int value { get; set; }
        public virtual Product Product { get; set; }
    }
}
