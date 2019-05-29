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
 

        Category mycat = new Category();

        public ActionResult SelectedCategory(int id)
        {
            Product pro = new Product();
            ViewBag.products = pro.CategoryProducts(id);
            ViewBag.cat_id = id;
            ViewBag.catname = mycat.categoryname(id);
            if (Session["user"] != null)
            {
                ViewBag.substate = mycat.checksubscribe((int)Session["User_Id"], id);

            }

                return View();
        }

        public ActionResult showCategories()
        {
            ViewBag.Categories = mycat.AllCategories();
            return View();
        }
        [HttpPost]
        public PartialViewResult Subscribe(int user_id,int cat_id)
        {
            if (Session["user"] != null)
            {
                Product pro = new Product();
                ViewBag.products = pro.CategoryProducts(cat_id);
                Category mycat = new Category();
                mycat.Subscribe((int)Session["User_Id"], cat_id);
                return PartialView();
            }
            return PartialView("errorview");
        }

        [HttpPost]
        public PartialViewResult UnSubscribe(int user_id, int cat_id)
        {
            Product pro = new Product();
            ViewBag.products = pro.CategoryProducts(cat_id);
            Category mycat = new Category();
            mycat.UnSubscribe((int)Session["User_Id"], cat_id);
            return PartialView();
        }

    }
}