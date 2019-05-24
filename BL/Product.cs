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
                Image = s.Image
            }).ToList();
            return producttest;
        }

        List<Product_table> selectProducts = new List<Product_table>();

        public List<Product_table> getTopSellingProduct()
        {
            var result = context.OrderDetails_table.GroupBy(e => e.Pro_Id) // group the list by country

              .OrderByDescending(                 // then sort by the summed values DESC
               g => g.Sum(e => e.Quantity))
               .Take(5)                            // then take the top X values
              .Select(                            // e.g. List.TopX(3) would return...
              r => new { ProID = r.Key, Sum = r.Sum(e => e.Quantity) }).ToList();


            for (int i = 0; i < result.Count; i++)
            {
                int x = result[i].ProID;
                selectProducts.Add(context.Product_table.FirstOrDefault(s => s.Product_Id == x));
            }


            return selectProducts;
        }

    }
}
