﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AfaqyStoreEntities : DbContext
    {
        public AfaqyStoreEntities()
            : base("name=AfaqyStoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<DeviceModelType> DeviceModelType { get; set; }
        public virtual DbSet<DeviceSIM> DeviceSIM { get; set; }
        public virtual DbSet<DeviceStatus> DeviceStatus { get; set; }
        public virtual DbSet<DeviceStatusHistory> DeviceStatusHistory { get; set; }
        public virtual DbSet<SIMCard> SIMCard { get; set; }
        public virtual DbSet<SIMCardStatus> SIMCardStatus { get; set; }
        public virtual DbSet<SIMCardStatusHistory> SIMCardStatusHistory { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
    }
}
