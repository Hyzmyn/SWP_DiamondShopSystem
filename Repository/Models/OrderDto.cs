using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class OrderDto
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime TimeOrder { get; set; }

        public string Note { get; set; }

        [Required]
        public bool OrderStatus { get; set; }
        public int OrderID { get; set; }
    }
}
