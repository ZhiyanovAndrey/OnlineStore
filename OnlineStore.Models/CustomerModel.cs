using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class CustomerModel
    {

        public int Customerid { get; set; }

        [MaxLength(20, ErrorMessage = "Можно ввести не более 20 символов")]
        [RegularExpression(pattern: @"^[А-ЯЁа-яё']*((-| )[А-ЯЁа-яё']*)?$", ErrorMessage = "Можно ввести только русские буквы")]

        public string Lastname { get; set; } = null!;
        [MaxLength(20, ErrorMessage = "Можно ввести не более 20 символов")]
        [RegularExpression(pattern: @"^[А-ЯЁа-яё']*((-| )[А-ЯЁа-яё']*)?$", ErrorMessage = "Можно ввести только русские буквы")]
        public string Firstname { get; set; } = null!;

        [MaxLength(20, ErrorMessage = "Можно ввести не более 20 символов")]
        [RegularExpression(pattern: @"^[А-ЯЁа-яё']*((-| )[А-ЯЁа-яё']*)?$", ErrorMessage = "Можно ввести только русские буквы")]
        public string? Firdname { get; set; }

        [RegularExpression (pattern: @"^\d{10}$", ErrorMessage = "Номер телефона должен содержать только 10 цифр")]
        public string Phone { get; set; }

        public List<OrderpositionModel> AllOrder { get; set; } = new List<OrderpositionModel>();

        public CustomerModel() { }

        public CustomerModel(string lastName, string firstname, string thirdName, string phone)
        {

            Lastname = lastName;
            Firstname = firstname;
            Firdname = thirdName;
            Phone = phone;


        }

    }
}
