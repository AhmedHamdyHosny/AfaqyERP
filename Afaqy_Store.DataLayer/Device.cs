namespace Afaqy_Store.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Device")]
    public partial class Device
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Device()
        {
            DeviceSIM = new HashSet<DeviceSIM>();
            DeviceStatusHistory = new HashSet<DeviceStatusHistory>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeviceId { get; set; }

        [StringLength(20)]
        public string SerialNumber { get; set; }

        [StringLength(20)]
        public string IMI { get; set; }

        public int ModelTypeId { get; set; }

        public int DeviceStatusId { get; set; }

        [Required]
        [StringLength(10)]
        public string Frameware { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public int? ModifyUserId { get; set; }

        public DateTime? ModifyDate { get; set; }

        public virtual DeviceStatus DeviceStatus { get; set; }

        public virtual ModelType ModelType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeviceSIM> DeviceSIM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeviceStatusHistory> DeviceStatusHistory { get; set; }
    }
}
