using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Gem
{
    public int GemId { get; set; }

    public string GemCode { get; set; } = null!;

    public string GemName { get; set; } = null!;

    public string? Origin { get; set; }

    public decimal? FourC { get; set; }

    public string? Proportion { get; set; }

    public string? Polish { get; set; }

    public string? Symmetry { get; set; }

    public string? Fluorescence { get; set; }

    public bool Active { get; set; }

    public virtual GemPriceList? GemPriceList { get; set; }
}
