using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Web;

namespace BL.SharedModels
{
   public class UserModel
    {
        public string fname { get; set; }
        public string lname { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int phone { get; set; }
        public int ssn { get; set; }
        public int type { get; set; }
        public bool state { set; get; }
        public byte[] Image { get; set; }
        public string FullName { get; set; }
        public long NationalId { get; set; }
        public string Bio { get; set; }
        public int userid { set; get; }
        public string gender { set; get; }
        public string typeName { set; get; }
        public HttpPostedFileBase ProfilePicture { get; set; }




    }
}
