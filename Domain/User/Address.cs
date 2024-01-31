using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.Enums.CounteryEnum;

namespace Domain.UserNS
{
    public class Address
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressID { get; set; }
        public string AddressName { get; set; } = string.Empty;
        public int userId { get; set; }
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public CountriesEnum? Country { get; set; } = CountriesEnum.PA;
        public string Phone { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public virtual User user { get; set; }
    }
}
