using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Repository.Models
{
	public class MaterialPriceList
	{
		[ForeignKey("Product")]
		[Key]
		public int MaterialID { get; set; }
		public decimal BuyPrice { get; set; }
		public decimal SellPrice { get; set; }
		public DateTime EffDate { get; set; }

        [ValidateNever]
        public virtual Product Product { get; set; }
    }
}