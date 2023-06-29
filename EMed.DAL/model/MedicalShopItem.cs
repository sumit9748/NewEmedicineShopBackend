
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;

namespace Emedicine.DAL.model
{
    public class MedicalShopItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? MedicineId { get; set; }

        [ForeignKey("MedicineId")]
        
        public virtual Medicine? Medicine { get; set; }

        [Required]
        public int? MedicalShopId { get; set; }

        [ForeignKey("MedicalShopId")]
        

        public virtual Medicalshop? MedicalShop { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
