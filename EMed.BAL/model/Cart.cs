using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Emedicine.DAL.model
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User user { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public int Quantity { get; set; }

        public int MedicineId { get; set; }
        [ForeignKey("MedicineId")]

        public virtual Medicine Medicine { get; set; }
        public int MedicalShopId { get; set; }
        [ForeignKey("MedicalShopId")]
        public virtual Medicalshop Medicicalshop { get; set; }
    }
}
