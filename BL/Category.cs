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
        public bool Subscribe(int user_id,int cat_id)
        {
            List<Subscribtion_table> myRecord = context.Subscribtion_table.Where(s => s.User_Id == user_id && s.Cat_Id == cat_id).Select(s => s).ToList();
            if (myRecord.Count() == 0)
            {
                Subscribtion_table model = new Subscribtion_table() { User_Id = user_id, Cat_Id = cat_id };
                context.Subscribtion_table.Add(model);
                context.SaveChanges();
            }
            return true;

        }
        
        public bool UnSubscribe(int user_id, int cat_id)
        {
            List<Subscribtion_table> myRecord = context.Subscribtion_table.Where(s => s.User_Id == user_id && s.Cat_Id == cat_id).Select(s => s).ToList();
            if (myRecord.Count > 0)
            {
                context.Subscribtion_table.Remove(myRecord[0]);
                context.SaveChanges();
            }
          
            return true;

        }

        public string checksubscribe(int user_id, int cat_id)
        {
            List<Subscribtion_table> myRecord = context.Subscribtion_table.Where(s => s.User_Id == user_id && s.Cat_Id == cat_id).Select(s => s).ToList();
            if(myRecord.Count > 0)
            {
                return "true";

            }
            return "false";
        }

        public string categoryname (int id)
        {
            return context.Category_table.Where(s => s.Cat_Id == id).Select(s => s.Cat_Name).First();
        }
    }
}
