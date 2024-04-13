using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Api.Models.Data
{
    public class Services
    {

        private readonly OnlineStoreContext _db;

        public Services(OnlineStoreContext db)
        {
            _db = db;
        }


        //	Метод добавления клиента.
        public async Task<Customer> CreateCustomerAsync(CustomerModel customerModel)
        {


          
                Customer newCustomer = new Customer(customerModel.Lastname, customerModel.Firstname, customerModel.Firdname,
                customerModel.Phone);
                await _db.Customers.AddAsync(newCustomer);
                await _db.SaveChangesAsync();

            return newCustomer;


        }

        //	Метод добавления заказа
        public async Task<Order> CreateOrder(OrderModel orderModel)
        {

            if (await _db.Customers.FirstOrDefaultAsync(c => c.Customerid == orderModel.Customerid) == null)
            {

                throw new Exception("Пользователь не найден");
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





        //	2.Метод получения клиента по номеру телефона.

        public CustomerModel GetCustomerByPhone(string phone)
        {

            var customer = _db.Customers.FirstOrDefault(c => c.Phone == phone);
            return customer?.ToDto();
        }


        /*  3.Метод получения списка товаров, с возможностью фильтрации по типу товара 
         *  и/или по наличию на складе и сортировки по цене(возрастанию и убыванию).*/

        public async Task<IEnumerable<Product>> GetProductsWithSort(string sortOrder)
        {
            var products = _db.Products.Select(u => u);


            switch (sortOrder)
            {
                case "ProductName_desc":
                    products = products.OrderByDescending(p => p.Productname);
                    break;
                case "ProductName":
                    products = products.OrderBy(p => p.Productname);
                    break;
                case "ProductPrice_desc":
                    products = products.OrderByDescending(p => p.Unitprice);
                    break;
                case "ProductPrice":
                    products = products.OrderBy(p => p.Unitprice);
                    break;
                case "UnitsinstockFilter":
                    products = products.Where(p => p.Unitsinstock > 0);
                    break;
                default:
                    products = products.OrderBy(p => p.Productname);
                    break;
            }


            return await products.ToListAsync();
        }

        // 4.Метод получения списка заказов по конкретному клиенту за выбранный временной период, отсортированный по дате создания.
        public async Task<IEnumerable<OrderModel>> GetOrderByCustomer(int CustomerId, DateTime dateStart, DateTime dateEnd)
        {

            //if (_db.Customers.FirstOrDefault(c => c.Customerid == CustomerId) != null)
            //{
            var orders = _db.Orders.Include(o => o.Customer)
                .Where(c => c.Customerid == CustomerId)
                .Where(o => o.Orderdate >= dateStart.Date && o.Orderdate <= dateEnd.Date)
                .OrderBy(o => o.Orderdate);

            return await orders.Select(d => d.ToDto()).ToListAsync();
            //}
            //return $"Пользователь с номером {CustomerId} не найден";
        }





        public string DeleteCustomer(int id)
        {
            Customer customer = _db.Customers.FirstOrDefault(c => c.Customerid == id);
            if (customer != null)
            {
                return DoAction(delegate ()
                {
                    _db.Customers.Remove(customer);
                    _db.SaveChanges();

                });

            }
            return $"Пользователь с номером {id} не найден";
        }


        public async Task<Order?> DeleteOrder(int id)
        {
            Order? order = await _db.Orders.FirstOrDefaultAsync(c => c.Orderid == id);
            if (order != null)
            {
                //try
                //{
                _db.Orders.Remove(order);
                await _db.SaveChangesAsync();
                //return "Выполнено";
                //}
                //catch (Exception ex)
                //{
                //    return ex.Message;
                //}


            }
            return order; //$"Заказ с номером {id} не найден";
        }



        private string DoAction(Action action)
        {
            try
            {
                action.Invoke();
                return $"Выполнено";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
