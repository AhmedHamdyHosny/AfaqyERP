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
            IndexRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { References = "DeviceStatus,DeviceModelType" } };
            var filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeviceId", Operation = GenericDataFormat.FilterOperations.Equal });
            DetailsRequestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeviceStatus,DeviceModelType" } };
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = "DeviceId,SerialNumber,IMEI,Firmware", References = "DeviceStatus,DeviceModelType" } };
            CreateReferences = new List<Reference>();
            CreateReferences.Add(new Reference() { TypeModel = typeof(DeviceModelTypeModel<DeviceModelTypeViewModel>), ViewDataName = "ModelTypeId", DataValueField = "ModelTypeId", DataTextField = "ModelTypeName", SelectColumns = "ModelTypeId,ModelTypeName" });

            EditReferences = new List<Reference>();
            EditReferences.Add(new Reference() { TypeModel = typeof(DeviceModelTypeModel<DeviceModelTypeViewModel>), ViewDataName = "ModelTypeId", DataValueField = "ModelTypeId", DataTextField = "ModelTypeName", SelectColumns = "ModelTypeId,ModelTypeName", PropertyName = "ModelType" });

            ActionItemsPropertyValue = new List<ActionItemPropertyValue>();
            //for test
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Create, PropertyName = "CreateUserId", Value = 1 });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Create, PropertyName = "CreateDate", Value = DateTime.Now });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Create, PropertyName = "DeviceStatusId", Value = (int)DBEnums.DeviceStatus.New });

            var deviceStatusHistory = new List<DeviceStatusHistory>();
            deviceStatusHistory.Add(new DeviceStatusHistory()
            {
                StatusId = (int)DBEnums.DeviceStatus.New,
                //for test
                CreateUserId = 1,
                CreateDate = DateTime.Now
            });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Create, PropertyName = "DeviceStatusHistory", Value = deviceStatusHistory });


            //for test
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Edit, PropertyName = "ModifyUserId", Value = 1 });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Edit, PropertyName = "ModifyDate", Value = DateTime.Now });
            PK_PropertyName = "DeviceId";

            ExcelFileName = "Devices.xlsx";
        }

        // POST: Device/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(DeviceCreateModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //for test
        //        var userId = 1;
        //        model.CreateUserId = userId;
        //        model.CreateDate = DateTime.Now;

        //        //set device status to initial status
        //        model.DeviceStatusId = (int)DBEnums.DeviceStatus.New;
        //        Device device = new DeviceModel<Device>().Insert(model);

        //        //insert current status to device status history
        //        var deviceStatusHistory = new DeviceStatusHistory();
        //        deviceStatusHistory.DeviceId = device.DeviceId;
        //        deviceStatusHistory.StatusId = device.DeviceStatusId;
        //        deviceStatusHistory.CreateUserId = userId;
        //        deviceStatusHistory.CreateDate = DateTime.Now;
        //        new DeviceStatusHistoryModel<DeviceStatusHistory>().Insert(deviceStatusHistory);

        //        TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Create };
        //        return RedirectToAction("Index");
        //    }

        //    var ModelTypes = new DeviceModelTypeModel<DeviceModelType>().Get();
        //    ViewBag.ModelTypeId = new SelectList(ModelTypes, "ModelTypeId", "ModelTypeName", model.ModelTypeId);
        //    return View(model);
        //}


        // GET: Device/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Device device = new DeviceModel<Device>().Get((int)id);
        //    if (device == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var deviceStatus = new DeviceStatusModel<DeviceStatus>().Get();
        //    var deviceModelTypes = new DeviceModelTypeModel<DeviceModelType>().Get();
        //    var model = new DeviceEditModel()
        //    {
        //        EditItem = device,
        //        ModelType = deviceModelTypes.Select(x => new SelectListItem() { Selected = device.ModelTypeId == x.ModelTypeId, Text = x.ModelTypeName, Value = x.ModelTypeId.ToString() }),
        //        Status = deviceStatus.Select(x => new SelectListItem() { Selected = device.DeviceStatusId == x.DeviceStatusId, Text = x.DeviceStatus_en, Value = x.DeviceStatusId.ToString() })
        //    };
        //    return View(model);
        //}

        // POST: Device/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "DeviceId,SerialNumber,IMEI,Firmware,ModelTypeId,DeviceStatusId,CreateUserId,CreateDate")] Device EditItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //for test
        //        EditItem.ModifyUserId = 1;
        //        EditItem.ModifyDate = DateTime.Now;

        //        new DeviceModel<Device>().Update(EditItem, EditItem.DeviceId);
        //        TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Edit };
        //        return RedirectToAction("Index");
        //    }
        //    var deviceStatus = new DeviceStatusModel<DeviceStatus>().Get();
        //    var deviceModelTypes = new DeviceModelTypeModel<DeviceModelType>().Get();
        //    var model = new DeviceEditModel()
        //    {
        //        EditItem = new DeviceModel<Device>().Get(EditItem.DeviceId),
        //        ModelType = deviceModelTypes.Select(x => new SelectListItem() { Selected = EditItem.ModelTypeId == x.ModelTypeId, Text = x.ModelTypeName, Value = x.ModelTypeId.ToString() }),
        //        Status = deviceStatus.Select(x => new SelectListItem() { Selected = EditItem.DeviceStatusId == x.DeviceStatusId, Text = x.DeviceStatus_en, Value = x.DeviceStatusId.ToString() })
        //    };
        //    return View(model);
        //}

    }
}