using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Contact
{
    public int OrderId { get; set; }

    public string AddressDelivery { get; set; } = null!;

    public string PhoneNumberDelivery { get; set; } = null!;

    public virtual Order? Order { get; set; }
}
