using BL;
using BL.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crafts.Controllers
{
    public class VendorController : Controller
    {

        // GET: Vendor orders 
        // we remove v_id =1 (default value)
        public ActionResult VendorOrders(int v_id=1)
        {
            Vendor myvendor = new Vendor();
            var orders = myvendor.VendorOrdersReview(v_id);
            ViewBag.orders = orders;
            return View();
        }


        [HttpGet]
        public ActionResult Addpost()
        {
          int userid= int.Parse(Session["User_Id"].ToString());
            var cat = Vendor.allcatigories();
            ViewBag.cat = cat;
            var Approvedproduct = Vendor.ApprovedProducts( userid);
            var Pendingproduct = Vendor.pendingProducts(userid);
            ViewBag.Pendingproduct = Pendingproduct;
            var Rejectedproduct = Vendor.rejectedProducts(userid);
            ViewBag.Rejectedproduct = Rejectedproduct;
            return View(Approvedproduct);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductModel newproduct, string cat_name)
        {
            int userid = int.Parse(Session["User_Id"].ToString());
            Vendor.addnewproduct(newproduct, cat_name, userid);
            return RedirectToAction("Addpost");
        }

        //Vendor submit orders 
        public ActionResult SubmitOrder(int id)
        {
            Vendor myvendor = new Vendor();
            myvendor.AcceptOrder(id);
            return RedirectToAction("VendorOrders");
        }
        public ActionResult RejectOrder(int id)
        {
            Vendor myvendor = new Vendor();
            myvendor.RejectOrder(id);
            return RedirectToAction("VendorOrders");
        }
        public PartialViewResult Search(string search,int id)
        {
            Vendor myvendor = new Vendor();
            ViewBag.orders=myvendor.SearchOrder(search, id);
            return PartialView();
        }

        Vendor vendorlogic = new Vendor();


        [HttpPost]
        public PartialViewResult follow(int vendor_id)
        {
            if (Session["user"] != null)
            {

                bool result = vendorlogic.followvendor((int)Session["User_Id"], vendor_id);
                return PartialView();
            }
            return PartialView("errorview");
        }

        [HttpPost]
        public PartialViewResult unfollow(int vendor_id)
        {
            bool result = vendorlogic.unfollowvendor((int)Session["User_Id"], vendor_id);
            return PartialView();
        }

    }

}