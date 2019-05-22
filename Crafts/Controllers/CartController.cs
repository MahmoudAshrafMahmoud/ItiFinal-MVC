using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crafts.Controllers
{
    public class CartController : Controller
    {

        CraftsEntities Context = new CraftsEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
    }
}