using System;
using System.Collections.Generic;

namespace Service.Model;

public class Product
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int? CategoryId { get; set; }

    public decimal MaterialCost { get; set; }

    public decimal GemCost { get; set; }

    public decimal ProductionCost { get; set; }

    public decimal PriceRate { get; set; }

}
