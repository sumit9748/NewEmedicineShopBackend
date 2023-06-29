using System.ComponentModel.DataAnnotations;

namespace Emedicine.DAL.model
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        [Required]
        public double Discount { get; set; }

        [Required]
        public DateTime ExpDate { get; set; }

        public string ImgUrl { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public string Type { get; set; }

        // Navigation property
        public List<MedicalShopItem> MedicalShopItems { get; set; }


    }
}
