using OnlineStore.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Api.Models;

public partial class Order
{
    public int Orderid { get; set; }

    public int Customerid { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Orderdate { get; set; }    

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Orderposition> Orderpositions { get; set; } = new List<Orderposition>();

    public Order() { }

    public Order(OrderpositionModel orderModel)
    {
        Orderid = orderModel.Orderid;
        Customerid = orderModel.Customerid;
        Orderdate = DateTime.Now;


    }

    public OrderpositionModel ToDto()
    {
        return new OrderpositionModel()
        {
            Orderid = this.Orderid,
            Customerid = this.Customerid,
            Orderdate = this.Orderdate
        };
    }
}




