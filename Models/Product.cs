using System.ComponentModel.DataAnnotations;

namespace ProductWebApi.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; } 

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ApprovalStatus { get; set; } 
        public DateTime ApprovedAt { get; set; }
    }
}
