using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.SharedModels
{
    public class OrderModel
    {
        public string Product_Name { set; get; }
        public double Product_Price { set; get; }
        public int Quantity { set; get; }
        public int Order_id { set; get; }
        public int Vendor_id { set; get; }
        public string Approval { get; set; }
        public string Order_Date { get; set; }
        public string Order_Address { get; set; }
        public int Order_Phone { get; set; }
        public float Expected_Price { get; set; }
        public byte[] Image { get; set; }
        public int OrderDetail_Id { get; set; }
    }
}
