using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.SharedModels;
using System.Web;

namespace BL
{
    public class Admin
    {

        CraftsEntities context = new CraftsEntities();

        public List<VendorRequest> VendorRequestView()
        {
            var VendorRequest = (from req in context.Request_table
                                 where req.reqState.ToLower() == "pending"
                                 select new VendorRequest
                                 {
                                     req_id = req.Request_Id,
                                     date = req.Request_Date,
                                     Full_Name = req.Full_Name,
                                     National_ID = req.National_ID,
                                     Seller_info = req.Seller_info,
                                     user_id = req.User_Id,
                                     state = req.reqState
                                 }
                                 ).ToList();
                  return VendorRequest;

            
        }



            public void AdminDescision(int id, string status)
            {
                Request_table req = new Request_table();
                req = context.Request_table.Where(x => x.Request_Id == id).FirstOrDefault();
                req.reqState = status;
                int userid = req.User_Id;

                if (req.reqState.ToLower() == "approved")
                {
                    User_table user = new User_table();
                    user = context.User_table.Where(x => x.User_Id == userid).FirstOrDefault();
                    user.Type_id = 2;
                }

                Admin_Req_App_table AdminApprove = new Admin_Req_App_table();
                AdminApprove.Request_Id = id;
                AdminApprove.State = status;

                Admin_table admin = (Admin_table)HttpContext.Current.Session["admin"];
                AdminApprove.Admin_Id = 1;
                //    return VendorRequest;

                context.Admin_Req_App_table.Add(AdminApprove);

            context.SaveChanges();
       }


        //User Table For Admin
        public List<UserModel> UserTable()
        {
            var Usertbl = (from user in context.User_table
                           where user.Type_id == 1
                           orderby user.User_Id descending
                           select new UserModel
                           {
                               userid = user.User_Id,
                               username = user.User_Name,
                               email = user.User_Email,
                               phone = user.PhoneNumber,
                               NationalId = user.SSN,
                               gender = user.Gender
                           }

                         ).Take(7).ToList();

            return Usertbl;
        }

        // Vendor Table For Admin
        public List<UserModel> VendorTable()
        {
            var Vendortbl = (from user in context.User_table
                           where user.Type_id == 2
                           orderby user.User_Id descending
                           select new UserModel
                           {
                               userid = user.User_Id,
                               username = user.User_Name,
                               email = user.User_Email,
                               phone = user.PhoneNumber,
                               NationalId = user.SSN,
                               gender = user.Gender
                           }

                         ).Take(7).ToList();

            return Vendortbl;
        }

        //Product Table

        public List<ProductModel> ProductTable()
        {
            var Producttbl = (from product in context.Product_table
                           join Category in context.Category_table
                           on product.Cat_id equals Category.Cat_Id
                           join vendor in context.User_table
                           on product.Vendor_id equals vendor.User_Id
                           orderby product.Product_Id descending
                           select new ProductModel
                           {
                               Product_Id = product.Product_Id,
                               Product_Name = product.Product_Name,
                               Product_Price = product.Product_Price,
                               CatName = Category.Cat_Name,
                               VendorName = vendor.User_Name,
                               state=product.State
                           }

                         ).Take(7).ToList();

            return Producttbl;
        }

        public List<UserModel> NumOFUsers()
        {
            var Usertbl = (from user in context.User_table
                           join type in context.Type_table
                           on user.Type_id equals type.Type_id
                           orderby user.User_Id descending
                           select new UserModel
                           {
                               userid = user.User_Id,
                               username = user.User_Name,
                               email = user.User_Email,
                               phone = user.PhoneNumber,
                               NationalId = user.SSN,
                               gender = user.Gender,
                               typeName=type.Type_Name
                           }

                         ).Take(9).ToList();

            return Usertbl;
        }



        public List<Product_table> DisplayPendingProducts()
        {

            List<Product_table> PendingProducts = context.Product_table.Where(x => x.State.ToLower() == "pending").ToList();

            return PendingProducts;
        }

        public void AdminApprove(int id, string status)
        {
            Product_table product = new Product_table();
            product = context.Product_table.Where(x => x.Product_Id == id).FirstOrDefault();
            product.State = status;
            context.SaveChanges();
        }


        //Admin Show all orders
        public List<Order_table> Orders()
        {
            return context.Order_table.ToList();
        }


           
        
        public bool AdminLogin(string mail,string password)
        {
            List<Admin_table> admins = context.Admin_table.Where(s => s.Admin_Email == mail && s.Password == password).Select(s => s).ToList();
            if (admins.Count() > 0)
            {
                HttpContext.Current.Session["admin_ID"] = admins.Select(s => s.Admin_Id).First();
                return true;
            }
            else
            {
                return false;
            }
        }



        public List<Message_table> userMessages()
        {
  
            var messages = (from ms in context.Message_table
                     select ms).ToList();
            return messages;
        }



        public void deleteMessage(int id)
        {
            try
            {
                context.Message_table.Remove(context.Message_table.Find(id));
            
            context.SaveChanges();
            }
            catch { }
        }
    }
}