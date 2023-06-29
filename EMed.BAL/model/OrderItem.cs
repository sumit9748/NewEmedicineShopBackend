using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emedicine.DAL.model
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order order { get; set; }
        [Required]
        public int? MedicineId { get; set; }
        [ForeignKey("MedicineId")]
        public virtual Medicine medicine { get; set; }

    }
}
