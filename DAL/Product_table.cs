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
    
    public partial class Product_table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product_table()
        {
            this.OrderDetails_table = new HashSet<OrderDetails_table>();
        }
    
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Description { get; set; }
        public double Product_Price { get; set; }
        public int Cat_id { get; set; }
        public int Vendor_id { get; set; }
        public string State { get; set; }
        public byte[] Image { get; set; }
    
        public virtual Category_table Category_table { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetails_table> OrderDetails_table { get; set; }
        public virtual User_table User_table { get; set; }
    }
}
