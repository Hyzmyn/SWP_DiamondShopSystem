using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Services
{
	public class RandomizeValue
	{
		// Color
		public decimal D { get; set; }
		public decimal E { get; set; }
		public decimal F { get; set; }
		public decimal J { get; set; }

		// Clarity
		public decimal IF { get; set; }
		public decimal VVS1 { get; set; }
		public decimal VVS2 { get; set; }
		public decimal VS1 { get; set; }
		public decimal VS2 { get; set; }

		// Cut
		public decimal Excellent { get; set; }
		public decimal VeryGood { get; set; }
		public decimal Good { get; set; }

		// Carat
		public decimal CaratPrice { get; set; }
		public RandomizeValue()
		{
			D = 0;
			E = 0;
			F = 0;
			J = 0;
			IF = 0;
			VVS1 = 0;
			VVS2 = 0;
			VS1 = 0;
			VS2 = 0;
			Excellent = 0;
			VeryGood = 0;
			Good = 0;
			CaratPrice = 0;
		}
	}

	public static class RandomNumberStore
	{
		public static readonly RandomizeValue RandomValues;

		static RandomNumberStore()
		{
			Random random = new Random();
			RandomValues = new RandomizeValue
			{
				// Set random values for each property
				D = random.Next(25, 27),
				E = random.Next(23, 24),
				F = random.Next(21, 22),
				J = random.Next(18, 20),
				IF = random.Next(26, 27),
				VVS1 = random.Next(24, 25),
				VVS2 = random.Next(22, 23),
				VS1 = random.Next(20, 21),
				VS2 = random.Next(18, 19),
                Excellent = random.Next(25, 27),
				VeryGood = random.Next(22, 24),
				Good = random.Next(19, 21),
				CaratPrice = random.Next(24, 26),
			};
		}
	}
}

