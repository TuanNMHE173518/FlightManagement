using System;
using System.Collections.Generic;

namespace BussinessObjects;

public partial class AccountMember
{
    public int AccountId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public string? Role { get; set; }

    public bool? Enable { get; set; }
}
