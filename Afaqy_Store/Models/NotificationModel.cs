using Afaqy_Store.DataLayer;
using Classes.Common;
using Classes.Utilities;
using GenericApiController.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afaqy_Store.Models
{

    public class NotificationModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiNotification/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public NotificationModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class NotificationViewModel : Notification
    {
       
    }
    
    public class NotificationDetailsViewModel : NotificationViewModel
    {
    }

    public class NotificationCreateBindModel : Notification
    {
        #region Delivery Request
        #endregion

        #region Delivery Note
        internal bool SendInDeliveryPhaseNotification(string DeliveryNoteId, int currentUserId, string referenceURL, string deliveryNoteServerAddURL)
        {
            List<int?> Users = new List<int?>();
            //Notify Delivery Note Creator User
            Transaction deliveryNote = new DeliveryNoteModel<Transaction>().Get(DeliveryNoteId);
            Users.Add(deliveryNote.CreateUserId);
            //Notifiy Delivery Request Creator user
            DeliveryRequest deliveryRequest = new DeliveryRequestModel<DeliveryRequest>().Get(deliveryNote.DeliveryRequestId);
            Users.Add(deliveryRequest.CreateUserId);
            //Notifiy customer sales man and sales man manager
            rpaux customer = new CustomerModel<rpaux>().Get(deliveryNote.Customer_aux_id);
            if (customer.salecode != null)
            {
                int? salesUserId = null;
                int? salesManagerUserId = null;
                new EmployeeModel<Employee>().GetEmployeeUserWithManager((int)customer.salecode, ref salesUserId, ref salesManagerUserId);
                Users.Add(salesUserId);
                Users.Add(salesManagerUserId);
            }

            //Notifiy branch technician manager and tecnician general manager
            //get delivery note warehouse branch
            im_warehouse warehouse = new WarehouseModel<im_warehouse>().Get(deliveryNote.Warehouse_wa_code);
            //warehouse.wa_costcenter (BranchId)
            List<Employee> emps = new EmployeeModel<Employee>().GetBranchEmployee(warehouse.wa_costcenter, withManager: false, jobTitleId: (int)DBEnums.JobTitle.Branch_Technicians_Manager);
            if (emps != null && emps.Count > 0)
            {
                Users.AddRange(emps.Select(x => x.UserId).ToList());
            }
            //Notifiy tecnician general manager
            emps = new EmployeeModel<Employee>().GetEmployee(withManager: false, jobTitleId: (int)DBEnums.JobTitle.Technicians_General_Manager);
            if (emps != null && emps.Count > 0)
            {
                Users.AddRange(emps.Select(x => x.UserId).ToList());
            }
            
            //remove current user from list
            Users = Users.Where(x => x != null && x != currentUserId).Distinct().ToList();
            //Add notifications
            List<Notification> notifications;
            notifications = Users.Select(x => new Notification()
            {
                NotificationTitle = Resources.Store.DeliveryRequestInDeliveryPhaseNotificationTitle,
                NotificationContent = Resources.Store.DeliveryRequestInDeliveryPhaseNotificationContent,
                StyleClass = Constant.NotificationStyleClass.DeliveryRequest_DefaultStyleClass,
                NotificationTypeId = (int)DBEnums.NotificationType.DeliveryRequest_InDeliveryPhaseNotification,
                ToUserId = x,
                CreateDate = DateTime.Now,
                IsRead = false,
                ReferenceId = DeliveryNoteId,
                ReferenceLink = referenceURL + "/" + DeliveryNoteId,
                PopupWindow = true,
                PopupWindowClass = Enums.PopupWindowClass.Meduim_Model
            }).ToList();

            //clear users
            Users.Clear();
            //get Server Managment Department Employees
            emps = new EmployeeModel<Employee>().GetEmployee(departmentId: (int)DBEnums.Department.Server_Management);
            if (emps != null && emps.Count > 0)
            {
                Users.AddRange(emps.Select(x => x.UserId).ToList());
            }
            //Notify Server Employees
            notifications.AddRange(Users.Select(x=> new Notification()
            {
                NotificationTitle = Resources.ServerManagement.DeliveryNoteServerAddNotificationTitle,
                NotificationContent = Resources.ServerManagement.DeliveryNoteServerAddNotificationContent,
                StyleClass = Constant.NotificationStyleClass.DeliveryRequest_DefaultStyleClass,
                NotificationTypeId = (int)DBEnums.NotificationType.DeliveryNote_ServerAddNotification,
                ToUserId = x,
                CreateDate = DateTime.Now,
                IsRead = false,
                ReferenceId = DeliveryNoteId,
                ReferenceLink = deliveryNoteServerAddURL + "/" + DeliveryNoteId,
                PopupWindow = true,
                PopupWindowClass = Enums.PopupWindowClass.Meduim_Model
            }));

            return new NotificationModel<Notification>().Import(notifications.ToArray());
        }
        internal bool SendServerAddedNotification(string DeliveryNoteId, string referenceURL)
        {
            List<int?> Users = new List<int?>();
            //Notify Delivery Note Creator User
            Transaction deliveryNote = new DeliveryNoteModel<Transaction>().Get(DeliveryNoteId);
            Users.Add(deliveryNote.CreateUserId);
            //Notifiy Delivery Request Creator user
            DeliveryRequest deliveryRequest = new DeliveryRequestModel<DeliveryRequest>().Get(deliveryNote.DeliveryRequestId);
            Users.Add(deliveryRequest.CreateUserId);
            //Notifiy customer sales man and sales man manager
            rpaux customer = new CustomerModel<rpaux>().Get(deliveryNote.Customer_aux_id);
            if (customer.salecode != null)
            {
                int? salesUserId = null;
                int? salesManagerUserId = null;
                new EmployeeModel<Employee>().GetEmployeeUserWithManager((int)customer.salecode, ref salesUserId, ref salesManagerUserId);
                Users.Add(salesUserId);
                Users.Add(salesManagerUserId);

            }
            //Notifiy branch technician manager and tecnician general manager
            //get delivery note warehouse branch
            im_warehouse warehouse = new WarehouseModel<im_warehouse>().Get(deliveryNote.Warehouse_wa_code);
            //warehouse.wa_costcenter (BranchId)
            List<Employee> emps = new EmployeeModel<Employee>().GetBranchEmployee(warehouse.wa_costcenter, withManager: false, jobTitleId: (int)DBEnums.JobTitle.Branch_Technicians_Manager);
            if (emps != null && emps.Count > 0)
            {
                Users.AddRange(emps.Select(x => x.UserId).ToList());
            }
            //Notifiy tecnician general manager
            emps = new EmployeeModel<Employee>().GetEmployee(withManager: false, jobTitleId: (int)DBEnums.JobTitle.Technicians_General_Manager);
            if (emps != null && emps.Count > 0)
            {
                Users.AddRange(emps.Select(x => x.UserId).ToList());
            }
            //Notify Server Manager
            emps = new EmployeeModel<Employee>().GetEmployee(withManager: false, jobTitleId: (int)DBEnums.JobTitle.Server_Managment_Manager);
            if (emps != null && emps.Count > 0)
            {
                Users.AddRange(emps.Select(x => x.UserId).ToList());
            }
            //Notify Warehouse Employee
            WarehouseInfo warehouseInfo = new WarehouseInfoModel<WarehouseInfo>().GetById(warehouse.wa_code);
            if (warehouseInfo != null)
            {
                int? empUserId = null;
                int? empManagerUserId = null;
                new EmployeeModel<Employee>().GetEmployeeUserWithManager(warehouseInfo.EmployeeId, ref empUserId, ref empManagerUserId);
                Users.Add(empUserId);
            }

            Users = Users.Where(x => x != null).Distinct().ToList();
            //Add notifications
            Notification[] notifications;
            notifications = Users.Select(x => new Notification()
            {
                NotificationTitle = Resources.ServerManagement.DeliveryNoteServerAddedNotificationTitle,
                NotificationContent = Resources.ServerManagement.DeliveryNoteServerAddedNotificationContent,
                StyleClass = Constant.NotificationStyleClass.DeliveryRequest_DefaultStyleClass,
                NotificationTypeId = (int)DBEnums.NotificationType.DeliveryNote_ServerAddedNotification,
                ToUserId = x,
                CreateDate = DateTime.Now,
                IsRead = false,
                ReferenceId = DeliveryNoteId,
                ReferenceLink = referenceURL + "/" + DeliveryNoteId,
                PopupWindow = true,
                PopupWindowClass = Enums.PopupWindowClass.Meduim_Model
            }).ToArray();

            return new NotificationModel<Notification>().Import(notifications);
        }
        internal bool SendDeliveryNoteTechnicalApprovedNotification(string DeliveryNoteId, string referenceURL, string deliveryStoreNamingURL)
        {
            List<int?> Users = new List<int?>();
            //Notify Delivery Note Creator User
            Transaction deliveryNote = new DeliveryNoteModel<Transaction>().Get(DeliveryNoteId);
            Users.Add(deliveryNote.CreateUserId);
            //Notifiy Delivery Request Creator user
            DeliveryRequest deliveryRequest = new DeliveryRequestModel<DeliveryRequest>().Get(deliveryNote.DeliveryRequestId);
            Users.Add(deliveryRequest.CreateUserId);
            //Notifiy customer sales man and sales man manager
            rpaux customer = new CustomerModel<rpaux>().Get(deliveryNote.Customer_aux_id);
            if (customer.salecode != null)
            {
                int? salesUserId = null;
                int? salesManagerUserId = null;
                new EmployeeModel<Employee>().GetEmployeeUserWithManager((int)customer.salecode, ref salesUserId, ref salesManagerUserId);
                Users.Add(salesUserId);
                Users.Add(salesManagerUserId);
            }

            Users = Users.Where(x => x != null).Distinct().ToList();
            //Add notifications
            List<Notification> notifications;
            notifications = Users.Select(x => new Notification()
            {
                NotificationTitle = Resources.Technician.DeliveryNoteTechnicalApprovedNotificationTitle,
                NotificationContent = Resources.Technician.DeliveryNoteTechnicalApprovedNotificationContent,
                StyleClass = Constant.NotificationStyleClass.DeliveryRequest_DefaultStyleClass,
                NotificationTypeId = (int)DBEnums.NotificationType.DeliveryNote_TechnicalApprovedNotification,
                ToUserId = x,
                CreateDate = DateTime.Now,
                IsRead = false,
                ReferenceId = DeliveryNoteId,
                ReferenceLink = referenceURL + "/" + DeliveryNoteId,
                PopupWindow = true,
                PopupWindowClass = Enums.PopupWindowClass.Meduim_Model
            }).ToList();

            //get delivery note warehouse branch
            im_warehouse warehouse = new WarehouseModel<im_warehouse>().Get(deliveryNote.Warehouse_wa_code);
            //Notify Warehouse Employee
            WarehouseInfo warehouseInfo = new WarehouseInfoModel<WarehouseInfo>().GetById(warehouse.wa_code);
            if (warehouseInfo != null)
            {
                int? empUserId = null;
                int? empManagerUserId = null;
                new EmployeeModel<Employee>().GetEmployeeUserWithManager(warehouseInfo.EmployeeId, ref empUserId, ref empManagerUserId);
                notifications.Add(new Notification()
                {
                    NotificationTitle = Resources.Store.DeliveryNoteStoreDeviceNamingNotificationTitle,
                    NotificationContent = Resources.Store.DeliveryNoteStoreDeviceNamingNotificationContent,
                    StyleClass = Constant.NotificationStyleClass.DeliveryRequest_DefaultStyleClass,
                    NotificationTypeId = (int)DBEnums.NotificationType.DeliveryNote_StoreDeviceNamingNotification,
                    ToUserId = empUserId,
                    CreateDate = DateTime.Now,
                    IsRead = false,
                    ReferenceId = DeliveryNoteId,
                    ReferenceLink = deliveryStoreNamingURL + "/" + DeliveryNoteId,
                    PopupWindow = true,
                    PopupWindowClass = Enums.PopupWindowClass.Meduim_Model
                });
            }

            return new NotificationModel<Notification>().Import(notifications.ToArray());
        }

        internal bool UpdateDeliveryNoteServerAdd(string deliveryNoteId)
        {
            //get all Unreaded Notification of Delivery Note which type is DeliveryNote_ServerAdd
            var filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "IsRead",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = false,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "ReferenceId",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = deliveryNoteId,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            var requestBody = new GenericDataFormat() { Filters = filters };
            var notifications = new NotificationModel<Notification>().Get(requestBody);
            List<UpdateItemFormat<Notification>>  items = notifications.Select(x => { x.IsRead = true; return new UpdateItemFormat<Notification>() { id = x.NotificationId, newValue = x }; }  ).ToList();
            new NotificationModel<Notification>().Update(items);
            return true;
        }
        #endregion
    }
}