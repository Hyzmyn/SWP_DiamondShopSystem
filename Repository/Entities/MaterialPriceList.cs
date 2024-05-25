using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class MaterialPriceList
{
    public int Id { get; set; }

    public int? MaterialId { get; set; }

    public decimal BuyPrice { get; set; }

    public decimal SellPrice { get; set; }

    public DateOnly EffDate { get; set; }

    public virtual ProductMaterial? Material { get; set; }
}
