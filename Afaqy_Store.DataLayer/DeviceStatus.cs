namespace Afaqy_Store.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DeviceStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DeviceStatus()
        {
            Device = new HashSet<Device>();
            DeviceStatusHistory = new HashSet<DeviceStatusHistory>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeviceStatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string DeviceStatus_en { get; set; }

        [StringLength(50)]
        public string DeviceStatus_ar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Device> Device { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeviceStatusHistory> DeviceStatusHistory { get; set; }
    }
}
