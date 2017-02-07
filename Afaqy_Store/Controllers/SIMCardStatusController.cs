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
    public class SIMCardStatusController : BaseController<SIMCardStatus, SIMCardStatus, SIMCardStatus, SIMCardStatus, SIMCardStatus, SIMCardStatusEditBindModel, SIMCardStatusEditModel , SIMCardStatusModel<SIMCardStatus>, SIMCardStatusModel<SIMCardStatus>>
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
        public override void FuncPreDetailsView(object id, ref List<SIMCardStatus> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "SIMCardStatusId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new SIMCardProviderModel<SIMCardStatus>().Get(requestBody);
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "SIMCardStatus.xlsx";
        }
    }

    
}