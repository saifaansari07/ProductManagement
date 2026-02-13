using System.ComponentModel.DataAnnotations;

namespace ProductWebApi.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public string Password { get; set; } = null!;

        public byte IsActive { get; set; } // 1- active, 0- inactive

        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

       
    }
}
