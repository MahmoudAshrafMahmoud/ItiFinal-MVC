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
            int userid = req.User_Id;

            if (req.reqState.ToLower() == "approved")
            {
                User_table user = new User_table();
                user = context.User_table.Where(x => x.User_Id == userid).FirstOrDefault();
                user.Type_id = 2;
            }


            context.SaveChanges();
        }
    }
}
