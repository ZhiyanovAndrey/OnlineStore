using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Api.Models.Data
{
    public class OrderService
    {
        private readonly OnlineStoreContext _db;

        public OrderService(OnlineStoreContext db)
        {
                _db = db;
        }

        //	Метод добавления заказа
        public async Task<Order> CreateOrderAsync(OrderModel orderModel)
        {

            if (await _db.Customers.FirstOrDefaultAsync(c => c.Customerid == orderModel.Customerid) == null)
            {

                throw new Exception($"Пользователь {orderModel.Customerid} не найден");
            }


            Order newOrder = new Order(orderModel);
            await _db.Orders.AddAsync(newOrder);
            await _db.SaveChangesAsync();

            return newOrder;

        }



        /* 5.Метод формирования заказа с проверкой наличия требуемого количества товара на складе,
         * а также уменьшение доступного количества товара на складе в БД в случае успешного создания заказа.*/
        public string CreateOrderPosition(OrderPositionModel orderPositionModel)
        {

            Order order = _db.Orders.FirstOrDefault(o => o.Orderid == orderPositionModel.Orderid);

            Product product = _db.Products.FirstOrDefault(p => p.Productid == orderPositionModel.Productid);

            if (order != null)
            {
                if (product != null)
                {
                    // выберем продукт по Productid и умножим цену на кол-во orderPositionModel.Quantity;
                    orderPositionModel.Unitprice = (decimal)product.Unitprice * orderPositionModel.Quantity;


                    // выберем продукт проверим количество и убавим из него Quantity
                    if (product.Unitsinstock >= orderPositionModel.Quantity)
                    {
                        var a = product.Unitsinstock;
                        var b = orderPositionModel.Quantity;
                        product.Unitsinstock = a - b;

                        //return DoAction(delegate ()
                        //{

                        Orderposition newOrderPosition = new Orderposition(orderPositionModel);
                        _db.Orderpositions.Add(newOrderPosition);
                        _db.Products.Update(product);
                        _db.SaveChanges();


                        //});
                    }
                    return $"Продукт в количестве {orderPositionModel.Quantity} отсутствует";

                }
                return $"Продукт с номером {orderPositionModel.Productid} не найден";
            }
            return $"Заказ с номером {orderPositionModel.Orderid} не найден";
        }


        public async Task<Order?> DeleteOrder(int id)
        {
            Order? order = await _db.Orders.FirstOrDefaultAsync(c => c.Orderid == id);
            if (order != null)
            {

                _db.Orders.Remove(order);
                await _db.SaveChangesAsync();

            }
            return order; //$"Заказ с номером {id} не найден";
        }



        //	получение всех заказов
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _db.Orders.ToListAsync();
        }

    }
}
