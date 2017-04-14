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
    public class DeviceModelTypeController : BaseController<DeviceModelType, DeviceModelTypeViewModel, DeviceModelTypeIndexViewModel, DeviceModelTypeDetailsViewModel, DeviceModelTypeCreateBindModel, DeviceModelTypeEditBindModel, DeviceModelTypeEditModel, DeviceModelType, ModelTypeModel<DeviceModelType>, ModelTypeModel<DeviceModelTypeViewModel>>
    {
        #region Unused Actions
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

        public override void FuncPreDetailsView(object id, ref List<DeviceModelTypeDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeviceModelTypeId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new ModelTypeModel<DeviceModelTypeDetailsViewModel>().Get(requestBody);
        }

        public override void FuncPreCreate(ref DeviceModelTypeCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }

        public override void FuncPreInitEditView(object id, ref DeviceModelType EditItem, ref DeviceModelTypeEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new ModelTypeModel<DeviceModelType>().Get(id);
            }
            if (EditItem != null)
            {
                model = new DeviceModelTypeEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref DeviceModelTypeEditBindModel EditItem)
        {
            id = EditItem.DeviceModelTypeId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "DeviceModelType.xlsx";
            string properties = "DeviceModelTypeId,DeviceModelTypeName,IsBlock";
            ExportRequestBody = new GenericDataFormat() {Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }
    }
}