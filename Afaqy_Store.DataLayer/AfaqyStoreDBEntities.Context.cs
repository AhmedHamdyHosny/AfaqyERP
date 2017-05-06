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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
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
    
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<BrandServerPort> BrandServerPort { get; set; }
        public virtual DbSet<ContactMethod> ContactMethod { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerContact> CustomerContact { get; set; }
        public virtual DbSet<CustomerContactDetails> CustomerContactDetails { get; set; }
        public virtual DbSet<CustomerServerAccount> CustomerServerAccount { get; set; }
        public virtual DbSet<CustomerServerAccountStatusHistory> CustomerServerAccountStatusHistory { get; set; }
        public virtual DbSet<CustomerServerStatus> CustomerServerStatus { get; set; }
        public virtual DbSet<CustomerServerUser> CustomerServerUser { get; set; }
        public virtual DbSet<CustomerStatus> CustomerStatus { get; set; }
        public virtual DbSet<CustomerType> CustomerType { get; set; }
        public virtual DbSet<DeliveryRequestDetails> DeliveryRequestDetails { get; set; }
        public virtual DbSet<DeliveryRequestStatus> DeliveryRequestStatus { get; set; }
        public virtual DbSet<DeliveryRequestStatusHistory> DeliveryRequestStatusHistory { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DeviceModelType> DeviceModelType { get; set; }
        public virtual DbSet<DeviceServer> DeviceServer { get; set; }
        public virtual DbSet<DeviceSIM> DeviceSIM { get; set; }
        public virtual DbSet<DeviceStatus> DeviceStatus { get; set; }
        public virtual DbSet<DeviceStatusHistory> DeviceStatusHistory { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<ItemFamily> ItemFamily { get; set; }
        public virtual DbSet<NotificationType> NotificationType { get; set; }
        public virtual DbSet<PointOfSale> PointOfSale { get; set; }
        public virtual DbSet<SaleTransactionType> SaleTransactionType { get; set; }
        public virtual DbSet<ServerRole> ServerRole { get; set; }
        public virtual DbSet<ServerUnit> ServerUnit { get; set; }
        public virtual DbSet<ServerUserRole> ServerUserRole { get; set; }
        public virtual DbSet<SIMCard> SIMCard { get; set; }
        public virtual DbSet<SIMCardContract> SIMCardContract { get; set; }
        public virtual DbSet<SIMCardContractRenewal> SIMCardContractRenewal { get; set; }
        public virtual DbSet<SIMCardProvider> SIMCardProvider { get; set; }
        public virtual DbSet<SIMCardStatus> SIMCardStatus { get; set; }
        public virtual DbSet<SIMCardStatusHistory> SIMCardStatusHistory { get; set; }
        public virtual DbSet<SystemServerIP> SystemServerIP { get; set; }
        public virtual DbSet<TechniqueCompany> TechniqueCompany { get; set; }
        public virtual DbSet<TechniqueSystem> TechniqueSystem { get; set; }
        public virtual DbSet<TempDevice> TempDevice { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<fm_c_branch> fm_c_branch { get; set; }
        public virtual DbSet<im_itema> im_itema { get; set; }
        public virtual DbSet<im_itemb> im_itemb { get; set; }
        public virtual DbSet<im_itemc> im_itemc { get; set; }
        public virtual DbSet<im_points> im_points { get; set; }
        public virtual DbSet<im_warehouse> im_warehouse { get; set; }
        public virtual DbSet<it_trans_a> it_trans_a { get; set; }
        public virtual DbSet<it_trans_b> it_trans_b { get; set; }
        public virtual DbSet<it_trans_serial> it_trans_serial { get; set; }
        public virtual DbSet<rpaux> rpaux { get; set; }
        public virtual DbSet<rpauxname> rpauxname { get; set; }
        public virtual DbSet<BrandServerPortView> BrandServerPortView { get; set; }
        public virtual DbSet<CustomerServerAccountView> CustomerServerAccountView { get; set; }
        public virtual DbSet<CustomerServerUnitsCountView> CustomerServerUnitsCountView { get; set; }
        public virtual DbSet<CustomerServerUnitsView> CustomerServerUnitsView { get; set; }
        public virtual DbSet<CustomerView> CustomerView { get; set; }
        public virtual DbSet<DeliveryRequestDetailsView> DeliveryRequestDetailsView { get; set; }
        public virtual DbSet<DeliveryRequestView> DeliveryRequestView { get; set; }
        public virtual DbSet<DeviceSIMView> DeviceSIMView { get; set; }
        public virtual DbSet<DeviceView> DeviceView { get; set; }
        public virtual DbSet<SIMCardContractView> SIMCardContractView { get; set; }
        public virtual DbSet<SIMCardView> SIMCardView { get; set; }
        public virtual DbSet<SystemServerIPView> SystemServerIPView { get; set; }
        public virtual DbSet<im_family> im_family { get; set; }
        public virtual DbSet<RpauxEmployeeView> RpauxEmployeeView { get; set; }
        public virtual DbSet<JobTitle> JobTitle { get; set; }
        public virtual DbSet<DeviceNamingType> DeviceNamingType { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<WarehouseInfo> WarehouseInfo { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<UserView> UserView { get; set; }
        public virtual DbSet<EmployeeView> EmployeeView { get; set; }
        public virtual DbSet<TransactionStatusHistory> TransactionStatusHistory { get; set; }
        public virtual DbSet<TransactionTechnician> TransactionTechnician { get; set; }
        public virtual DbSet<TransactionDetailsView> TransactionDetailsView { get; set; }
        public virtual DbSet<TransactionTechnicianView> TransactionTechnicianView { get; set; }
        public virtual DbSet<TransactionView> TransactionView { get; set; }
        public virtual DbSet<DeliveryRequestTechnician> DeliveryRequestTechnician { get; set; }
        public virtual DbSet<DeliveryRequestTechnicianView> DeliveryRequestTechnicianView { get; set; }
        public virtual DbSet<DeliveryRequest> DeliveryRequest { get; set; }
        public virtual DbSet<TransactionStatus> TransactionStatus { get; set; }
        public virtual DbSet<TransactionType> TransactionType { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<TransactionItem> TransactionItem { get; set; }
        public virtual DbSet<TransactionItemInfoHistory> TransactionItemInfoHistory { get; set; }
        public virtual DbSet<TransactionItemView> TransactionItemView { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<TransactionDetails> TransactionDetails { get; set; }
    
        public virtual int sp_InsertDolphinDeliveryNote(Nullable<int> cmp_seq, Nullable<int> salesmanId, Nullable<int> currencyId, string tra_status, string tra_user, Nullable<int> device_NewStatus, Nullable<int> deliveryRequest_NewStatus)
        {
            var cmp_seqParameter = cmp_seq.HasValue ?
                new ObjectParameter("cmp_seq", cmp_seq) :
                new ObjectParameter("cmp_seq", typeof(int));
    
            var salesmanIdParameter = salesmanId.HasValue ?
                new ObjectParameter("salesmanId", salesmanId) :
                new ObjectParameter("salesmanId", typeof(int));
    
            var currencyIdParameter = currencyId.HasValue ?
                new ObjectParameter("currencyId", currencyId) :
                new ObjectParameter("currencyId", typeof(int));
    
            var tra_statusParameter = tra_status != null ?
                new ObjectParameter("tra_status", tra_status) :
                new ObjectParameter("tra_status", typeof(string));
    
            var tra_userParameter = tra_user != null ?
                new ObjectParameter("tra_user", tra_user) :
                new ObjectParameter("tra_user", typeof(string));
    
            var device_NewStatusParameter = device_NewStatus.HasValue ?
                new ObjectParameter("device_NewStatus", device_NewStatus) :
                new ObjectParameter("device_NewStatus", typeof(int));
    
            var deliveryRequest_NewStatusParameter = deliveryRequest_NewStatus.HasValue ?
                new ObjectParameter("deliveryRequest_NewStatus", deliveryRequest_NewStatus) :
                new ObjectParameter("deliveryRequest_NewStatus", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_InsertDolphinDeliveryNote", cmp_seqParameter, salesmanIdParameter, currencyIdParameter, tra_statusParameter, tra_userParameter, device_NewStatusParameter, deliveryRequest_NewStatusParameter);
        }
    }
}
