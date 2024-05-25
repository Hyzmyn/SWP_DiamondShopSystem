using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class ProductCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? CategoryType { get; set; }

    public bool CategoryStatus { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
