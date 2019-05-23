using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BL.SharedModels
{
   public class CartListModel
    {
        public Product_table ProductData { get; set; }
        public int ProductQty { get; set; }
        public double ProductTotalPrice { get; set; }
    }
}
