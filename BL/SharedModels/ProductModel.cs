using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BL.SharedModels
{
    public class ProductModel
    {
        public int Product_Id { set; get; }
        [Required(ErrorMessage ="Product Name is required")]
        public string Product_Name { set; get; }
        [Required(ErrorMessage = "Product description is required")]
        public string Product_Description { set; get; }
        [Required(ErrorMessage = "Product Price is required")]
        public double Product_Price { set; get; }
        public byte[] Image { get; set; }

        public int VendorID { set; get; } 

        public int CatID { set; get; }
        [Required(ErrorMessage = "Catigory Name is required")]
        public string CatName { set; get; }

        public int Quantity { set; get; }

        public string VendorName { set; get; }
        [Required(ErrorMessage = "image is required")]
        public HttpPostedFileBase insertedimg { get; set; }
        public int Vendor_id { get; set; }
        public string Catigory_name { get; set; }

    }
}         
