using OnlineStore.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Api.Models;

public partial class Order
{
    public int Orderid { get; set; }

    public int Customerid { get; set; }

    //[Column(TypeName = "date")]
    public DateTime? Orderdate { get; set; }    

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Orderposition> Orderpositions { get; set; } = new List<Orderposition>();

    public Order()
    {
        //Orderdate = DateTime.Now;
        //Orderdate = DateOnly.FromDateTime(DateTime.Now);
    }


    public Order(OrderModel orderModel)
    {
        Orderid = orderModel.Orderid;
        Customerid = orderModel.Customerid;
        Orderdate = orderModel.Orderdate;


    }

    public OrderModel ToDto()
    {
        return new OrderModel()
        {
            Orderid = this.Orderid,
            Customerid = this.Customerid,
            Orderdate = this.Orderdate
        };
    }
}




