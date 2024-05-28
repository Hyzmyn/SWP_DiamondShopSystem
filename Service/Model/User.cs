using System;
using System.Collections.Generic;

namespace Service.Model;

public class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public int? RoleId { get; set; }

    public bool UserStatus { get; set; }

    public string? NiSize { get; set; }

}
