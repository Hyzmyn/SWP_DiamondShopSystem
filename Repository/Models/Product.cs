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
		public decimal Weight { get; set; }
		public int CategoryID { get; set; }
		public decimal ProductionCost { get; set; }
		public decimal PriceRateID { get; set; }
		public decimal TotalCost { get; set; }

		[ForeignKey("GemID")]
		public virtual Gem Gems { get; set; }

		[ForeignKey("PriceRateID")]
		public virtual ICollection<PriceRateList> PriceRateLists { get; set; }
		public virtual ICollection<MaterialPriceList> MaterialPriceLists { get; set; }
		public virtual ICollection<OrderDetail> OrderDetails { get; set; }
	}
}
