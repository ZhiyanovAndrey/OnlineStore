﻿using Microsoft.AspNetCore.Mvc;
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
        public bool Create(CustomerModel customerModel)
        {


            return DoAction(delegate ()
            {
              
                    Customer newCustomer = new Customer(customerModel.Lastname, customerModel.Firstname, customerModel.Firdname,
                    customerModel.Phone);
                    _db.Customers.Add(newCustomer);
                    _db.SaveChanges();
                            
            });
        }


        //	Метод получения клиента по номеру телефона.

        public UserModel Get(int id)
        {
            User user = _db.Users.FirstOrDefault(u => u.Id == id);
            return user?.ToDto(); // проверка на null, вернет null не будет пытаться вызвать ToDto().
        }


        //  Метод получения списка товаров, с возможностью фильтрации по типу товара и/или по наличию на складе и сортировки по цене(возрастанию и убыванию).
        //	
        //	
        //	Метод получения списка заказов по конкретному клиенту за выбранный временной период, отсортированный по дате создания.

        // Метод формирования заказа с проверкой наличия требуемого количества товара на складе, а также уменьшение доступного количества товара на складе в БД в случае успешного создания заказа.

        //	Прочие методы, на усмотрение кандидата.

        //public bool Delete(int id)
        //{
        //    User user = _db.Users.FirstOrDefault(u => u.Id == id);
        //    if (user != null)
        //    {
        //        return DoAction(delegate ()
        //        {
        //            _db.Users.Remove(user);
        //            _db.SaveChanges();

        //        });

        //    }
        //    return false;
        //}

        private bool DoAction(Action action)
        {
            try
            {
                action.Invoke(); // вызываем методы сообщенные с делегатом
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}