using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BL.SharedModels
{
   public class VendorRequest
    {
        public int user_id { set; get; }
        public DateTime date { set; get; }
        public int req_id { set; get; }
    }
}
