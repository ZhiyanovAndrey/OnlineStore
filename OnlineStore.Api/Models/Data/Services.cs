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
        public string Create(CustomerModel customerModel)
        {


                return DoAction(delegate ()
                {
                    Customer newCustomer = new Customer(customerModel.Lastname, customerModel.Firstname, customerModel.Firdname,
                    customerModel.Phone);
                    _db.Customers.Add(newCustomer);
                    _db.SaveChanges();

                });


        }


        public string Delete(int id)
        {
            Customer customer = _db.Customers.FirstOrDefault(u => u.Customerid == id);
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


        //  Метод получения списка товаров, с возможностью фильтрации по типу товара и/или по наличию на складе и сортировки по цене(возрастанию и убыванию).



        //	Метод получения списка заказов по конкретному клиенту за выбранный временной период, отсортированный по дате создания.

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
