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
    
    public partial class User_table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User_table()
        {
            this.Comments_Table = new HashSet<Comments_Table>();
            this.Posts_table = new HashSet<Posts_table>();
            this.ProductReview_table = new HashSet<ProductReview_table>();
        }
    
        public int User_Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string User_Name { get; set; }
        public string User_Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public long SSN { get; set; }
        public int Type_id { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string Gender { get; set; }
        public string Bio { get; set; }
        public double Rating { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments_Table> Comments_Table { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Posts_table> Posts_table { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductReview_table> ProductReview_table { get; set; }
    }
}
