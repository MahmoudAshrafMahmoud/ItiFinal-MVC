﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BL.SharedModels
{
   public class CartModel
    {
        public List<CartListModel> CartItem { get; set; }
        public int UserID { get; set; }
        public double OrderTotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
