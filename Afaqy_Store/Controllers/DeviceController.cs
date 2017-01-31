﻿using Afaqy_Store.DataLayer;
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
        public override void FuncPreIndexView(ref List<DeviceViewModel> model)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeviceStatus,DeviceModelType" } };
            model = new DeviceModel<DeviceViewModel>().Get(requestBody);
        }
        public override void FuncPreDetailsView(object id, ref List<DeviceViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeviceId", Operation = GenericDataFormat.FilterOperations.Equal });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeviceStatus,DeviceModelType" } };
            items = new DeviceModel<DeviceViewModel>().Get(requestBody);
        }
        public override void FuncPreInitCreateView()
        {
            //prepare dropdown list for item references
            List<DeviceModelType> deviceModelTypes =  new DeviceModelTypeModel<DeviceModelType>().GetAsDDLst("ModelTypeId,ModelTypeName", "ModelTypeName");
            ViewBag.ModelTypeId = deviceModelTypes.Select(x=> new Classes.Helper.CustomSelectListItem() { Text = x.ModelTypeName, Value = x.ModelTypeId.ToString()});
        }
        public override void FuncPreCreate(ref DeviceCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
            model.DeviceStatusId = (int)DBEnums.DeviceStatus.New;

            var deviceStatusHistory = new List<DeviceStatusHistory>();
            deviceStatusHistory.Add(new DeviceStatusHistory()
            {
                StatusId = (int)DBEnums.DeviceStatus.New,
                CreateUserId = User.UserId,
                CreateDate = DateTime.Now
            });
            model.DeviceStatusHistory = deviceStatusHistory;
        }
        public override void FuncPreInitEditView(object id, ref Device EditItem, ref DeviceEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new DeviceModel<Device>().Get(id);
            }
            if (EditItem != null)
            {
                model = new DeviceEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                List<DeviceModelType> deviceModelTypes = new DeviceModelTypeModel<DeviceModelType>().GetAsDDLst("ModelTypeId,ModelTypeName", "ModelTypeName");
                model.ModelType = deviceModelTypes.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.ModelTypeName, Value = x.ModelTypeId.ToString(), Selected = (selectedItem.ModelTypeId == x.ModelTypeId) });
            }
        }
        public override void FuncPreEdit(ref object id, ref DeviceEditBindModel EditItem)
        {
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "Devices.xlsx";
            //filters
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = "DeviceId,SerialNumber,IMEI,Firmware", References = "DeviceStatus,DeviceModelType" } };
        }
    }
}