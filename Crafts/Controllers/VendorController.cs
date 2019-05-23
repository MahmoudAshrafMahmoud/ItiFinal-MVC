using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.SharedModels;
using BL;

namespace Crafts.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
       public ActionResult Addpost()
        {
            var cat =  Vendor.allcatigories();
            
            return View(cat);
        }
        [HttpPost]
        public ActionResult Addpost(ProductModel newproduct , string cat_name )
        {
            var cat = Vendor.allcatigories();
            Vendor.addnewproduct(newproduct , cat_name);
            return View(cat);
        }
        
       
    }
   
}