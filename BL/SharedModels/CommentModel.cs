using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.SharedModels
{
    public class CommentModel
    {
        public int Comment_id { get; set; }
        public string Comment_body { get; set; }
        public int Comment_postID { get; set; }
        public int Comment_UserID { get; set; }
        public System.DateTime Comment_Date { get; set; }
        public string Comment_Username { get; set; }
        public byte[] Comment_profilePicture { get; set; }

        
    }
}
