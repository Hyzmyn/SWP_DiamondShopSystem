using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class ProductGem
{
    public int? ProductId { get; set; }

    public int? GemId { get; set; }

    public virtual Gem? Gem { get; set; }

    public virtual Product? Product { get; set; }
}
