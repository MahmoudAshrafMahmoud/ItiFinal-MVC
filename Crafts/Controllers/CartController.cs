using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using DAL;

namespace Crafts.Controllers
{
    public class CartController : Controller
    {
        List<BL.Order> newd = new List<BL.Order>();


        // GET: Cart
        public ActionResult Index()
        {
           
            return View();
        }
    }
}