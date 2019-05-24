using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BL.SharedModels;

 namespace BL
{
   public class Order
    {

        CraftsEntities context = new CraftsEntities();
        public List<MyOrdersModel> ShowMyOrders(int user_id)
        {
            var query = (from viewOrder in context.Order_table
                         where viewOrder.User_Id == user_id
                         select new MyOrdersModel
                         {
                             OrderId = viewOrder.Order_Id,
                             Address = viewOrder.Order_Address,
                             Date = viewOrder.Order_Date,
                             Phone = viewOrder.Order_Phone,
                             Total = viewOrder.Expected_Price

                         }

                            ).ToList();

            return query;
        }


        public List<MyOrdersModel> ShowMyOrdersDetails(int order_id)
        {
            var query = (from viewOrderdetails in context.OrderDetails_table
                         join
                         viewselectedproducts in context.Product_table
                         on viewOrderdetails.Pro_Id equals viewselectedproducts.Product_Id
                         where viewOrderdetails.Order_Id == order_id
                         select new MyOrdersModel
                         {
                             ProductName = viewselectedproducts.Product_Name,
                            Quantity= viewOrderdetails.Quantity,
                             Productprice = viewselectedproducts.Product_Price,
                             Status = viewOrderdetails.Approval
                            

                         }

                            ).ToList();

            return query;
        }

    }
}
