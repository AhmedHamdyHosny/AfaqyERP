using Afaqy_Store.DataLayer;
using Classes.Helper;
using Classes.Utilities;
using GenericApiController.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Models
{
    public class DeliveryRequestModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeliveryRequest/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public DeliveryRequestModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeliveryRequestViewModel : DeliveryRequestView
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
        public string InstallationService
        {
            get
            {
                if (this.WithInstallationService != null)
                {
                    return (bool)this.WithInstallationService ? Resources.Resource.True : Resources.Resource.False;
                }
                else
                {
                    return null;
                }
            }
        }
    }
    public class DeliveryRequestIndexViewModel : DeliveryRequestView
    {

    }
    public class DeliveryRequestDetailsViewModel : DeliveryRequestViewModel
    {
        public List<RequestDetails_DetailsViewModel> DeliveryRequestDetails { get; set; }
        public List<DeliveryRequestTechnicianViewModel> DeliveryRequestTechnician { get; set; }
    }
    [Bind(Include = "DeliveryRequestId,POS_ps_code,Warehouse_wa_code,SaleTransactionTypeId,Customer_aux_id,CustomerName,CustomerContact_serial,AlternativeContactName,AlternativeContactTelephone,DeliveryRequestDate_Str,DeliveryRequestTime_Str,SystemId,WithInstallationService,Note,DeliveryRequestDetails")]
    public class DeliveryRequestCreateBindModel : DeliveryRequest
    {
        public string DeliveryRequestDate_Str { get; set; }
        public string DeliveryRequestTime_Str { get; set; }

        internal static bool SendAfterApproveDeliveryRequestNotification(DeliveryRequest deliveryRequest, string detailsReferenceURL, string assignReferenceLnk, int userId)
        {
            List<Notification> notifications = new List<Notification>();
            //Notify Warehouse Employee
            WarehouseInfo warehouseInfo = new WarehouseInfoModel<WarehouseInfo>().GetById(deliveryRequest.Warehouse_wa_code);
            if (warehouseInfo != null)
            {
                int? empUserId = null;
                int? empManagerUserId = null;
                new EmployeeModel<Employee>().GetEmployeeUserWithManager(warehouseInfo.EmployeeId, ref empUserId, ref empManagerUserId);
                //add notification to store employee
                notifications.Add(new Notification()
                {
                    NotificationTitle = Resources.Store.ApprovedDeliveryRequestNotificationTitle,
                    NotificationContent = Resources.Store.ApprovedDeliveryRequestNotificationContent,
                    StyleClass = Classes.Common.Constant.NotificationStyleClass.DeliveryRequest_DefaultStyleClass,
                    ToUserId = (int)empUserId,
                    CreateDate = DateTime.Now,
                    IsRead = false,
                    NotificationTypeId = (int)Classes.Common.DBEnums.NotificationType.DeliveryRequest_ApproveNotification,
                    ReferenceId = deliveryRequest.DeliveryRequestId.ToString(),
                    ReferenceLink = detailsReferenceURL + "/" + deliveryRequest.DeliveryRequestId,
                    PopupWindow = true,
                    PopupWindowClass = Classes.Common.Enums.PopupWindowClass.Meduim_Model
                });
            }

            List<int?> Users = new List<int?>();
            //Notifiy branch technician manager and tecnician general manager
            //get delivery request warehouse branch
            List<GenericDataFormat.FilterItems> warehouseFilters = new List<GenericDataFormat.FilterItems>();
            warehouseFilters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "wa_code",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = deliveryRequest.Warehouse_wa_code,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            im_warehouse warehouse = new WarehouseModel<im_warehouse>().Get(new GenericDataFormat() { Filters = warehouseFilters}).SingleOrDefault();
            //warehouse.wa_costcenter (BranchId)
            List<Employee> emps = new EmployeeModel<Employee>().GetBranchEmployee(warehouse.wa_costcenter, withManager: false, jobTitleId: (int)Classes.Common.DBEnums.JobTitle.Branch_Technicians_Manager);
            if (emps != null && emps.Count > 0)
            {
                Users.AddRange(emps.Select(x => x.UserId).ToList());
            }
            //Notifiy tecnician general manager
            emps = new EmployeeModel<Employee>().GetEmployee(withManager: false, jobTitleId: (int)Classes.Common.DBEnums.JobTitle.Technicians_General_Manager);
            if (emps != null && emps.Count > 0)
            {
                Users.AddRange(emps.Select(x => x.UserId).ToList());
            }
            Users = Users.Where(x => x != null).Distinct().ToList();
            if(Users.Count > 0)
            {
                //Add Notifications
                notifications.AddRange(Users.Select(x => new Notification()
                {
                    NotificationTitle = Resources.Technician.ApprovedDeliveryRequestNotificationTitle,
                    NotificationContent = Resources.Technician.ApprovedDeliveryRequestNotificationContent,
                    StyleClass = Classes.Common.Constant.NotificationStyleClass.DeliveryRequest_DefaultStyleClass,
                    ToUserId = x,
                    CreateDate = DateTime.Now,
                    IsRead = false,
                    NotificationTypeId = (int)Classes.Common.DBEnums.NotificationType.DeliveryRequest_ApproveNotification,
                    ReferenceId = deliveryRequest.DeliveryRequestId.ToString(),
                    ReferenceLink = assignReferenceLnk + "/" + deliveryRequest.DeliveryRequestId,
                    PopupWindow = true,
                    PopupWindowClass = Classes.Common.Enums.PopupWindowClass.Meduim_Model
                }));
            }
            
            //Notifiy customer sales man and sales man manager
            Users.Clear();
            rpaux customer = new CustomerModel<rpaux>().Get(deliveryRequest.Customer_aux_id);
            if (customer.salecode != null)
            {
                int? salesUserId = null;
                int? salesManagerUserId = null;
                new EmployeeModel<Employee>().GetEmployeeUserWithManager((int)customer.salecode, ref salesUserId, ref salesManagerUserId);
                Users.Add(salesUserId);
                Users.Add(salesManagerUserId);
            }
            Users = Users.Where(x => x != null && x != userId).Distinct().ToList();
            if(Users.Count > 0)
            {
                //Add Notifications
                notifications.AddRange(Users.Select(x => new Notification()
                {
                    NotificationTitle = Resources.Sales.ApprovedDeliveryRequestNotificationTitle,
                    NotificationContent = Resources.Sales.ApprovedDeliveryRequestNotificationContent,
                    StyleClass = Classes.Common.Constant.NotificationStyleClass.DeliveryRequest_DefaultStyleClass,
                    ToUserId = x,
                    CreateDate = DateTime.Now,
                    IsRead = false,
                    NotificationTypeId = (int)Classes.Common.DBEnums.NotificationType.DeliveryRequest_ApproveNotification,
                    ReferenceId = deliveryRequest.DeliveryRequestId.ToString(),
                    ReferenceLink = detailsReferenceURL + "/" + deliveryRequest.DeliveryRequestId,
                    PopupWindow = true,
                    PopupWindowClass = Classes.Common.Enums.PopupWindowClass.Meduim_Model
                }));
            }

            return new NotificationModel<Notification>().Import(notifications.ToArray());
        }
        
    }
    [Bind(Include = "DeliveryRequestId,POS_ps_code,Warehouse_wa_code,SaleTransactionTypeId,Customer_aux_id,CustomerName,CustomerContact_serial,AlternativeContactName,AlternativeContactTelephone,DeliveryRequestDate_Str,DeliveryRequestTime_Str,SystemId,WithInstallationService,DeliveryRequestStatusId,cmp_seq,Note,DeliveryRequestDetails,IsBlock,CreateUserId,CreateDate")]
    public class DeliveryRequestEditBindModel : DeliveryRequest
    {
        //public string POSId { get; set; }
        //public string WarehouseId { get; set; }
        public string DeliveryRequestDate_Str { get; set; }
        public string DeliveryRequestTime_Str { get; set; }

        internal static bool SendAfterAssignDeliveryRequestNotification(DeliveryRequest deliveryRequest, string detailsReferenceURL, string createDeliveryNoteReferenceLnk, int userId)
        {
            List<Notification> notifications = new List<Notification>();
            //Notify Warehouse Employee
            WarehouseInfo warehouseInfo = new WarehouseInfoModel<WarehouseInfo>().GetById(deliveryRequest.Warehouse_wa_code);
            if (warehouseInfo != null)
            {
                int? empUserId = null;
                int? empManagerUserId = null;
                new EmployeeModel<Employee>().GetEmployeeUserWithManager(warehouseInfo.EmployeeId, ref empUserId, ref empManagerUserId);
                //add notification to store employee
                notifications.Add(new Notification()
                {
                    NotificationTitle = Resources.Store.TechnicianAssignedDeliveryRequestNotificationTitle,
                    NotificationContent = Resources.Store.TechnicianAssignedDeliveryRequestNotificationContent,
                    StyleClass = Classes.Common.Constant.NotificationStyleClass.DeliveryRequest_DefaultStyleClass,
                    ToUserId = (int)empUserId,
                    CreateDate = DateTime.Now,
                    IsRead = false,
                    NotificationTypeId = (int)Classes.Common.DBEnums.NotificationType.DeliveryRequest_TechnicianAssignedNotification,
                    ReferenceId = deliveryRequest.DeliveryRequestId.ToString(),
                    ReferenceLink = createDeliveryNoteReferenceLnk + "/" + deliveryRequest.DeliveryRequestId,
                    PopupWindow = true,
                    PopupWindowClass = Classes.Common.Enums.PopupWindowClass.Meduim_Model
                });
            }

            List<int?> Users = new List<int?>();
            //Notifiy customer sales man and sales man manager
            rpaux customer = new CustomerModel<rpaux>().Get(deliveryRequest.Customer_aux_id);
            if (customer.salecode != null)
            {
                int? salesUserId = null;
                int? salesManagerUserId = null;
                new EmployeeModel<Employee>().GetEmployeeUserWithManager((int)customer.salecode, ref salesUserId, ref salesManagerUserId);
                Users.Add(salesUserId);
                Users.Add(salesManagerUserId);
            }
            Users = Users.Where(x => x != null && x != userId).Distinct().ToList();
            if (Users.Count > 0)
            {
                //Add Notifications
                notifications.AddRange(Users.Select(x => new Notification()
                {
                    NotificationTitle = Resources.Sales.TechnicianAssignedDeliveryRequestNotificationTitle,
                    NotificationContent = Resources.Sales.TechnicianAssignedDeliveryRequestNotificationContent,
                    StyleClass = Classes.Common.Constant.NotificationStyleClass.DeliveryRequest_DefaultStyleClass,
                    ToUserId = x,
                    CreateDate = DateTime.Now,
                    IsRead = false,
                    NotificationTypeId = (int)Classes.Common.DBEnums.NotificationType.DeliveryRequest_TechnicianAssignedNotification,
                    ReferenceId = deliveryRequest.DeliveryRequestId.ToString(),
                    ReferenceLink = detailsReferenceURL + "/" + deliveryRequest.DeliveryRequestId,
                    PopupWindow = true,
                    PopupWindowClass = Classes.Common.Enums.PopupWindowClass.Meduim_Model
                }));
            }

            return new NotificationModel<Notification>().Import(notifications.ToArray());
            return true;
        }
    }
    public class DeliveryRequestEditModel
    {
        public DeliveryRequest EditItem { get; set; }
        public List<DeliveryRequestDetailsView> DeliveryRequestDetails { get; set; }
        public IEnumerable<CustomSelectListItem> PointOfSale { get; set; }
        public IEnumerable<CustomSelectListItem> Warehouse { get; set; }
        public IEnumerable<CustomSelectListItem> SaleTransactionType { get; set; }
        public IEnumerable<CustomSelectListItem> Customer { get; set; }
        public IEnumerable<CustomSelectListItem> TechniqueSystem { get; set; }
        public IEnumerable<CustomSelectListItem> CustomerContact { get; set; }
    }
}