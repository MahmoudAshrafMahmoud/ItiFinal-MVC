using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BL.SharedModels;

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
            context.SaveChanges();

        }

        public List<Product_table> ProductsRequestsDisplay()
        {
            return  context.Product_table.Where(s => s.State == "pending").ToList();
            
        
        }
    }
}
