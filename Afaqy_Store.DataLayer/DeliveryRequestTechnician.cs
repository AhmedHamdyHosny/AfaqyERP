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
    
    public partial class DeliveryRequestTechnician
    {
        public int DeliveryRequestTechnicianId { get; set; }
        public int cmp_seq { get; set; }
        public int DeliveryRequestId { get; set; }
        public int Employee_aux_id { get; set; }
        public bool IsBlock { get; set; }
        public int CreateUserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        public virtual DeliveryRequest DeliveryRequest { get; set; }
    }
}
