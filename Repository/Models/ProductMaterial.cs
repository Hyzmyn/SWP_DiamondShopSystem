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
<<<<<<< HEAD
        public int  MaterialID { get; set; }
        public int ProductID { get; set; }
        public decimal Weight { get; set; }

        [ForeignKey("ProductID")]
=======
        public int MaterialID { get; set; }
        public decimal Weight { get; set; }

        [ForeignKey("MaterialID")]
>>>>>>> 275bb62d07d918df0bffe2a6de9523233ebe2f41
        public virtual Product Product { get; set; }
    }
}
