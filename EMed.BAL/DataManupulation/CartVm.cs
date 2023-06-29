

using System.ComponentModel.DataAnnotations;
using System;

namespace Emedicine.DAL.DataManupulation
{
    public class CartVm
    {
        public int UserId { set; get; }
        public List<Tuple<int, int>> Medicines { get; set; }
        public int MedicalShopId { set; get; }

    }
    public class CartVM2
    {
        public int UserId { set; get; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public int Quantity { get; set; }

        public int MedicineId { get; set; }

        public int MedicalShopId { get; set; }
    }
}
