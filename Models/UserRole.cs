using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductWebApi.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        
        public int UserId {  get; set; }

        public int RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

    }
}
