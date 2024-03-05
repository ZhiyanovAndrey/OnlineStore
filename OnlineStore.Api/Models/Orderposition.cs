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


    public Orderposition() { }

    public Orderposition(OrderPositionModel orderPositionModel)
    {
        Orderpositionsid = orderPositionModel.Orderpositionsid;
        Orderid = orderPositionModel.Orderid;
        Productid = orderPositionModel.Productid;
        Unitprice = orderPositionModel.Unitprice;
        Quantity = orderPositionModel.Quantity;

    }

    public OrderPositionModel ToDto()
    {
        return new OrderPositionModel()
        {
            Orderpositionsid = this.Orderpositionsid,
            Orderid = this.Orderid,
            Productid = this.Productid,
            Unitprice = this.Unitprice,
            Quantity = this.Quantity,

        };
    }

}
