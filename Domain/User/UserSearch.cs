using Domain.UserNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.UserNs
{
    public class UserSearch
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<string> serachKeywords { get; set; } = new List<string>();

        [ForeignKey(nameof(User))]
        public int userId { get; set; }
    }
}
