using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.SharedModels
{
   public class ReviewModel
    {
        public int review_id { get; set; }
        public int product_id { get; set; }
        public string review_body { get; set; }
        public int User_id { get; set; }
        public DateTime review_date { get; set; }
        public string username { get; set; }
        public byte[] user_image { get; set; }



    }
}
