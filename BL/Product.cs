using BL.SharedModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Product
    {
        CraftsEntities context = new CraftsEntities();

        //Get Category Products By category ID
        public List<ProductModel> CategoryProducts(int cat_id)
        {
            var query = context.Product_table.Where(s => s.Cat_id == cat_id).Select(s => new ProductModel
            {
                Product_Id = s.Product_Id,
                Product_Name = s.Product_Name,
                Product_Description = s.Product_Description,
                Image = s.Image
            }).ToList();
            return query;
        }

        public List<ProductModel> productsForTestingCartMethods()
        {
            List<ProductModel> producttest = context.Product_table.Select(s => new ProductModel
            {
                Product_Id = s.Product_Id,
                Product_Name = s.Product_Name,
                Product_Description = s.Product_Description,
                Product_Price = s.Product_Price,
                Image = s.Image
            }).ToList();
            return producttest;
        }
    }
}
