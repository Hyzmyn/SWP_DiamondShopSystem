using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public int? OrderDetailId { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime TimeOrder { get; set; }

    public string? Note { get; set; }

    public bool OrderStatus { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Contact OrderNavigation { get; set; } = null!;

    public virtual User? User { get; set; }

    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}
