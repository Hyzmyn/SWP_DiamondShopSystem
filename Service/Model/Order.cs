using System;
using System.Collections.Generic;

namespace Service.Model;

public  class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public int? OrderDetailId { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime TimeOrder { get; set; }

    public string? Note { get; set; }

    public bool OrderStatus { get; set; }

}
