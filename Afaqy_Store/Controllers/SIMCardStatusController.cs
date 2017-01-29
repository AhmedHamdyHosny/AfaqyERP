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
    public class SIMCardStatusController : BaseController<SIMCardStatus, SIMCardStatus, SIMCardStatus, SIMCardStatusEditBindModel, SIMCardStatusEditModel , SIMCardStatusModel<SIMCardStatus>, SIMCardStatusModel<SIMCardStatus>>
    {
        public SIMCardStatusController()
        {
            PK_PropertyName = "SIMCardStatusId";
            List<GenericDataFormat.FilterItems> filters = null;

            #region Index
            
            #endregion

            #region Details
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "SIMCardStatusId", Operation = GenericDataFormat.FilterOperations.Equal });
            DetailsRequestBody = new GenericDataFormat() { Filters = filters};
            #endregion

            #region Create
            
            #endregion

            #region Edit
            //edit view Dropdown Lists

            //on edit dependences
            
            #endregion

            #region Export
            ExportFileName = "SIMCardStatus.xlsx";
            //filters
            
            #endregion

        }

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