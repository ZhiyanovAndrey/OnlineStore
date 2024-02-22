using System;
using System.Collections.Generic;

namespace OnlineStore.Api.Models;

public partial class Order
{
    public int Orderid { get; set; }

    public int Customerid { get; set; }

    public DateOnly? Orderdate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Orderposition> Orderpositions { get; set; } = new List<Orderposition>();
}
