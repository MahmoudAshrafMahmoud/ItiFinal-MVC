using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crafts.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category Products
        //We will replace id input parameter (this default one) with any id 
        public ActionResult CategoryProducts(int id=1)
        {
            Product myProduct = new Product();
            var CategoryProducts = myProduct.CategoryProducts(id);

            return View();
        }
        public ActionResult showCategories()
        {
            Category mycat = new Category();
            ViewBag.Categories = mycat.AllCategories();
            return View();
        }


    }
}