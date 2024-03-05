using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class OrderPositionModel
    {
        public int Orderpositionsid { get; set; }

        public int Orderid { get; set; }

        public int Productid { get; set; }

        public decimal Unitprice { get; set; }

        public int Quantity { get; set; }
    }
}
