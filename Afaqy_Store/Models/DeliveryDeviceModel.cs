using Afaqy_Store.DataLayer;
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
    public class DeliveryDeviceModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiDeliveryDevice/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public DeliveryDeviceModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class DeliveryDeviceViewModel : DeliveryDeviceView
    {

    }

    public class DeliveryDeviceDetailsViewModel : DeliveryDeviceViewModel
    {

    }
    
    [Bind(Include = "DeliveryItemId,cmp_seq,DeliveryDetailsId,DeviceId,Employee_aux_id,InstallingDateTime,DeviceNaming,DeviceNamingTypeId,AddToServer,TrackWithTechnician,ServerUpdated,Note,IsBlock,CreateUserId,CreateDate")]
    public class DeliveryDeviceEditBindModel : DeliveryDevice
    {
        internal static bool SendAddToServerNotification(string DeliveryNoteId, string referenceURL)
        {
            List<int?> Users = new List<int?>();
            //Notify Delivery Note Creator User
            DeliveryNote deliveryNote = new DeliveryNoteModel<DeliveryNote>().Get(DeliveryNoteId);
            Users.Add(deliveryNote.CreateUserId);
            //Notifiy Delivery Request Creator user
            DeliveryRequest deliveryRequest = new DeliveryRequestModel<DeliveryRequest>().Get(deliveryNote.DeliveryRequestId);
            Users.Add(deliveryRequest.CreateUserId);
            //Notifiy customer sales man and sales man manager
            rpaux customer = new CustomerModel<rpaux>().Get(deliveryNote.Customer_aux_id);
            if(customer.salecode != null)
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
            List<Employee> emps = new EmployeeModel<Employee>().GetBranchEmployee(warehouse.wa_costcenter , withManager: false, jobTitleId: (int)Classes.Common.DBEnums.JobTitle.Branch_Technicians_Manager);
            if(emps != null && emps.Count > 0)
            {
                Users.AddRange(emps.Select(x => x.UserId).ToList());
            }
            //Notifiy tecnician general manager
            emps = new EmployeeModel<Employee>().GetEmployee(withManager: false, jobTitleId: (int)Classes.Common.DBEnums.JobTitle.Technicians_General_Manager);
            if (emps != null && emps.Count > 0)
            {
                Users.AddRange(emps.Select(x => x.UserId).ToList());
            }
            //Notify Server Manager
            emps = new EmployeeModel<Employee>().GetEmployee(withManager: false, jobTitleId: (int)Classes.Common.DBEnums.JobTitle.Server_Managment_Manager);
            if (emps != null && emps.Count > 0)
            {
                Users.AddRange(emps.Select(x => x.UserId).ToList());
            }
            //Notify Warehouse Employee
            WarehouseInfo warehouseInfo = new WarehouseInfoModel<WarehouseInfo>().GetById(warehouse.wa_code);
            if(warehouseInfo != null)
            {
                int? empUserId = null;
                int? empManagerUserId = null;
                new EmployeeModel<Employee>().GetEmployeeUserWithManager(warehouseInfo.EmployeeId, ref empUserId, ref empManagerUserId);
                Users.Add(empUserId);
            }

            Users = Users.Where(x => x != null).Distinct().ToList();
            //Add notifications
            Notification[] notifications ;
            notifications = Users.Select(x => new Notification()
            {
                NotificationTitle = "Delivery Note Added to Server",
                NotificationContent = "A new delivery note has been added to server successfully",
                StyleClass = "fa fa-phone bg-themeprimary white",
                NotificationTypeId = (int)Classes.Common.DBEnums.NotificationType.DeliveryNote_ServerAddedNotification,
                ToUserId = x,
                CreateDate = DateTime.Now,
                IsRead = false,
                ReferenceId = DeliveryNoteId,
                ReferenceLink = referenceURL + "/"+ DeliveryNoteId,
                PopupWindow = true,
                PopupWindowClass = Classes.Common.Enums.PopupWindowClass.Meduim_Model
            }).ToArray();

            return new NotificationModel<Notification>().Import(notifications);
        }
    }

}