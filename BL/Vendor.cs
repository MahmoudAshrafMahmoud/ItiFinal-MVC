using BL.SharedModels;
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
                             Image = (byte[])l.Image,
                             OrderDetail_Id=v.OrderDetail_Id
                         }).ToList();
                                             
            return query;
        }

        public bool AcceptOrder(int id)
        {
            (from p in context.OrderDetails_table
             where p.OrderDetail_Id == id
             select p).ToList().ForEach(x => x.Approval = "yes");

            context.SaveChanges();
            return true;
        }

        //select TOP 4 User_Id,Rating from User_table
        //ORDER BY Rating DESC




        List<User_table> selectVendor = new List<User_table>();

        public List<User_table> getTopTrendVendors()
        {
            var result = context.User_table 

              .OrderByDescending(g => g.Rating)
               .Take(5)                      
              .Select(                            
              r => new { User_Id = r.User_Id, Name = r.FName , Image = r.ProfilePicture}).ToList();


            for (int i = 0; i < result.Count; i++)
            {
                int x = result[i].User_Id;
                selectVendor.Add(context.User_table.FirstOrDefault(s => s.User_Id == x));
            }


            return selectVendor;
        }
    }
}
