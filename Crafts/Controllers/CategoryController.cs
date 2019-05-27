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
        //public ActionResult CatProducts(int id = 1)
        //{
        //    Product pro = new Product();
        //    ViewBag.products = pro.CategoryProducts(id);
        //    returF:\ITI_fullstack\MVC Final Project\ItiFinal-MVC\Crafts\Controllers\CategoryController.csn View();
        //}
        public ActionResult SelectedCategory(int id)
        {
            Product pro = new Product();
            ViewBag.products = pro.CategoryProducts(id);
            ViewBag.cat_id = id;
            return View();
        }
        public ActionResult showCategories()
        {
            Category mycat = new Category();
            ViewBag.Categories = mycat.AllCategories();
            return View();
        }
        [HttpPost]
        public PartialViewResult Subscribe(int user_id,int cat_id)
        {
            Product pro = new Product();
            ViewBag.products = pro.CategoryProducts(cat_id);
            Category mycat = new Category();
            mycat.Subscribe(user_id, cat_id);
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult UnSubscribe(int user_id, int cat_id)
        {
            Product pro = new Product();
            ViewBag.products = pro.CategoryProducts(cat_id);
            Category mycat = new Category();
            mycat.UnSubscribe(user_id, cat_id);
            return PartialView();
        }

    }
}