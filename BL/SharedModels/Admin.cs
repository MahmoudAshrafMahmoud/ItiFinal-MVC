using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.SharedModels
{
   public class Admin
    {
        public int Admin_Id { get; set; }
        public string Admin_Name { get; set; }
        public string Admin_Email { get; set; }
        public string Password { get; set; }
        public int SSN { get; set; }
        public string State { get; set; }
        public int Request_ID { get; set; }
    }
}
