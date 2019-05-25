using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using BL.SharedModels;
using BL;

namespace Admin.Controllers
{
    public class AdminController : Controller
    {
        
        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayVendorRegister()
        {
            Request_table reqobj = new Request_table();
            ViewBag.Request = reqobj;


            BL.Admin admin = new BL.Admin();
            List<VendorRequest> ReqVendor = admin.VendorRequestView();
            return View(ReqVendor);
        }
    }
}