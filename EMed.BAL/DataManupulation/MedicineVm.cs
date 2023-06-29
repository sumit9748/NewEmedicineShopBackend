using Emedicine.DAL.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emedicine.DAL.DataManupulation
{
    public class MedicineVm
    {
        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public double UnitPrice { get; set; }

        public double Discount { get; set; }

        public DateTime ExpDate { get; set; }

        public string ImgUrl { get; set; }

        public bool Status { get; set; }

        public string Type { get; set; }
        public List<int> MedicalShops { get; set; }
    }
    public class MedicineVm2
    {
        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public double UnitPrice { get; set; }

        public double Discount { get; set; }

        public DateTime ExpDate { get; set; }

        public string ImgUrl { get; set; }

        public bool Status { get; set; }

        public string Type { get; set; }
    }
    
}
