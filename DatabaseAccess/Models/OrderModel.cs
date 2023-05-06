using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatabaseAccess.Models
{
    public class OrderModel
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "DeliveryDate is required")]
        public DateTime DeliveryDate { get; set; }

        public double Price = 98.99;
        public double Sum {  get; set; }
    }
}
