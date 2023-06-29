using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emedicine.DAL.model
{
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User user { get; set; }
        [Required]

        public decimal OrderTotal { get; set; }
        [Required]
        public string OrderStatus { get; set; }
        
    }
}
