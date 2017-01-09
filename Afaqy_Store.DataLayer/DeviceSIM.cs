namespace Afaqy_Store.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DeviceSIM")]
    public partial class DeviceSIM
    {
        public int DeviceSIMId { get; set; }

        public int DeviceId { get; set; }

        public int SIMCardId { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public int? ModifyUserId { get; set; }

        public DateTime? ModifyDate { get; set; }

        public virtual Device Device { get; set; }

        public virtual SIMCard SIMCard { get; set; }
    }
}
