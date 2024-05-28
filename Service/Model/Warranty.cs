using System;
using System.Collections.Generic;

namespace Service.Model;

public class Warranty
{
    public int WarrantyId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public DateOnly BuyDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? Instance { get; set; }

    public bool WarrantyStatus { get; set; }

}
