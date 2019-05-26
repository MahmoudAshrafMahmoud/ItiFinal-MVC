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
                                     req_id=req.Request_Id,
                                     date=req.Request_Date,
                                     Full_Name=req.Full_Name,
                                     National_ID=req.National_ID,
                                     Seller_info=req.Seller_info,
                                     user_id=req.User_Id,
                                     state=req.reqState
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

            AdminApprove.Admin_Id = admin.Admin_Id;

            context.Admin_Req_App_table.Add(AdminApprove);

            context.SaveChanges();
        }

        //public void display()
        //{
            
        //}

        public List<Product_table> DisplayPendingProducts()
        {

            List<Product_table> PendingProducts = context.Product_table.Where(x=>x.State.ToLower()=="pending").ToList();

            return PendingProducts;
        }

        public void AdminApprove(int id,string status)
        {
            Product_table product = new Product_table();
            product = context.Product_table.Where(x => x.Product_Id == id).FirstOrDefault();
            product.State = status;
            context.SaveChanges();
        }


           
        
        public bool AdminLogin(string mail,string password)
        {
            List<Admin_table>admins=context.Admin_table.Where(s => s.Admin_Email == mail && s.Password == password).Select(s => s).ToList();
            if (admins.Count() > 0)
            {
                HttpContext.Current.Session["admin_ID"]= admins.Select(s=>s.Admin_Id).First();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
