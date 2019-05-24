using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.SharedModels
{
    public class MyOrdersModel
    {
        public int OrderId { set; get; }
        public DateTime Date { set; get; }
        public string Address { set; get; }

        public int Phone { set; get; }

        public double Total { set; get; }

        public string Status { set; get; }

        public int Quantity { set; get; }

        public string ProductName { set; get; }

        public double Productprice { set; get; }

    }
}
