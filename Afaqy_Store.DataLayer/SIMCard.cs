namespace Afaqy_Store.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SIMCard")]
    public partial class SIMCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SIMCard()
        {
            DeviceSIM = new HashSet<DeviceSIM>();
            SIMCardStatusHistory = new HashSet<SIMCardStatusHistory>();
        }

        public int SIMCardId { get; set; }

        [Required]
        [StringLength(20)]
        public string SerialNumber { get; set; }

        [StringLength(20)]
        public string GSM { get; set; }

        public int SIMCardStatusId { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public int? ModifyUserId { get; set; }

        public DateTime? ModifyDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeviceSIM> DeviceSIM { get; set; }

        public virtual SIMCardStatus SIMCardStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SIMCardStatusHistory> SIMCardStatusHistory { get; set; }
    }
}
