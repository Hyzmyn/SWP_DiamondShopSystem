using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
	public class GemPriceList
	{
		[Key, ForeignKey("Gem")]
		public int GemID { get; set; }
		public string Origin { get; set; }
		public decimal CaratWeight { get; set; }
		public string Color { get; set; }
		public string Clarity { get; set; }
		public string Cut { get; set; }
		public decimal Price { get; set; }
		public DateTime EffDate { get; set; }

		public virtual Gem Gem { get; set; }
	}
}
