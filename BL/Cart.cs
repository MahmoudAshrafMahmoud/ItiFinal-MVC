using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BL.SharedModels;
using System.Web;


namespace BL
{
    class Cart
    {
        CraftsEntities context = new CraftsEntities();
        CartListModel MycartItems = new CartListModel();
        public void AddItemToCart(int ProductID)
        {
            //Get Product Data
            Product_table ProductInfo = context.Product_table.FirstOrDefault(s => s.Product_Id == ProductID);
            //creating Empty CartItem List
            //Add Product to CartItem
            MycartItems.ProductData = ProductInfo;
            MycartItems.ProductQty = 1;
            MycartItems.ProductTotalPrice = MycartItems.ProductData.Product_Price;
            CartModel MyCart = new CartModel();
            bool oldCartCheck = checkForOldCart();
            if (oldCartCheck == false) {
                 MyCart = addToNewCart();
            }
            else {
                 MyCart = addToOldCart(ProductID);
            }
            // Add Cart to session
            HttpContext.Current.Session["cart"] = MyCart;

            RecalculateTotalPrice();
            //return RedirectToAction("Home", "category");
        }
        public bool checkForOldCart(){
            bool oldCart;
            if (HttpContext.Current.Session["cart"] == null){
                oldCart = false;
            }
            else { oldCart = true; }
            return oldCart;
        }
        public CartModel addToNewCart(){
            //creating Empty CartItem List
            List<CartListModel> MycartList = new List<CartListModel>();
            //Add CartItem to CartItem List
            MycartList.Add(MycartItems);
            //creating Empty Cart
            CartModel Newcart = new CartModel();
            //Add CartItem to Empty Cart
            Newcart.CartItem = MycartList;
            //Add Items number to session
            HttpContext.Current.Session["count"] = 1;
            return Newcart;
        }
        public CartModel addToOldCart(int ProductID)
           {
            //Getting Cart from session
            CartModel Mycart = (CartModel)HttpContext.Current.Session["cart"];
            //Check if the product exsits
            bool item = false;
            for (int i = 0; i < Mycart.CartItem.Count(); i++)
            {
                if (Mycart.CartItem[i].ProductData.Product_Id == ProductID)
                {
                    Mycart.CartItem[i].ProductQty = Mycart.CartItem[i].ProductQty + 1;
                    Mycart.CartItem[i].ProductTotalPrice = Mycart.CartItem[i].ProductQty * Mycart.CartItem[i].ProductData.Product_Price;
                    item = true;
                }
            }

            if (item == false){
                //creating Empty CartItem List
                List<CartListModel> MycartList = Mycart.CartItem;
                //Getting CartListItem number
                HttpContext.Current.Session["count"] = Convert.ToInt32(HttpContext.Current.Session["count"]) + 1;
                //Add CartItem to CartItem List
                MycartList.Add(MycartItems);

                //Add CartItem to Empty Cart
                Mycart.CartItem = MycartList;
            }
            return Mycart;
        }
        public void RecalculateTotalPrice()
        {
            CartModel Mycart = (CartModel)HttpContext.Current.Session["cart"];
            double TotalPrice = 0;
            for (int i = 0; i < Mycart.CartItem.Count(); i++)
            {
                TotalPrice = TotalPrice + Mycart.CartItem[i].ProductQty * Mycart.CartItem[i].ProductData.Product_Price;
                Mycart.CartItem[i].ProductTotalPrice = Mycart.CartItem[i].ProductQty * Mycart.CartItem[i].ProductData.Product_Price;
            }
            Mycart.OrderTotalPrice = TotalPrice;
            HttpContext.Current.Session["TotalPrice"] = TotalPrice;
            HttpContext.Current.Session["cart"] = Mycart;
        }
    }
    }
