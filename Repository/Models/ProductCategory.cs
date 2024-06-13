using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class ProductCategory
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public bool CategoryStatus { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
