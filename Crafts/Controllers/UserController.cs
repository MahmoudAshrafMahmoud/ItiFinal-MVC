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

            return View();
        }
        [HttpPost]
        public ActionResult login(string User_Email, string Password)
        {
            User_table user = ul.login(User_Email, Password);
            if (user != null)
            {
                Session.Add("user", user);
                return RedirectToAction("Home", "User");

            }
            else
            {
                ViewBag.NotValidUser = "User Name OR Password is Incorrect";
                return View("login");
            }

        }
    }
}