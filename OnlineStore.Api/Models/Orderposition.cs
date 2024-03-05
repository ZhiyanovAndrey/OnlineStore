using OnlineStore.Models;

namespace OnlineStore.Api.Models;

public partial class Orderposition
{
    public int Orderpositionsid { get; set; }

    public int Orderid { get; set; }

    public int Productid { get; set; }

    public decimal Unitprice { get; set; }

    public int Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;


    public Orderposition(OrderpositionModel orderModel)
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
