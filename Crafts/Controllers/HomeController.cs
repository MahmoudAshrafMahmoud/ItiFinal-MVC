using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;

namespace Crafts.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            getTopPro();
            getTrendVendors();
            return View();
        }
       
        public void getTrendVendors()
        {
            BL.Vendor VendorLogic = new BL.Vendor();
            List<User_table> selectedVendor = VendorLogic.getTopTrendVendors();
            ViewBag.selectedVendor = selectedVendor;
            
        }

        public void getTopPro()
        {
            BL.Product productLogic = new BL.Product();
            List<Product_table> selectedPro = productLogic.getTopSellingProduct();
            ViewBag.selectedPro = selectedPro;
        }


    }
}