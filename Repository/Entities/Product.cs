using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int? CategoryId { get; set; }

    public decimal MaterialCost { get; set; }

    public decimal GemCost { get; set; }

    public decimal ProductionCost { get; set; }

    public decimal PriceRate { get; set; }

    public virtual ProductCategory? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ProductMaterial ProductNavigation { get; set; } = null!;

    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}
