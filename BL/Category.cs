using BL.SharedModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Category
    {
        CraftsEntities context = new CraftsEntities();
        public List<CategoryModel> AllCategories()
        {
            var query = context.Category_table.
                Select(s => new CategoryModel { Cat_Id = s.Cat_Id, Cat_Name = s.Cat_Name }).ToList();
            return query;
        }
    }
}
