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
		[Range(1, int.MaxValue, ErrorMessage = "GemID must be a positive number.")]
		public int GemID { get; set; }

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Weight must be a positive number.")]
		public decimal Weight { get; set; }
		[Required]
		public int CategoryID { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "ProductCost must be a positive number.")]
		public decimal ProductionCost { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "PriceRate must be a positive number.")]
		public decimal PriceRateID { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "PriceRate must be a positive number.")]
		public decimal TotalCost { get; set; }
    }
}
