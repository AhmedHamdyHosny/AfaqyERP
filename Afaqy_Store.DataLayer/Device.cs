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
    
    public partial class Device
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Device()
        {
            this.DeviceSIM = new HashSet<DeviceSIM>();
            this.DeviceStatusHistory = new HashSet<DeviceStatusHistory>();
        }
    
        public int DeviceId { get; set; }
        public string SerialNumber { get; set; }
        public string IMEI { get; set; }
        public int ModelTypeId { get; set; }
        public int DeviceStatusId { get; set; }
        public string Firmware { get; set; }
        public Nullable<int> BranchId { get; set; }
        public int CreateUserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Branch Branch { get; set; }
        public virtual DeviceStatus DeviceStatus { get; set; }
        public virtual DeviceModelType DeviceModelType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeviceSIM> DeviceSIM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeviceStatusHistory> DeviceStatusHistory { get; set; }
    }
}
