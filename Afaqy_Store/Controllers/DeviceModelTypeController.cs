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
    public class DeviceModelTypeController : BaseController<DeviceModelType, DeviceModelType, DeviceModelType, DeviceModelType, DeviceModelTypeCreateBindModel, DeviceModelTypeEditBindModel, DeviceModelTypeEditModel, DeviceModelTypeModel<DeviceModelType>, DeviceModelTypeModel<DeviceModelType>>
    {
        public DeviceModelTypeController()
        {
            #region Details
            
            #endregion
        }

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

        public override void FuncPreDetailsView(object id, ref List<DeviceModelType> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "ModelTypeId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new DeviceModelTypeModel<DeviceModelType>().Get(requestBody);
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "DeviceModelType.xlsx";
        }
    }
}