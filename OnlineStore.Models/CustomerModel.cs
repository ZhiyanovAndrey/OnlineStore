namespace OnlineStore.Models
{
    public class CustomerModel
    {
       
            public int Customerid { get; set; }

            public string Lastname { get; set; } = null!;

            public string Firstname { get; set; } = null!;

            public string? Firdname { get; set; }

            public string? Phone { get; set; }

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
}