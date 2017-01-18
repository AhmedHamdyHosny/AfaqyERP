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
    public class DeviceModelTypeController : BaseController<DeviceModelType, DeviceModelType, DeviceModelTypeCreateBindModel, DeviceModelTypeEditBindModel, DeviceModelTypeEditModel, DeviceModelTypeModel<DeviceModelType>, DeviceModelTypeModel<DeviceModelType>>
    {
        public DeviceModelTypeController()
        {
            PK_PropertyName = "ModelTypeId";
            List<GenericDataFormat.FilterItems> filters = null;

            #region Index

            #endregion

            #region Details
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "ModelTypeId", Operation = GenericDataFormat.FilterOperations.Equal });
            DetailsRequestBody = new GenericDataFormat() { Filters = filters };
            #endregion

            #region Create
            
            #endregion

            #region Edit
            //edit view Dropdown Lists

            //on edit dependences

            #endregion

            #region Export
            ExportFileName = "DeviceModelType.xlsx";
            //filters

            #endregion

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
        public override bool HideConfirmed(object id)
        {
            return false;
        }

        [NonAction]
        public override bool HideConfirmed(object[] ids)
        {
            return false;
        }

        [NonAction]
        public override ActionResult Import(FormCollection fc)
        {
            return null;
        }
    }
}