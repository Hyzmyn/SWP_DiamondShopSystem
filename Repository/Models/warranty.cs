using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Warranty
    {
        [Key]
        public int WarrantyID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public DateTime BuyDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Instance { get; set; }
        public bool WarrantyStatus { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
