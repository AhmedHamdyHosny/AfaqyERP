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
    
    public partial class DeliveryDeviceInfoHistory
    {
        public int HistoryId { get; set; }
        public int DeliveryItemId { get; set; }
        public int cmp_seq { get; set; }
        public int DeliveryDetailsId { get; set; }
        public int DeviceId { get; set; }
        public Nullable<int> Employee_aux_id { get; set; }
        public Nullable<System.DateTime> InstallingDateTime { get; set; }
        public string DeviceNaming { get; set; }
        public Nullable<int> DeviceNamingTypeId { get; set; }
        public Nullable<bool> AddToServer { get; set; }
        public Nullable<bool> TrackWithTechnician { get; set; }
        public Nullable<bool> ServerUpdated { get; set; }
        public string Note { get; set; }
        public bool IsBlock { get; set; }
        public int CreateUserId { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        public virtual DeliveryDevice DeliveryDevice { get; set; }
    }
}
