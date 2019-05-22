using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.SharedModels
{
    class OrderModel
    {
        public string Pro_Name { set; get; }
        public string Pro_Desc { set; get; }
        public double Price { set; get; }
        public string Cat_Name { set; get; }
        public int Quantity { set; get; }
        public int Order_id { set; get; }
        public int Vendor_id { set; get; }
    }
}
