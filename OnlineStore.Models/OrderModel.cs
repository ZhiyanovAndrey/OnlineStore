using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class OrderModel
    {
        public int Orderid { get; set; }

        public int Customerid { get; set; }

        public DateTime? Orderdate { get; set; }  

        //public List<Orderposition> Orderpositions { get; set; } = new List<Orderposition>();
    }
}
