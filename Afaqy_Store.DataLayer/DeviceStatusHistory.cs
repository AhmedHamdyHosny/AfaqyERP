namespace Afaqy_Store.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DeviceStatusHistory")]
    public partial class DeviceStatusHistory
    {
        [Key]
        public int HistoryId { get; set; }

        public int DeviceId { get; set; }

        public int StatusId { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual Device Device { get; set; }

        public virtual DeviceStatus DeviceStatus { get; set; }
    }
}
