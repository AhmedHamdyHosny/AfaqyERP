using Afaqy_Store.DataLayer;
using Afaqy_Store.Models;
using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Controllers
{
    public class DeviceStatusController : BaseController<DeviceStatus, DeviceStatusViewModel, DeviceStatusIndexViewModel, DeviceStatusDetailsViewModel, DeviceStatus, DeviceStatusEditBindModel, DeviceStatusEditModel, DeviceStatus, DeviceStatusModel<DeviceStatus>, DeviceStatusModel<DeviceStatusViewModel>>
    {
        #region Unused Actions
        [NonAction]
        public override ActionResult Create()
        {
            return null;
        }
        [NonAction]
        public override bool DeleteConfirmed(object id)
        {
            return false;
        }
        [NonAction]
        public override bool DeleteConfirmed(object[] ids)
        {
            return false;
        }
        [NonAction]
        public override bool DeactiveConfirmed(object id)
        {
            return false;
        }
        [NonAction]
        public override bool DeactiveGroupConfirmed(object[] ids)
        {
            return false;
        }
        [NonAction]
        public override ActionResult Import(FormCollection fc)
        {
            return null;
        }
        #endregion

        public override void FuncPreDetailsView(object id, ref List<DeviceStatusDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeviceStatusId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new DeviceStatusModel<DeviceStatusDetailsViewModel>().Get(requestBody);
        }
        public override void FuncPreInitEditView(object id, ref DeviceStatus EditItem, ref DeviceStatusEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new DeviceStatusModel<DeviceStatus>().Get(id);
            }
            if (EditItem != null)
            {
                model = new DeviceStatusEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref DeviceStatusEditBindModel EditItem)
        {
            id = EditItem.DeviceStatusId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "DeviceStatus.xlsx";
            string properties = "DeviceStatusId,DeviceStatus_en,DeviceStatus_ar,IsBlock";
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }
    }
}