namespace Afaqy_Store.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SIMCardStatusHistory")]
    public partial class SIMCardStatusHistory
    {
        [Key]
        public int HistoryId { get; set; }

        public int SIMCardId { get; set; }

        public int SIMCardStatusId { get; set; }

        [StringLength(200)]
        public string Note { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual SIMCard SIMCard { get; set; }

        public virtual SIMCardStatus SIMCardStatus { get; set; }
    }
}
