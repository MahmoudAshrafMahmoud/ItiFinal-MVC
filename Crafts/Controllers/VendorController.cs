using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;

namespace Crafts.Controllers
{
    public class VendorController : Controller
    {
        
        // GET: Vendor
        public ActionResult VendorOrders(int v_id)
        {
            Vendor myvendor = new Vendor();
            var orders = myvendor.VendorOrdersReview(v_id);

            return View();
        }
    }
}