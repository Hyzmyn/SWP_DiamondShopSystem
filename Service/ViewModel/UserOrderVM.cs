using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;


namespace Service.ViewModel
{
	public class UserOrderVM
	{
		public User? User { get; set; }
		public IEnumerable<OrderDetail>? OrderDetail { get; set; }
	}
}
