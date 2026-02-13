using System.ComponentModel.DataAnnotations;

namespace ProductWebApi.Models
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
