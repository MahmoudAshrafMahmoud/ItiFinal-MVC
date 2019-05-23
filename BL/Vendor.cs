using BL.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.IO;

namespace BL
{
  public class Vendor
    {
       public static void addnewproduct(ProductModel newproduct , string cat_name)
        {
            byte[] fileData = null;
            var binaryreader = new BinaryReader(newproduct.Image.InputStream);
            fileData = binaryreader.ReadBytes(newproduct.Image.ContentLength);
            using (CraftsEntities context = new CraftsEntities() )
            {
                var catid =int.Parse((from c in context.Category_table where c.Cat_Name == cat_name select c.Cat_Id).FirstOrDefault().ToString());
                Product_table pro = new Product_table
                {
                    Product_Name = newproduct.Product_Name,
                    Product_Description = newproduct.Product_Description,
                    Product_Price = newproduct.Product_Price,
                    Add_Date = DateTime.Now,
                    Image=fileData,
                    Cat_id= catid,
                    Vendor_id=1,
                    State="pendding"
                };
                context.Product_table.Add(pro);
                context.SaveChanges();
            }
        }
        
        public static List<CategoryModel> allcatigories()
        {
            using (CraftsEntities context = new CraftsEntities())
            {
                var cat = (from c in context.Category_table
                           select new CategoryModel
                           {
                               Cat_Id = c.Cat_Id
,
                               Cat_Name = c.Cat_Name
                           }).ToList();
                return cat;
            }
        }
    }
}
