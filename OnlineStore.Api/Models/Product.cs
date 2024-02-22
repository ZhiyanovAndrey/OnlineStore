using System;
using System.Collections.Generic;

namespace OnlineStore.Api.Models;

public partial class Product
{
    public int Productid { get; set; }

    public int Categoryid { get; set; }

    public string Productname { get; set; } = null!;

    public decimal? Unitprice { get; set; }

    public int? Unitsinstock { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Orderposition> Orderpositions { get; set; } = new List<Orderposition>();
}
