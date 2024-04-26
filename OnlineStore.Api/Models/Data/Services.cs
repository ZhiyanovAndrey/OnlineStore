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


            public async Task<Customer> CreateCustomerAsync(CustomerModel customerModel)
        {

            Customer newCustomer = new Customer(customerModel);
            await _db.Customers.AddAsync(newCustomer);
            await _db.SaveChangesAsync();

            return newCustomer;


        }

        public async Task<Customer?> DeleteCustomerAsync(int id)
        {
            Customer? customer = await _db.Customers.FirstOrDefaultAsync(c => c.Customerid == id);
            if (customer != null)
            {
                _db.Customers.Remove(customer);
                await _db.SaveChangesAsync();
            }
            return customer;
        }


        //	2.Метод получения клиента по номеру телефона.

        public async Task<CustomerModel> GetCustomerByPhoneAsync(string phone)
        {

            Customer? customer = await _db.Customers.FirstOrDefaultAsync(c => c.Phone == phone);
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
