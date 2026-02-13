using System.ComponentModel.DataAnnotations;

namespace ProductWebApi.Models
{
    public class Role
    {

        [Key]
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

        public ICollection<UserRole> UserRoles { get; set; }= new List<UserRole>();
    }
}
