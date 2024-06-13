using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public int UserID { get; set; }
<<<<<<< HEAD
        public int OrderDetailID { get; set; }
=======
>>>>>>> 275bb62d07d918df0bffe2a6de9523233ebe2f41
        public decimal TotalPrice { get; set; }
        public DateTime TimeOrder { get; set; }
        public string Note { get; set; }
        public bool OrderStatus { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
<<<<<<< HEAD
        [ForeignKey("OrderID")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
=======
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
>>>>>>> 275bb62d07d918df0bffe2a6de9523233ebe2f41
    }
}
