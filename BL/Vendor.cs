using BL.SharedModels;
using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BL
{
    public class Vendor
    {
        CraftsEntities context = new CraftsEntities();
       static User_table usersession= (User_table) HttpContext.Current.Session["user"] ;
       

        //Get Vendor orders
        public  List<OrderModel> VendorOrdersReview(int v_id)
        {
            var query = (from v in context.OrderDetails_table
                         join s in context.Order_table
                         on v.Order_Id equals s.Order_Id
                         join l in context.Product_table
                         on v.Pro_Id equals l.Product_Id
                         where v.Approval=="pending" && l.Vendor_id==v_id
                         select new OrderModel()
                         {
                             Product_Name = l.Product_Name,
                             Product_Price = l.Product_Price,
                             Quantity = v.Quantity,
                             Order_id = s.Order_Id,
                             Vendor_id = l.Vendor_id,
                             Approval = v.Approval,
                             Order_Date = s.Order_Date.ToString(),
                             Order_Address = s.Order_Address,
                             Order_Phone = s.Order_Phone,
                             Expected_Price = (float)s.Expected_Price,
                             Image = l.Image,
                             OrderDetail_Id=v.OrderDetail_Id
                         }).ToList();
                                             
            return query;
        }

        public bool AcceptOrder(int id)
        {
            (from p in context.OrderDetails_table
             where p.OrderDetail_Id == id
             select p).ToList().ForEach(x => x.Approval = "Approved");

            context.SaveChanges();
            return true;
        }
        public bool RejectOrder(int id)
        {
            (from p in context.OrderDetails_table
             where p.OrderDetail_Id == id
             select p).ToList().ForEach(x => x.Approval = "Rejected");

            context.SaveChanges();
            return true;
        }

        public static void addnewproduct (ProductModel newproduct , string cat_name , int userid)
        {
            byte[] fileData = null;
            var binaryReader = new BinaryReader(newproduct.insertedimg.InputStream);
            fileData = binaryReader.ReadBytes(newproduct.insertedimg.ContentLength);
            using (CraftsEntities context = new CraftsEntities())
            {
                var catid = int.Parse((from c in context.Category_table where c.Cat_Name == cat_name select c.Cat_Id).FirstOrDefault().ToString());
                var pro = new Product_table
                {
                    Product_Name = newproduct.Product_Name,
                    Product_Description = newproduct.Product_Description,
                    Product_Price = newproduct.Product_Price,
                    Cat_id = catid,
                    Image = fileData,
                    Add_Date = DateTime.Now,
                    Vendor_id = userid,
                    State="pending"
                };
                context.Product_table.Add(pro);
                context.SaveChanges();
            }
        }
        public static List<CategoryModel> allcatigories()
        {
            using(CraftsEntities context = new CraftsEntities())
            {
                var cat = (from c in context.Category_table
                           select new CategoryModel
                           {
                               Cat_Name = c.Cat_Name,
                               Cat_Id = c.Cat_Id
                           }).ToList();
                return cat;
            }
        }

        //select TOP 4 User_Id,Rating from User_table
        //ORDER BY Rating DESC




        List<User_table> selectVendor = new List<User_table>();

        public List<User_table> getTopTrendVendors()
        {
            var result = context.User_table 

              .OrderByDescending(g => g.rating).Where(s=>s.Type_id==2)
               .Take(5)                      
              .Select(                            
              r => new { User_Id = r.User_Id, Name = r.FName , Image = r.ProfilePicture}).ToList();


            for (int i = 0; i < result.Count; i++)
            {
                int x = result[i].User_Id;
                selectVendor.Add(context.User_table.FirstOrDefault(s => s.User_Id == x));
            }


            return selectVendor;
        }
        //function to get Aprroved product  due to vendor id 
        public static List<ProductModel> ApprovedProducts(int userid)
        {
            using (CraftsEntities context = new CraftsEntities())
            {
                var pro = (from p in context.Product_table
                           where p.Vendor_id == userid && p.State== "approved"
                           join c in context.Category_table
                           on p.Cat_id equals c.Cat_Id
                           select new ProductModel
                           {
                               Image = p.Image,
                               Product_Description = p.Product_Description,
                               Product_Price = p.Product_Price,
                               Catigory_name = c.Cat_Name,
                               Product_Name=p.Product_Name,
                               Product_Id = p.Product_Id

                           }).ToList();
                return pro;
                           
            }
        }

        public List<OrderModel> SearchOrder(string search,int v_id)
        {
            var query = (from v in context.OrderDetails_table
                         join s in context.Order_table
                         on v.Order_Id equals s.Order_Id
                         join l in context.Product_table
                         on v.Pro_Id equals l.Product_Id
                         where v.Approval == "pending" && l.Vendor_id == v_id && l.Product_Name.Contains(search)
                         select new OrderModel()
                         {
                             Product_Name = l.Product_Name,
                             Product_Price = l.Product_Price,
                             Quantity = v.Quantity,
                             Order_id = s.Order_Id,
                             Vendor_id = l.Vendor_id,
                             Approval = v.Approval,
                             Order_Date = s.Order_Date.ToString(),
                             Order_Address = s.Order_Address,
                             Order_Phone = s.Order_Phone,
                             Expected_Price = (float)s.Expected_Price,
                             Image = (byte[])l.Image,
                             OrderDetail_Id = v.OrderDetail_Id
                         }).ToList();
            return query;
            
        }

        //function to get pending product  due to vendor id 
        public static List<ProductModel> pendingProducts(int userid)
        {
            using (CraftsEntities context = new CraftsEntities())
            {
                var pro = (from p in context.Product_table
                           where p.Vendor_id == userid && p.State == "pending"
                           join c in context.Category_table
                           on p.Cat_id equals c.Cat_Id
                           select new ProductModel
                           {
                               Image = p.Image,
                               Product_Description = p.Product_Description,
                               Product_Price = p.Product_Price,
                               Catigory_name = c.Cat_Name,
                               Product_Name = p.Product_Name,
                               Product_Id = p.Product_Id
                           }).ToList();
                return pro;

            }
        }

        //function to get rejected product  due to vendor id 
        public static List<ProductModel> rejectedProducts(int userid)
        {
            using (CraftsEntities context = new CraftsEntities())
            {
                var pro = (from p in context.Product_table
                           where p.Vendor_id == userid && p.State == "rejected"
                           join c in context.Category_table
                           on p.Cat_id equals c.Cat_Id
                           select new ProductModel
                           {
                               Image = p.Image,
                               Product_Description = p.Product_Description,
                               Product_Price = p.Product_Price,
                               Catigory_name = c.Cat_Name,
                               Product_Name = p.Product_Name,
                               Product_Id = p.Product_Id

                           }).ToList();
                return pro;

            }
        }

        public static List<ProductModel> outofstockProducts(int userid)
        {
            using (CraftsEntities context = new CraftsEntities())
            {
                var pro = (from p in context.Product_table
                           where p.Vendor_id == userid && p.State == "out of stock"
                           join c in context.Category_table
                           on p.Cat_id equals c.Cat_Id
                           select new ProductModel
                           {
                               Image = p.Image,
                               Product_Description = p.Product_Description,
                               Product_Price = p.Product_Price,
                               Catigory_name = c.Cat_Name,
                               Product_Name = p.Product_Name,
                               Product_Id = p.Product_Id

                           }).ToList();
                return pro;

            }
        }

        public bool followvendor(int user_id, int vendor_id)
        {
            List<Following_table> myRecord = context.Following_table.Where(s => s.User_id == user_id && s.Vendor_id == vendor_id).Select(s => s).ToList();
            if (myRecord.Count() == 0)
            {
                Following_table model = new Following_table() { User_id = user_id, Vendor_id = vendor_id };
                context.Following_table.Add(model);
                context.SaveChanges();
            }
            return true;

        }



        public bool unfollowvendor(int user_id, int vendor_id)
        {
            List<Following_table> myRecord = context.Following_table.Where(s => s.User_id == user_id && s.Vendor_id == vendor_id).Select(s => s).ToList();
            if (myRecord.Count() != 0)
            {
                context.Following_table.Remove(myRecord[0]);
                context.SaveChanges();
            }
            return true;

        }

        public string checkfollow(int user_id, int vendor_id)
        {
            List<Following_table> myRecord = context.Following_table.Where(s => s.User_id == user_id && s.Vendor_id == vendor_id).Select(s => s).ToList();
            if (myRecord.Count > 0)
            {
                return "true";

            }
            return "false";
        }

        public bool outofstock(int id)
        {
            (from p in context.Product_table
             where p.Product_Id == id
             select p).ToList().ForEach(x => x.State = "out of stock");

            context.SaveChanges();
            return true;
        }

        public bool restoreproduct(int id)
        {
            (from p in context.Product_table
             where p.Product_Id == id
             select p).ToList().ForEach(x => x.State = "pending");

            context.SaveChanges();
            return true;
        }
    }
}
