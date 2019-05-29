using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.SharedModels
{
    public class PostModel
    {
        public int Post_id { get; set; }
        public string Post_body { get; set; }
        public Nullable<int> User_id { get; set; }
        public byte[] Post_pic { get; set; }
        public string username { get; set; }
        public DateTime Post_date { get; set; }
        public int user_id { get; set; }
        public byte[] user_image { get; set; }

        public List<CommentModel> commentsofpost { get; set; }
    }
}
