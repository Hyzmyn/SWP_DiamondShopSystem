using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
	public class ProductDto
	{
		[Required]
		public int ProductID { get; set; }
		[Required]
		public string ProductCode { get; set; }
		[Required]
		public string ProductName { get; set; }

		public IFormFile? ImageUrl1 { get; set; }

		public IFormFile? ImageUrl2 { get; set; }
		[Required]
		public int GemID { get; set; }
		[Required]
		public int MaterialID { get; set; }
		[Required]
		public int CategoryID { get; set; }
		[Required]
		public decimal ProductionCost { get; set; }
		[Required]
		public decimal PriceRateID { get; set; }
        [Required]
        public decimal TotalCost { get; set; }
    }
}
