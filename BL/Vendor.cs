﻿using BL.SharedModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Vendor
    {
        CraftsEntities context = new CraftsEntities();

        //Get Vendor orders
        public  List<OrderModel> VendorOrdersReview(int v_id)
        {
            var query = (from v in context.OrderDetails_table
                         join s in context.Order_table
                         on v.Order_Id equals s.Order_Id
                         join l in context.Product_table
                         on v.Pro_Id equals l.Product_Id
                         where v.Approval=="pending" && l.Vendor_id==v_id
                         select new OrderModel()
                         {
                             Product_Name = l.Product_Name,
                             Product_Price = l.Product_Price,
                             Quantity = v.Quantity,
                             Order_id = s.Order_Id,
                             Vendor_id = l.Vendor_id,
                             Approval = v.Approval,
                             Order_Date = s.Order_Date.ToString(),
                             Order_Address = s.Order_Address,
                             Order_Phone = s.Order_Phone,
                             Expected_Price = (float)s.Expected_Price,
                             Image = (byte[])l.Image
                         }).ToList();
                                             
            return query;
        }
    }
}
