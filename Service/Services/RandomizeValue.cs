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
        // Gold
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }

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
            BuyPrice = 0;
            SellPrice = 0;
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
                BuyPrice = Math.Round(random.Next(5000000, 5400000) / 10000m) * 10000,
                D = Math.Round(random.Next(2500000, 2700001) / 10000m) * 10000,
                E = Math.Round(random.Next(2300000, 2400001) / 10000m) * 10000,
                F = Math.Round(random.Next(2100000, 2200001) / 10000m) * 10000,
                J = Math.Round(random.Next(1800000, 2000001) / 10000m) * 10000,
                IF = Math.Round(random.Next(2600000, 2700001) / 10000m) * 10000,
                VVS1 = Math.Round(random.Next(2400000, 2500001) / 10000m) * 10000,
                VVS2 = Math.Round(random.Next(2200000, 2300001) / 10000m) * 10000,
                VS1 = Math.Round(random.Next(2000000, 2100001) / 10000m) * 10000,
                VS2 = Math.Round(random.Next(1800000, 1900001) / 10000m) * 10000,
                Excellent = Math.Round(random.Next(2500000, 2700001) / 10000m) * 10000,
                VeryGood = Math.Round(random.Next(2200000, 2400001) / 10000m) * 10000,
                Good = Math.Round(random.Next(1900000, 2100001) / 10000m) * 10000,
                CaratPrice = Math.Round(random.Next(2400000, 2600001) / 10000m) * 10000,
            };
            RandomValues.SellPrice = CalculateSellPrice(RandomValues.BuyPrice, random);
        }

        private static decimal CalculateSellPrice(decimal buyPrice, Random random)
        {
            decimal randomIncrement = Math.Round(random.Next(100000, 500000) / 10000m) * 10000; // Random value between 100 and 500
            decimal sellPrice = buyPrice + randomIncrement;
            return sellPrice;
        }


    }
}

