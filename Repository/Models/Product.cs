using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{

    public class Product
    {
        [Key]
        public int ProductID { get; set; }
<<<<<<< HEAD

        public string ProductCode { get; set; }
       
        public string ProductName { get; set; }
       
        public string ImageUrl { get; set; }
        public int CategoryID { get; set; }
       
        public decimal MaterialId { get; set; }
       
        public decimal GemId { get; set; }
       
        public decimal ProductionCost { get; set; }
       
=======
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryID { get; set; }
        public decimal ProductionCost { get; set; }
>>>>>>> 275bb62d07d918df0bffe2a6de9523233ebe2f41
        public decimal PriceRate { get; set; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { get; set; }
<<<<<<< HEAD
        [ForeignKey("MaterialId")]
        public virtual ProductMaterial ProductMaterial { get; set; }
        [ForeignKey("GemId")]
        public virtual Gem Gem { get; set; }

=======
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
        public virtual ICollection<ProductGem> ProductGems { get; set; }
>>>>>>> 275bb62d07d918df0bffe2a6de9523233ebe2f41
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
