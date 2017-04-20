//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Afaqy_Store.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Customer = new HashSet<Customer>();
            this.Employee1 = new HashSet<Employee>();
        }
    
        public int EmployeeId { get; set; }
        public int cmp_seq { get; set; }
        public string FirstName_en { get; set; }
        public string FirstName_ar { get; set; }
        public string LastName_en { get; set; }
        public string LastName_ar { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string ResidencyNumber { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public Nullable<int> JobTitleId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<bool> IsManager { get; set; }
        public Nullable<int> ManagerId { get; set; }
        public string Branch_br_code { get; set; }
        public Nullable<System.DateTime> JoinDate { get; set; }
        public Nullable<int> UserId { get; set; }
        public bool IsBlock { get; set; }
        public int CreateUserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        public virtual Country Country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employee1 { get; set; }
        public virtual Employee Employee2 { get; set; }
        public virtual JobTitle JobTitle { get; set; }
    }
}
