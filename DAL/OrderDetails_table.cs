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
    
    public partial class OrderDetails_table
    {
        public int OrderDetail_Id { get; set; }
        public int Quantity { get; set; }
        public string Approval { get; set; }
        public int Pro_Id { get; set; }
        public int Order_Id { get; set; }
        public int rating { get; set; }
    
        public virtual Order_table Order_table { get; set; }
    }
}
