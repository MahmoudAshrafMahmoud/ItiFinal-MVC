//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comments_Table
    {
        public int Comment_id { get; set; }
        public string Comment_body { get; set; }
        public int Comment_postID { get; set; }
        public int Comment_UserID { get; set; }
        public System.DateTime Comment_Date { get; set; }
    
        public virtual Posts_table Posts_table { get; set; }
        public virtual User_table User_table { get; set; }
    }
}
