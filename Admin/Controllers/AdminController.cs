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
        BL.Admin admin = new BL.Admin();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayVendorRegister()
        {
            Request_table reqobj = new Request_table();
            ViewBag.Request = reqobj;


            List<VendorRequest> ReqVendor = admin.VendorRequestView();
            return View(ReqVendor);
        }

        //public ActionResult VendorCount()
        //{

        //    ViewBag.count = admin.VendorRequestView().Count();
        //    return View();
        //}

        //admin accept or refuse vendors
        public ActionResult VendorDescision(int id, string status)
        {
            admin.AdminDescision(id, status);
            return RedirectToAction("DisplayVendorRegister");
        }

        //public ActionResult ProductsRequestsDisplay()
        //{
        //    return View();

        //}


        public ActionResult AdminApproveProducts()
        {
            BL.Admin admin = new BL.Admin();

            List<Product_table> product = admin.DisplayPendingProducts();
            return View(product);
        }
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AfterAdminLogin(string mail, string password)
        {
            BL.Admin myadmin = new BL.Admin();
            bool loginStatus = myadmin.AdminLogin(mail, password);
            if (loginStatus == true)
            {
                return View("AdminLogin");
            }
            else
            {
                return View("AfterAdminLogin");
            }

        }

        public ActionResult AdminApproval(int id, string status)
        {
            admin.AdminApprove(id, status);
            return RedirectToAction("AdminApproveProducts");
        }
             
            
    }

   

    }
