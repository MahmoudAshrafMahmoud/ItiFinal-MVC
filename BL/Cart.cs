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
    public class Cart
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
            if (oldCartCheck == false)
            {
                MyCart = addToNewCart();
            }
            else
            {
                MyCart = addToOldCart(ProductID);
            }
            // Add Cart to session
            HttpContext.Current.Session["cart"] = MyCart;

            RecalculateTotalPrice();
            //return RedirectToAction("Home", "category");
        }
        public bool checkForOldCart()
        {
            bool oldCart;
            if (HttpContext.Current.Session["cart"] == null)
            {
                oldCart = false;
            }
            else { oldCart = true; }
            return oldCart;
        }
        public CartModel addToNewCart()
        {
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

            if (item == false)
            {
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
        public CartModel viewMyCart()
        {

            return (CartModel)HttpContext.Current.Session["cart"];
        }
        public void increase(int ID)
        {
            CartModel Mycart = (CartModel)HttpContext.Current.Session["cart"];

            //Check if the product exsits
            for (int i = 0; i < Mycart.CartItem.Count(); i++)
            {
                if (Mycart.CartItem[i].ProductData.Product_Id == ID)
                {
                    Mycart.CartItem[i].ProductQty = Mycart.CartItem[i].ProductQty + 1;
                }
            }
            RecalculateTotalPrice();
            HttpContext.Current.Session["cart"] = Mycart;
        }
        public void decrease(int ID)
        {
            CartModel Mycart = (CartModel)HttpContext.Current.Session["cart"];
            //Check if the product exsits
            for (int i = 0; i < Mycart.CartItem.Count(); i++)
            {
                if (Mycart.CartItem[i].ProductData.Product_Id == ID)
                {
                    if (Mycart.CartItem[i].ProductQty - 1 != 0)
                    {
                        Mycart.CartItem[i].ProductQty = Mycart.CartItem[i].ProductQty - 1;
                    }
                }
            }
            RecalculateTotalPrice();
            HttpContext.Current.Session["cart"] = Mycart;
        }
        public void clearCart()
        {
            HttpContext.Current.Session["cart"] = null;
            HttpContext.Current.Session["count"] = 0;
            HttpContext.Current.Session["TotalPrice"] = 0;
        }
        public bool checkout(int phone, string address)
        {
            bool success;
            CartModel Mycart = (CartModel)HttpContext.Current.Session["cart"];
            using (CraftsEntities conn = new CraftsEntities())
            {
                if (HttpContext.Current.Session["UserID"] != null)
                {
                    Order_table NewOrder = new Order_table();
                    NewOrder.Expected_Price = Mycart.OrderTotalPrice;
                    NewOrder.User_Id = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                    NewOrder.Order_Date = DateTime.Now;
                    NewOrder.Order_Phone = phone;
                    NewOrder.Order_Address = address;
                    //add instances to context
                    conn.Order_table.Add(NewOrder);
                    for (int i = 0; i < Mycart.CartItem.Count; i++)
                    {
                        OrderDetails_table NewOrderItem = new OrderDetails_table();
                        NewOrderItem.Order_Id = NewOrder.Order_Id;
                        NewOrderItem.Pro_Id = Mycart.CartItem[i].ProductData.Product_Id;
                        NewOrderItem.Quantity = Mycart.CartItem[i].ProductQty;
                        conn.OrderDetails_table.Add(NewOrderItem);
                    }
                    conn.SaveChanges();
                    success = true;
                }
                else { success = false; }
            }
            return success;
        }
    }
}
