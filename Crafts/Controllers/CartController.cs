using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using DAL;
using BL.SharedModels;

namespace Crafts.Controllers
{
    public class CartController : Controller
    {
        BL.Product productLogic = new BL.Product();

        // GET: Cart
        public ActionResult Index()
        { 
            return View();
        }

        public ActionResult ShowProductToTest()
        {
            List<BL.SharedModels.ProductModel> testproducts = productLogic.productsForTestingCartMethods();
            ViewBag.productstest = testproducts;
            return View();
        }

        BL.Cart CartLogic = new BL.Cart();
        public PartialViewResult Add(int ID)
        {
            CartLogic.AddItemToCart(ID);
            return PartialView("_shoppingCart");
        }
        public ActionResult cartDisplay()
        {
            if (CartLogic.viewMyCart() != null)
            {
                CartModel MyCart = CartLogic.viewMyCart();
                ViewBag.cart = MyCart;
                ViewBag.CartItems = MyCart.CartItem;
                return View("cartDisplay");
            }
            else
            {
                return View("cartDisplayEmpty");

            }

        }
        public PartialViewResult Increase(int ID)
        {
            CartLogic.increase(ID);
            return PartialView("_UpdateQuantityText");

        }
        public PartialViewResult Decrease(int ID)
        {
            CartLogic.decrease(ID);
            return PartialView("_UpdateQuantityText");

        }
        public ActionResult checkout(int phone, string address)
        {
            Session.Add("checkOutRequest",true);
            CartModel MyCart = CartLogic.viewMyCart();
            ViewBag.cart = MyCart;
            ViewBag.CartItems = MyCart.CartItem;
            bool success = CartLogic.checkout( phone, address);
            if (success == true)
            { return View("Receipt");}
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

    }
}