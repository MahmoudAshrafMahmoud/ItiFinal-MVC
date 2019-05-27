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
            BL.Admin admin = new BL.Admin();
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
                return View("AfterAdminLogin"); 
            }
            else
            {
                return View("AdminLogin");
            }

        }
        public ActionResult userMessages()
        {

            List<Message_table> messages = admin.userMessages();
            
            return View(messages);
        }

        public ActionResult AdminApproval(int id, string status)
        {
            admin.AdminApprove(id, status);
            return RedirectToAction("AdminApproveProducts");
        }

        
        public ActionResult deleteMessage(int id)
        {

            admin.deleteMessage(id);

            return View("userMessages");
        }


        public ActionResult AdminShowOrders()
        {
            List<Order_table> orders = admin.Orders();
            return View(orders);
        }


        public ActionResult TablesView()
        {
            ViewBag.UserTable = admin.UserTable();
            ViewBag.VendorTable = admin.VendorTable();
            ViewBag.ProductTable = admin.ProductTable();

            return View();
        }

             
            
    }

   

    }
