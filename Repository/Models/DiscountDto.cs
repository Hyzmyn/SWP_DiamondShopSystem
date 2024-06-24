using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class DiscountDto
    {
        [Required(ErrorMessage = "Order ID is required")]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Discount Code is required")]
        public string DiscountCode { get; set; }

        [Required(ErrorMessage = "Discount Amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Discount Amount must be a positive number")]
        public decimal DiscountAmount { get; set; }

        [Required(ErrorMessage = "Discount Date is required")]
        [DataType(DataType.Date)]
        public DateTime DiscountDate { get; set; }

        [Required(ErrorMessage = "Discount Status is required")]
        public bool DiscountStatus { get; set; }
        public int DiscountID { get; set; }
    }
}
