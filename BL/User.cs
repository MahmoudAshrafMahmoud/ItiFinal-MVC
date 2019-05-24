using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.SharedModels;
using DAL;


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
    }
}
