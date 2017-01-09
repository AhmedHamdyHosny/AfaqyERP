namespace Afaqy_Store.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SIMCardStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SIMCardStatus()
        {
            SIMCard = new HashSet<SIMCard>();
            SIMCardStatusHistory = new HashSet<SIMCardStatusHistory>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SIMCardStatusId { get; set; }

        [Required]
        [StringLength(20)]
        public string SIMCardStatusName_en { get; set; }

        [StringLength(20)]
        public string SIMCardStatusName_ar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SIMCard> SIMCard { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SIMCardStatusHistory> SIMCardStatusHistory { get; set; }
    }
}
