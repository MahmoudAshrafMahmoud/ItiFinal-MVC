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
        BL.Product productLogic = new BL.Product();

        // GET: Cart
        public ActionResult Index()
        { 
            return View();
        }

        public ActionResult ShowProductToTest()
        {
            List<BL.SharedModels.ProductModel> testproducts = productLogic.productsForTestingCartMethods(); 
            return View();
        }
    }
}