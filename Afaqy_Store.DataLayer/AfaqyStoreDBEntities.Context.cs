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
    
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<BrandServerPort> BrandServerPort { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<DeviceModelType> DeviceModelType { get; set; }
        public virtual DbSet<DeviceSIM> DeviceSIM { get; set; }
        public virtual DbSet<DeviceStatus> DeviceStatus { get; set; }
        public virtual DbSet<DeviceStatusHistory> DeviceStatusHistory { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SIMCard> SIMCard { get; set; }
        public virtual DbSet<SIMCardContract> SIMCardContract { get; set; }
        public virtual DbSet<SIMCardContractRenewal> SIMCardContractRenewal { get; set; }
        public virtual DbSet<SIMCardProvider> SIMCardProvider { get; set; }
        public virtual DbSet<SIMCardStatus> SIMCardStatus { get; set; }
        public virtual DbSet<SIMCardStatusHistory> SIMCardStatusHistory { get; set; }
        public virtual DbSet<SystemServerIP> SystemServerIP { get; set; }
        public virtual DbSet<TechniqueCompany> TechniqueCompany { get; set; }
        public virtual DbSet<TechniqueSystem> TechniqueSystem { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
    }
}
