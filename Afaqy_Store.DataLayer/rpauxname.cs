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
    
    public partial class rpauxname
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public rpauxname()
        {
            this.it_trans_a = new HashSet<it_trans_a>();
            this.rpaux = new HashSet<rpaux>();
        }
    
        public int aux_id { get; set; }
        public string title { get; set; }
        public string contact_name { get; set; }
        public string position { get; set; }
        public string code { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string extension { get; set; }
        public int serial { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Nullable<int> sendalert { get; set; }
        public string auxn_adduser { get; set; }
        public Nullable<System.DateTime> auxn_adddate { get; set; }
        public Nullable<int> blocked { get; set; }
        public string auxn_updUser { get; set; }
        public System.DateTime auxn_updDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<it_trans_a> it_trans_a { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rpaux> rpaux { get; set; }
    }
}
