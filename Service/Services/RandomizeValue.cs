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
				D = Math.Round(random.Next(2500, 2701) / 100m) * 100,
				E = Math.Round(random.Next(2300, 2401) / 100m) * 100,
				F = Math.Round(random.Next(2100, 2201) / 100m) * 100,
				J = Math.Round(random.Next(1800, 2001) / 100m) * 100,
				IF = Math.Round(random.Next(2600, 2701) / 100m) * 100,
				VVS1 = Math.Round(random.Next(2400, 2501) / 100m) * 100,
				VVS2 = Math.Round(random.Next(2200, 2301) / 100m) * 100,
				VS1 = Math.Round(random.Next(2, 2101) / 100m) * 100,
				VS2 = Math.Round(random.Next(1800, 1901) / 100m) * 100,
				Excellent = Math.Round(random.Next(2500, 2701) / 100m) * 100,
				VeryGood = Math.Round(random.Next(2200, 2401) / 100m) * 100,
				Good = Math.Round(random.Next(1900, 2101) / 100m) * 100,
				CaratPrice = Math.Round(random.Next(2400, 2601) / 100m) * 100,
			};
		}
	}
}

