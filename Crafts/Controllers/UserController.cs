using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using BL.SharedModels;
using DAL;
using System.IO;

namespace Crafts.Controllers
{
    public class UserController : Controller
    {
        User ul = new User();
        BL.Vendor VendorLogic = new BL.Vendor();
        BL.Product productLogic = new BL.Product();

        // GET: User
        public ActionResult Home()
        {
            if (Session["user"] != null)
            {
                
               ViewBag.selectedVendor = VendorLogic.getTopTrendVendors();
               ViewBag.selectedPro = productLogic.getTopSellingProduct();           
                List<ProductModel> topSeller = ul.topSellerSup();
                ViewBag.topSeller = topSeller;
                ViewBag.lastAdded = ul.lastAdded();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult login()
        {
           
            return View();
        }

        public ActionResult Show_Orders()
        {
            BL.Order order = new BL.Order();
            int id = (int)Session["User_Id"];
            List<MyOrdersModel> ViewOrders = order.ShowMyOrders(id);

            return View(ViewOrders);

        }

        public ActionResult ViewDtails(int id)
        {
            BL.Order order = new BL.Order();

            List<MyOrdersModel> ViewOrdersDetails = order.ShowMyOrdersDetails(id);
            return PartialView(ViewOrdersDetails);
        }

        public ActionResult Be_Vendor(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult VendorRegister(string FullName, int NationalId, string Bio)
        {
            User_table USer = (User_table)Session["user"];

            int id = USer.User_Id;
            BL.User user = new BL.User();
            ViewBag.message = user.Vendor_Register(FullName, NationalId, Bio, 1);
            return PartialView();
        }


        [HttpPost]
        public ActionResult login(string User_Email, string Password)
        {
            User_table user = ul.login(User_Email, Password);
            if (user != null)
            {
                var base64 = Convert.ToBase64String(user.ProfilePicture);
                var ImgSRC = string.Format("data:image/gif;base64,{0}", base64);
                Session.Add("user", user);
                Session.Add("User_Id", user.User_Id);
                Session.Add("UserFullname", user.FName + " " +user.LName);
                Session.Add("User_Email", user.User_Email);
                Session.Add("ProfilePicture", ImgSRC);
                Session.Add("Rating", user.Rating);
                Session.Add("Bio", user.Bio);
                Session.Add("Gender", user.Gender);

                if (Session["checkOutRequest"] != null)
                {
                    return RedirectToAction("cartDisplay", "Cart");
                }
                else
                {
                    return RedirectToAction("Home", "User");
                }
            }
            else
            {
                ViewBag.NotValidUser = "User Name OR Password is Incorrect";
                return View("login");
            }

        }

        public ActionResult logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult RegisterNewUser()
        {

            BL.Category myCategory = new Category();
            List<CategoryModel> selectedCategory = myCategory.AllCategories();
            ViewBag.selectedCategory = selectedCategory;

            return View();
        }



        [HttpPost]
        public ActionResult RegisterNewUser(User_table newUser, HttpPostedFileBase fileSelected)

        {
            byte[] fileData = null;
            var binaryReader = new BinaryReader(fileSelected.InputStream);
            fileData = binaryReader.ReadBytes(fileSelected.ContentLength);
            newUser.ProfilePicture = fileData;
            

            using (CraftsEntities myData = new CraftsEntities())

            {
                newUser.Rating = 0;
                newUser.Type_id = 1;
                myData.User_table.Add(newUser);
                myData.SaveChanges();
                Session["userID"] = newUser.User_Id;
                ModelState.Clear();
                newUser = null;
            }

                return View();
        }
        

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name, string email, string subject, string message)
        {

            ul.addMessage( name, email, subject, message);
            ul.sendEmail(email);
            return RedirectToAction("Index", "Home");
        }


        //Selected User
        public ActionResult SelectedUser(int id)
        {
            BL.User user = new BL.User();
            User_table selecteduser = user.GetSelectedUser(id);

            Product product = new Product();
            ViewBag.VendorProducts = product.GetProductsOfvendor(id);


            ViewBag.BestSellingVendorProducts = product.BestSellingForVendor(id);

            return View(selecteduser);


        }


        

    
        public ActionResult myprofile()
        {
            if (Session["User_Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login", "User");
            }
         }

        public ActionResult mycategories()
        {
             ViewBag.mycats =  ul.mySubCat((int)Session["User_Id"]);
            return View();
        }

        public ActionResult mysubvendors()
        {
            ViewBag.subs = ul.myFollowedVendor((int)Session["User_Id"]);
            return View();
        }

    }

  
}