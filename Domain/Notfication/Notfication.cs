using Domain.UserNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.NotficationNS
{
    public class Notfication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotficationId { get; set; }
        public int userId { get; set; }
        public string NotficationTitle { get; set; }
        public string NotficationBody { get; set; }
        public string NotficationType { get; set; }
        public string slug { get; set; }
        public virtual User User { get; set; }
    }
}
