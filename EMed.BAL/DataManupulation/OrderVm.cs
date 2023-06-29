using Emedicine.DAL.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emedicine.DAL.DataManupulation
{
    public class OrderVm
    {
        public int UserId { get; set; }
        public int MedicalshopId { get; set; }
        public IList<CartVM2> carts { get; set; }
    }
    public class OrderByMonth
    {
        public int Month { get; set; }
        public double Total { get; set; }
    }
}
