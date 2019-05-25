using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using BL.SharedModels;
using DAL;

namespace Crafts.Controllers
{
    public class UserController : Controller
    {
        User ul = new User();
        // GET: User
        public ActionResult Home()
        {
            if (Session["user"] != null)
            {

                List<ProductModel> topSeller = ul.topSellerSup();
                ViewBag.topSeller = topSeller;
                ViewBag.lastAdded = ul.lastAdded();
                return View();
            }
            else
            {
                return RedirectToAction("login", "User");
            }
        }

        [HttpGet]
        public ActionResult login()
        {
            ViewBag.ID = id;
            return View();
        }

        public ActionResult Show_Orders(int id)
        {
            BL.Order order = new BL.Order();

            List<MyOrdersModel> ViewOrders = order.ShowMyOrders(id);

            return View(ViewOrders);

        }

        public ActionResult ViewDtails(int id)
        {
            BL.Order order = new BL.Order();

            List<MyOrdersModel> ViewOrdersDetails = order.ShowMyOrdersDetails(id);
            return PartialView(ViewOrdersDetails);
        }

        public ActionResult Be_Vendor(int id)
        {
            return View();
        }

        //Vendor Register
        [HttpPost]
        public ActionResult VendorRegister(string FullName, int NationalId, string Bio)
        {
            User_table USer = (User_table)Session["user"];

            int id= USer.User_Id ;
            BL.User user = new BL.User();
            ViewBag.message = user.Vendor_Register(FullName, NationalId, Bio,id);
            return PartialView();
        }




        [HttpPost]
        public ActionResult login(string User_Email, string Password)
        {
            User_table user = ul.login(User_Email, Password);
            if (user != null)
            {
                Session.Add("user", user);
                Session.Add("userID", user.User_Id);
                if (Session["checkOutRequest"] != null)
                {
                    return RedirectToAction("cartDisplay", "Cart");
                }
                else
                {
                    return RedirectToAction("Home", "User");
                }
            }
            else
            {
                ViewBag.NotValidUser = "User Name OR Password is Incorrect";
                return View("login");
            }

        }

        public ActionResult logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult RegisterNewUser()
        {
            return View();
        }

    }
}