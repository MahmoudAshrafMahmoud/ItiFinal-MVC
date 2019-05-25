using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.SharedModels;
using DAL;
using System.Web;

namespace BL
{
    public class User
    {
        CraftsEntities context = new CraftsEntities();

        public User_table login(string User_Email, string Password)
        {
            User_table user = context.User_table.Where(s => s.User_Email.Equals(User_Email) && s.Password.Equals(Password)).FirstOrDefault();
            return user;
        }




        public List<ProductModel> topSeller()
        {

            int[] query = (from o in context.OrderDetails_table
                           join p in context.Product_table
                           on o.Pro_Id equals p.Product_Id

                           group o.Quantity by p.Product_Id into g
                           orderby g.Sum() descending
                           select g.Key).ToArray(); // get the top 5 orders

            List<ProductModel> topSoldProducts = new List<ProductModel>();
            ProductModel[] topSold = new ProductModel[query.Length];
            for (int i = 0; i < query.Length; i++)
            {

                int pid = query[i];

                var x = (from p in context.Product_table
                         where p.Product_Id == pid
                         select new ProductModel
                         {
                             Product_Id = p.Product_Id,
                             Product_Description = p.Product_Description,
                             Product_Name = p.Product_Name,
                             Image = p.Image,
                             Product_Price = p.Product_Price,
                             Vendor_id = p.Vendor_id

                         }).ToList();

                topSoldProducts.AddRange((List<ProductModel>)x);
            }
            return topSoldProducts;
        }

        public List<ProductModel> followedVendorsProducts(int userID)
        {
            var productsList = (from u in context.User_table
                                join f in context.Following_table
                                on u.User_Id equals f.User_id
                                join p in context.Product_table
                                on f.Vendor_id equals p.Vendor_id
                                where u.User_Id == userID
                                select new ProductModel
                                {
                                    Product_Id = p.Product_Id,
                                    Product_Name = p.Product_Name,
                                    Product_Description = p.Product_Description,
                                    Product_Price = p.Product_Price,
                                    Image = p.Image,
                                    Vendor_id = p.Vendor_id
                                    
                                }).ToList();
            return productsList;
        }



        public List<ProductModel> topSellerSup()
        {
            User_table userSession = (User_table)HttpContext.Current.Session["user"];
            int[] query = (from o in context.OrderDetails_table
                           join p in context.Product_table
                           on o.Pro_Id equals p.Product_Id

                           group o.Quantity by p.Product_Id into g
                           orderby g.Sum() descending
                           select g.Key).ToArray(); // get the top 5 orders

            List<ProductModel> topSoldProducts = new List<ProductModel>();
            ProductModel[] topSold = new ProductModel[query.Length];
            for (int i = 0; i < query.Length; i++)
            {

                int pid = query[i];

                var x = (from p in context.Product_table
                         join s in context.Subscribtion_table
                         on p.Cat_id equals s.Cat_Id
                         where p.Product_Id == pid && s.User_Id == userSession.User_Id
                         select new ProductModel
                         {
                             Product_Id = p.Product_Id,
                             Product_Description = p.Product_Description,
                             Product_Name = p.Product_Name,
                             Image = p.Image,
                             Product_Price = p.Product_Price,
                             Vendor_id = p.Vendor_id

                         }).ToList();

                topSoldProducts.AddRange((List<ProductModel>)x);
            }
            return topSoldProducts;
        }
    }
}
