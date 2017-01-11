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
    public class DeviceController : BaseController<Device,DeviceViewModel,DeviceEditModel,DeviceModel<Device>,DeviceModel<DeviceViewModel>>
    {
        public DeviceController()
        {
            IndexRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { References = "DeviceStatus,DeviceModelType" } };
            var filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeviceId", Operation = GenericDataFormat.FilterOperations.Equal });
            DetailsRequestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeviceStatus,DeviceModelType" } };
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = "DeviceId,SerialNumber,IMEI,Firmware", References = "DeviceStatus,DeviceModelType" } };
            References = new List<Reference>();
            References.Add(new Reference() { TypeModel = typeof(DeviceModelTypeModel<DeviceModelTypeViewModel>), ViewDataName = "ModelTypeId", DataValueField = "ModelTypeId", DataTextField = "ModelTypeName", SelectColumns = "ModelTypeId,ModelTypeName" });
            ExcelFileName = "Devices.xlsx";
        }

        // POST: Device/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeviceId,SerialNumber,IMEI,Firmware,ModelTypeId")] Device model)
        {
            if (ModelState.IsValid)
            {
                //for test
                model.CreateUserId = 1;
                model.CreateDate = DateTime.Now;

                new DeviceModel<Device>().Insert(model);
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Create };
                return RedirectToAction("Index");
            }

            var ModelTypes = new DeviceModelTypeModel<DeviceModelType>().Get();
            ViewBag.ModelTypeId = new SelectList(ModelTypes, "ModelTypeId", "ModelTypeName", model.ModelTypeId);
            return View(model);
        }


        // GET: Device/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = new DeviceModel<Device>().Get((int)id);
            if (device == null)
            {
                return HttpNotFound();
            }

            var deviceStatus = new DeviceStatusModel<DeviceStatus>().Get();
            var deviceModelTypes = new DeviceModelTypeModel<DeviceModelType>().Get();

            var model = new DeviceEditModel()
            {
                EditItem = device,
                ModelType = deviceModelTypes.Select(x => new SelectListItem() { Selected = device.ModelTypeId == x.ModelTypeId, Text = x.ModelTypeName, Value = x.ModelTypeId.ToString() }),
                Status = deviceStatus.Select(x => new SelectListItem() { Selected = device.DeviceStatusId == x.DeviceStatusId, Text = x.DeviceStatus_en, Value = x.DeviceStatusId.ToString() })
            };

            return View(model);
        }

        // POST: Device/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceId,SerialNumber,IMEI,Firmware,ModelTypeId,CreateUserId,CreateDate")] Device EditItem)
        {
            if (ModelState.IsValid)
            {
                //for test
                EditItem.ModifyUserId = 1;
                EditItem.ModifyDate = DateTime.Now;

                new DeviceModel<Device>().Update(EditItem, EditItem.DeviceId);
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Edit };
                return RedirectToAction("Index");
            }
            var deviceStatus = new DeviceStatusModel<DeviceStatus>().Get();
            var deviceModelTypes = new DeviceModelTypeModel<DeviceModelType>().Get();
            var model = new DeviceEditModel()
            {
                EditItem = new DeviceModel<Device>().Get(EditItem.DeviceId),
                ModelType = deviceModelTypes.Select(x => new SelectListItem() { Selected = EditItem.ModelTypeId == x.ModelTypeId, Text = x.ModelTypeName, Value = x.ModelTypeId.ToString() }),
                Status = deviceStatus.Select(x => new SelectListItem() { Selected = EditItem.DeviceStatusId == x.DeviceStatusId, Text = x.DeviceStatus_en, Value = x.DeviceStatusId.ToString() })
            };
            return View(model);
        }

    }
}