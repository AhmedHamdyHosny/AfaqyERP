using Afaqy_Store.DataLayer;
using Afaqy_Store.Models;
using Classes.Common;
using Classes.Utilities;
using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static Classes.Common.Enums;

namespace Afaqy_Store.Controllers
{
    public class DeviceController : BaseController<Device,DeviceViewModel, DeviceCreateBindModel, DeviceEditBindModel, DeviceEditModel,DeviceModel<Device>,DeviceModel<DeviceViewModel>>
    {
        public DeviceController()
        {
            PK_PropertyName = "DeviceId";
            List<GenericDataFormat.FilterItems> filters = null;
            ActionItemsPropertyValue = new List<ActionItemPropertyValue>();
            var user = new UserViewModel().GetUserFromSession();
            var userId = user.UserId;

            #region Index
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsDeleted", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            IndexRequestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeviceStatus,DeviceModelType" } };
            #endregion

            #region Details
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeviceId", Operation = GenericDataFormat.FilterOperations.Equal });
            DetailsRequestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeviceStatus,DeviceModelType" } };
            #endregion

            #region Create
            //Create view Dropdown Lists
            CreateReferences = new List<Reference>();
            CreateReferences.Add(new Reference() { TypeModel = typeof(DeviceModelTypeModel<DeviceModelTypeViewModel>), ViewDataName = "ModelTypeId", DataValueField = "ModelTypeId", DataTextField = "ModelTypeName", SelectColumns = "ModelTypeId,ModelTypeName" });

            //on create dependences
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Create, PropertyName = "CreateUserId", Value = userId });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Create, PropertyName = "CreateDate", Value = DateTime.Now });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Create, PropertyName = "DeviceStatusId", Value = (int)DBEnums.DeviceStatus.New });

            var deviceStatusHistory = new List<DeviceStatusHistory>();
            deviceStatusHistory.Add(new DeviceStatusHistory()
            {
                StatusId = (int)DBEnums.DeviceStatus.New,
                CreateUserId = userId,
                CreateDate = DateTime.Now
            });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Create, PropertyName = "DeviceStatusHistory", Value = deviceStatusHistory });
            #endregion

            #region Edit
            //edit view Dropdown Lists
            EditReferences = new List<Reference>();
            EditReferences.Add(new Reference() { TypeModel = typeof(DeviceModelTypeModel<DeviceModelTypeViewModel>), ViewDataName = "ModelTypeId", DataValueField = "ModelTypeId", DataTextField = "ModelTypeName", SelectColumns = "ModelTypeId,ModelTypeName", PropertyName = "ModelType" });

            //on edit dependences
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Edit, PropertyName = "ModifyUserId", Value = userId });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Edit, PropertyName = "ModifyDate", Value = DateTime.Now });
            #endregion

            #region Export
            ExportFileName = "Devices.xlsx";
            //filters
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsDeleted", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = "DeviceId,SerialNumber,IMEI,Firmware", References = "DeviceStatus,DeviceModelType" } };
            #endregion
            
        }

        

    }
}