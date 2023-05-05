using System.ComponentModel.DataAnnotations;

namespace DNATestingKit.Models
{
    public class OrderModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }     
        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
    }
}
