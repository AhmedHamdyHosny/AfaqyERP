namespace Afaqy_Store.DataLayer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AfaqyStoreDBEntities : DbContext
    {
        public AfaqyStoreDBEntities()
            : base("name=AfaqyStoreDBEntities")
        {
        }

        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<DeviceSIM> DeviceSIM { get; set; }
        public virtual DbSet<DeviceStatus> DeviceStatus { get; set; }
        public virtual DbSet<DeviceStatusHistory> DeviceStatusHistory { get; set; }
        public virtual DbSet<ModelType> ModelType { get; set; }
        public virtual DbSet<SIMCard> SIMCard { get; set; }
        public virtual DbSet<SIMCardStatus> SIMCardStatus { get; set; }
        public virtual DbSet<SIMCardStatusHistory> SIMCardStatusHistory { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .Property(e => e.SerialNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Device>()
                .Property(e => e.IMI)
                .IsUnicode(false);

            modelBuilder.Entity<Device>()
                .Property(e => e.Frameware)
                .IsUnicode(false);

            modelBuilder.Entity<Device>()
                .HasMany(e => e.DeviceSIM)
                .WithRequired(e => e.Device)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Device>()
                .HasMany(e => e.DeviceStatusHistory)
                .WithRequired(e => e.Device)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DeviceStatus>()
                .Property(e => e.DeviceStatus_en)
                .IsUnicode(false);

            modelBuilder.Entity<DeviceStatus>()
                .HasMany(e => e.Device)
                .WithRequired(e => e.DeviceStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DeviceStatus>()
                .HasMany(e => e.DeviceStatusHistory)
                .WithRequired(e => e.DeviceStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ModelType>()
                .Property(e => e.ModelTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<ModelType>()
                .HasMany(e => e.Device)
                .WithRequired(e => e.ModelType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SIMCard>()
                .Property(e => e.SerialNumber)
                .IsUnicode(false);

            modelBuilder.Entity<SIMCard>()
                .HasMany(e => e.DeviceSIM)
                .WithRequired(e => e.SIMCard)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SIMCard>()
                .HasMany(e => e.SIMCardStatusHistory)
                .WithRequired(e => e.SIMCard)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SIMCardStatus>()
                .Property(e => e.SIMCardStatusName_en)
                .IsUnicode(false);

            modelBuilder.Entity<SIMCardStatus>()
                .HasMany(e => e.SIMCard)
                .WithRequired(e => e.SIMCardStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SIMCardStatus>()
                .HasMany(e => e.SIMCardStatusHistory)
                .WithRequired(e => e.SIMCardStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName_en)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName_en)
                .IsUnicode(false);

            modelBuilder.Entity<UserType>()
                .Property(e => e.UserTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<UserType>()
                .HasMany(e => e.User)
                .WithRequired(e => e.UserType)
                .WillCascadeOnDelete(false);
        }
    }
}
