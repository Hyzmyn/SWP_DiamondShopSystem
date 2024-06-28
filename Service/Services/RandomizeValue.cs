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
        public DateTime EffectDate { get; set; }
        public Decimal BuyPrice { get; set; }
        public Decimal SellPrice { get; set; }


        public RandomizeValue()
        {
            EffectDate = DateTime.Now.AddDays(-2);
            BuyPrice = 0;
            SellPrice = 0;
        }

        public List<RandomizeValue> RandomMaterialPriceValue()
        {
            List<RandomizeValue> datesWithValues = new List<RandomizeValue>();

            Random random = new Random();
            for (int i = -5; i <= 5; i++)
            {
                DateTime currentDate = DateTime.Now.AddDays(i); // Use DateTime.Now or your desired start date
                RandomizeValue newDate = new RandomizeValue();

                newDate.EffectDate = currentDate;
                newDate.BuyPrice = GetRandomBuyPrice(random);
                newDate.SellPrice = CalculateSellPrice(newDate.BuyPrice, random);

                datesWithValues.Add(newDate);
            }

            return datesWithValues;
        }

        private static decimal GetRandomBuyPrice(Random random)
        {
            decimal price = Math.Round(random.Next(7500, 8501) / 100m) * 100;
            return price;
        }

        private static decimal CalculateSellPrice(decimal buyPrice, Random random)
        {
            decimal randomIncrement = random.Next(100, 501); // Random value between 100 and 500
            decimal sellPrice = buyPrice + randomIncrement;
            return sellPrice;
        }

        /*              cách call function này
        MyClass myObject = new MyClass();
List<MyClass> datesWithValues = myObject.RandomMatetialPriceValue();

foreach (var date in datesWithValues)
{
    Console.WriteLine($"Effect Date: {date.EffectDate}");
    Console.WriteLine($"BuyPrice: {date.BuyPrice}");
    Console.WriteLine($"SellPrice: {date.SellPrice}");
    Console.WriteLine();

    // Accessing color and shape individually
    string BuyPrice = date.BuyPrice;
    string SellPrice = date.SellPrice;

    Console.WriteLine($"BuyPrice: {BuyPrice}");
    Console.WriteLine($"SellPrice: {SellPrice}");
    Console.WriteLine();
}
        */
    }
}

