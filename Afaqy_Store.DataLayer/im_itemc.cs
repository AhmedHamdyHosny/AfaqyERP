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
    
    public partial class im_itemc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public im_itemc()
        {
            this.it_trans_b = new HashSet<it_trans_b>();
        }
    
        public int ic_cmp_seq { get; set; }
        public decimal ic_ia_item_id { get; set; }
        public Nullable<decimal> ic_serial_id { get; set; }
        public string ic_color { get; set; }
        public string ic_wa_code { get; set; }
        public Nullable<double> ic_small_qty { get; set; }
        public Nullable<double> ic_base_reorder { get; set; }
        public Nullable<double> ic_base_min { get; set; }
        public Nullable<double> ic_base_opti { get; set; }
        public string ic_bin_nb { get; set; }
        public Nullable<decimal> ic_qty1 { get; set; }
        public Nullable<int> ic_service { get; set; }
        public Nullable<decimal> ic_bookqty { get; set; }
        public string ic_adduser { get; set; }
        public Nullable<System.DateTime> ic_adddate { get; set; }
        public string ic_upduser { get; set; }
        public Nullable<System.DateTime> ic_upddate { get; set; }
        public Nullable<decimal> ic_soqty { get; set; }
        public Nullable<decimal> ic_poqty { get; set; }
    
        public virtual im_itema im_itema { get; set; }
        public virtual im_warehouse im_warehouse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<it_trans_b> it_trans_b { get; set; }
    }
}
