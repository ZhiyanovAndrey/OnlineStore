﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using System.Text.RegularExpressions;

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
        public string CreateCustomer(CustomerModel customerModel)
        {


            return DoAction(delegate ()
            {
                Customer newCustomer = new Customer(customerModel.Lastname, customerModel.Firstname, customerModel.Firdname,
                customerModel.Phone);
                _db.Customers.Add(newCustomer);
                _db.SaveChangesAsync();
            });



        }

        //	Метод добавления заказа
        public string CreateOrder(OrderModel orderModel)
        {

               if (_db.Customers.FirstOrDefault(c => c.Customerid == orderModel.Customerid) != null) 
                {

            return DoAction(delegate()
            {
                
                Order newOrder = new Order(orderModel);
                _db.Orders.Add(newOrder);
                _db.SaveChangesAsync();
            });
                }
                return $"Пользователь с номером {orderModel.Customerid} не найден";



        }

        //	Метод удаления клиента
        public string Delete(int id)
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


        //	Метод получения клиента по номеру телефона.

        public CustomerModel GetCustomerByPhone(string phone)
        {
            Customer customer = _db.Customers.FirstOrDefault(c => c.Phone == phone);
            return customer?.ToDto();
        }


        ////  Метод получения списка товаров, с возможностью фильтрации по типу товара и/или по наличию на складе и сортировки по цене(возрастанию и убыванию).
        //public CustomerModel GetProduct(string phone)
        //{
        //    Product product = _db.Customers.FirstOrDefault(c => c.Phone == phone);
        //    return customer?.ToDto();
        //}
        public IEnumerable<CustomerModel> GetProducts(string sortOrder) // меняем IEnumerable на Task для использования ToListAsync() вместо ToList()
        {
            return _db.Customers.Select(c => c.ToDto()).ToList();
        }


     //	Метод получения списка заказов по конкретному клиенту за выбранный временной период, отсортированный по дате создания.
        public async Task<IEnumerable<OrderModel>> GetOrderByCustomer(int CustomerId, DateTime dateStart, DateTime dateEnd)
        {
            var orders = _db.Orders.Include(o => o.Customer)
                .Where(c => c.Customerid == CustomerId)
                //.Where(o => o.Orderdate >= dateStart.Date && o.Orderdate <= dateEnd.Date)
                .OrderBy(o => o.Orderdate);
         
            return await orders.Select(d => d.ToDto()).ToListAsync();
        }


        //[HttpGet]
        //public async Task<IEnumerable<UserModel>> GetUsers() // меняем IEnumerable на Task для использования ToListAsync() вместо ToList()
        //{
        //    return await _db.Users.Select(u => u.ToDto()).ToListAsync();
        //}


   

        // Метод формирования заказа с проверкой наличия требуемого количества товара на складе, а также уменьшение доступного количества товара на складе в БД в случае успешного создания заказа.

        //	Прочие методы, на усмотрение кандидата.


        //// валидация номера телефона
        //        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        //        {
        //            Regex regex = new Regex("[^0-9]+");
        //            e.Handled = regex.IsMatch(e.Text);
        //        }



        private string DoAction(Action action)
        {
            try
            {
                action.Invoke(); // вызываем методы сообщенные с делегатом
                return $"Выполнено";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
