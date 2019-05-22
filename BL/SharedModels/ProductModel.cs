using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BL.SharedModels
{
    class ProductModel
    {
        public int Product_Id { set; get; }
        public string Product_Name { set; get; }
        public string Product_Description { set; get; }
        public double Product_Price { set; get; }
        public byte[] Image { get; set; }
    }
}
