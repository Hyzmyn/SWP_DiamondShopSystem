using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class GemPriceList
{
    public int GemId { get; set; }

    public string? Origin { get; set; }

    public decimal? CaratWeight { get; set; }

    public string? Color { get; set; }

    public string? Clarity { get; set; }

    public string? Cut { get; set; }

    public decimal Price { get; set; }

    public DateOnly EffDate { get; set; }

    public virtual Gem Gem { get; set; } = null!;
}
