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
    
    public partial class Posts_table
    {
        public int Post_id { get; set; }
        public string Post_body { get; set; }
        public int User_id { get; set; }
        public byte[] Post_pic { get; set; }
        public System.DateTime Post_date { get; set; }
    
        public virtual User_table User_table { get; set; }
    }
}