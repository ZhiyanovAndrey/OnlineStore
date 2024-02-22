using System;
using System.Collections.Generic;

namespace OnlineStore.Api.Models;

public partial class Customer
{
    public int Customerid { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string? Firdname { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
