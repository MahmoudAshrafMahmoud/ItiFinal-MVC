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
                             Image = (byte[])l.Image,
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

        public static void addnewproduct (ProductModel newproduct , string cat_name)
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
                    Vendor_id = usersession.User_Id
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

              .OrderByDescending(g => g.Rating)
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
        //function to get product details due to vendor id 
        public static List<ProductModel> getproductdetails()
        {
            using (CraftsEntities context = new CraftsEntities())
            {
                var pro = (from p in context.Product_table
                           where p.Vendor_id == 1
                           join c in context.Category_table
    on p.Cat_id equals c.Cat_Id
                           select new ProductModel
                           {
                               Image = p.Image,
                               Product_Description = p.Product_Description,
                               Product_Price = p.Product_Price,
                               Catigory_name = c.Cat_Name,
                               Product_Name=p.Product_Name
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
    }
}
