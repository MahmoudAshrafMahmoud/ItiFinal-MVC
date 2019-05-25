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

        public string Full_Name { set; get; }

        public int National_ID { set; get; }

        public string Seller_info { set; get; }

        public string state { set; get; }
    }
}
