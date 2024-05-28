using System;
using System.Collections.Generic;

namespace Service.Model;

public  class ProductCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? CategoryType { get; set; }

    public bool CategoryStatus { get; set; }

}
