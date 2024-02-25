using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OnlineStore.Api.Models;

public partial class Customer
{
    public int Customerid { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string? Firdname { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public Customer() { }

    public Customer(string lastName, string firstname, string thirdName, string phone)
    {
        Lastname = lastName;
        Firstname = firstname;
        Firdname = thirdName;
        Phone = phone;
    }

    public Customer(CustomerModel model)
    {
        Lastname = model.Lastname;
        Firstname = model.Firstname;
        Firdname = model.Firdname;
        Phone = model.Phone;
    }

    // применим патерн DTO
    public CustomerModel ToDto()
    {
        return new CustomerModel()
        {
            Customerid = this.Customerid,
            Lastname = this.Lastname,
            Firstname = this.Firstname,
            Firdname = this.Firdname,
            Phone = this.Phone

        };

        // конструктор, чтобы сразу задать пользователя статус задан по умолчанию
        //    public User(string sname, string name, string email, string pass, string phone = null,
        //UserStatus status = UserStatus.User, byte[] photo = null)
        //    {
        //        Surname = sname;
        //        Name = name;
        //        Email = email;
        //        Password = pass;
        //        Phone = phone;
        //        Status = status;
        //        Photo = photo;
        //        RegistrationDate = DateTime.Now;

        //    }

        //    public User (UserModel model)
        //    {

        //        Surname = model.Surname;
        //        Name = model.Name;
        //        Email = model.Email;
        //        Password = model.Password;
        //        Phone = model.Phone;
        //        Status = model.Status;
        //        Photo = model.Photo;
        //        RegistrationDate = model.RegistrationDate;

        //    }

        //    // применим патерн DTO
        //    public UserModel ToDto()
        //    {
        //        return new UserModel()
        //        {
        //            Id = this.Id, // через This т.к в usermodel тоже id
        //            Surname = this.Surname,
        //            Name = this.Name,
        //            Email = this.Email,
        //            Password = this.Password,
        //            Phone = this.Phone,
        //            Status = this.Status,
        //            Photo = this.Photo,
        //            RegistrationDate = this.RegistrationDate

        //        };
        //    }
        //}


    }
}
