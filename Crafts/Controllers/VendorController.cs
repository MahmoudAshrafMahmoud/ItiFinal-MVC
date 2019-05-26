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
            var cat = Vendor.allcatigories();
            ViewBag.cat = cat;
            var product = Vendor.getproductdetails();
            return View(product);
        }
        [HttpPost]
        public ActionResult Addpost(ProductModel newproduct, string cat_name)
        {
            Vendor.addnewproduct(newproduct, cat_name);
            var cat = Vendor.allcatigories();
            var product = Vendor.getproductdetails();
            ViewBag.cat = cat;
            var product = Vendor.getproductdetails();
            return View(product);
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
     
    }

}