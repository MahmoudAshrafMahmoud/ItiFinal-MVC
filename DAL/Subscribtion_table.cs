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
    
    public partial class Subscribtion_table
    {
        public int Subscribe_Id { get; set; }
        public int User_Id { get; set; }
        public int Cat_Id { get; set; }
    
        public virtual Category_table Category_table { get; set; }
        public virtual User_table User_table { get; set; }
    }
}