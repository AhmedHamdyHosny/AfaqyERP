//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Security.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.UserRole = new HashSet<UserRole>();
            this.UserService = new HashSet<UserService>();
            this.UserServiceAccess = new HashSet<UserServiceAccess>();
        }
    
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName_en { get; set; }
        public string FirstName_ar { get; set; }
        public string LastName_en { get; set; }
        public string LastName_ar { get; set; }
        public Nullable<int> CountryId { get; set; }
        public int UserTypeId { get; set; }
        public bool AllowAccess { get; set; }
        public bool IsBlock { get; set; }
        public int CreateUserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        public virtual UserType UserType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> UserRole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserService> UserService { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserServiceAccess> UserServiceAccess { get; set; }
    }
}