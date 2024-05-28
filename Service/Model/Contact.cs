using System;
using System.Collections.Generic;

namespace Service.Model;

public class Contact
{
    public int OrderId { get; set; }

    public string AddressDelivery { get; set; } = null!;

    public string PhoneNumberDelivery { get; set; } = null!;


}
