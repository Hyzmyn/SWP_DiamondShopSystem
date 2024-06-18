using Microsoft.AspNetCore.Http;
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
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl1 { get; set; }
       
        public string ImageUrl2 { get; set; }
        public int GemID { get; set; }
        public int MaterialID { get; set; }
        public int CategoryID { get; set; }
        public decimal ProductionCost { get; set; }
        public decimal PriceRate { get; set; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
        public virtual ICollection<ProductGem> ProductGems { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
