using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class ProductMaterial
    {
        [Key]
        public int  MaterialID { get; set; }
        public int ProductID { get; set; }
        public decimal Weight { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
