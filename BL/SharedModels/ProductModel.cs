using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Web;

namespace BL.SharedModels
{
    public class ProductModel
    {
        public int Product_Id { set; get; }
        public string Product_Name { set; get; }
        public string Product_Description { set; get; }
        public double Product_Price { set; get; }
        public byte[] Image { get; set; }

        public int VendorID { set; get; } 

        public int CatID { set; get; }

        public string CatName { set; get; }

        public int Quantity { set; get; }

        public string VendorName { set; get; }
        public HttpPostedFileBase insertedimg { get; set; }
        public int Vendor_id { get; set; }
    }
}         
