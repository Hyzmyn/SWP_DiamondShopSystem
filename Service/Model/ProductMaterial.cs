using System;
using System.Collections.Generic;

namespace Service.Model;

public class ProductMaterial
{
    public int ProductId { get; set; }

    public int? MaterialId { get; set; }

    public decimal Weight { get; set; }

}
