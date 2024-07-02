using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Gem
    {
        [ForeignKey("Product")]
        [Key]
        public int GemID { get; set; }
        public string GemCode { get; set; }
        public string GemName { get; set; }
        public string Origin { get; set; }
        public decimal FourC { get; set; }
        public string Proportion { get; set; }
        public string Polish { get; set; }
        public string Symmetry { get; set; }
        public string Fluorescence { get; set; }
        public DateTime? DateTime { get; set; }
        public bool Active { get; set; }


        public virtual Product Product { get; set; }

    }
}
