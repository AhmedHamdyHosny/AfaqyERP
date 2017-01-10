﻿using Afaqy_Store.DataLayer;
using Afaqy_Store.Models;
using Classes.Common;
using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Controllers
{
    public class DeviceController : BaseController<Device,DeviceViewModel,DeviceEditModel,DeviceModel<Device>,DeviceModel<DeviceViewModel>>
    {
        public DeviceController()
        {
            IndexRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { References = "DeviceStatus,ModelType" } };
            var filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeviceId", Operation = GenericDataFormat.FilterOperations.Equal });
            DetailsRequestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeviceStatus,ModelType" } };
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = "DeviceId,SerialNumber,IMI,Frameware", References = "DeviceStatus,ModelType" } };
            References = new List<Reference>();
            References.Add(new Reference() { TypeModel = typeof(SIMCardStatusModel<SIMCardViewModel>), ViewDataName = "ModelTypeId", DataValueField = "DeviceStatusId", DataTextField = "DeviceStatus_en", SelectColumns = "DeviceStatusId,DeviceStatus_en" });
            ExcelFileName = "Devices.xlsx";
        }
    }
}