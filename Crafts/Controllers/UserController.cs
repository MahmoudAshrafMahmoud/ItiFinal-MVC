using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.SharedModels;

namespace Crafts.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult DisplayUserPage(int id)
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
            return View(ViewOrdersDetails);
        }

        public ActionResult Be_Vendor(int id)
        {
            return PartialView();
        }


    }
}