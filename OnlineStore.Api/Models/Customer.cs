using OnlineStore.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Api.Models;

public partial class Customer
{
    public int Customerid { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string? Firdname { get; set; }

    [Phone]
    public string Phone { get; set; }

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


    }




}
