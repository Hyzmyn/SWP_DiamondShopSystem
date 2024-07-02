using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel
{
    public class MonthlyOrderTotal
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalOrders { get; set; }
    }

}
