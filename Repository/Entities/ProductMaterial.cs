using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class ProductMaterial
{
    public int ProductId { get; set; }

    public int? MaterialId { get; set; }

    public decimal Weight { get; set; }

    public virtual MaterialPriceList? MaterialPriceList { get; set; }

    public virtual Product? Product { get; set; }
}
