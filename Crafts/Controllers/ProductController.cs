using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.SharedModels;

namespace Crafts.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductView(int id)
        {
            BL.Product proModel = new BL.Product();
            ProductModel proDuctDetails = proModel.ProductDetailsView(id);

            return View(proDuctDetails);
        }

        
    }
}