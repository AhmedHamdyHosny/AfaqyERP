namespace Afaqy_Store.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName_en { get; set; }

        [StringLength(20)]
        public string FirstName_ar { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName_en { get; set; }

        [StringLength(20)]
        public string LastName_ar { get; set; }

        public int? CountryId { get; set; }

        public int UserTypeId { get; set; }

        public bool Active { get; set; }

        public bool AllowAccess { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual Country Country { get; set; }

        public virtual UserType UserType { get; set; }
    }
}
