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
    public class SIMCardStatusController : BaseController<SIMCardStatus, SIMCardStatusViewModel, SIMCardStatusIndexViewModel, SIMCardStatusDetailsViewModel, SIMCardStatus, SIMCardStatusEditBindModel, SIMCardStatusEditModel, SIMCardStatus, SIMCardStatusModel<SIMCardStatus>, SIMCardStatusModel<SIMCardStatusViewModel>>
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
        public override void FuncPreDetailsView(object id, ref List<SIMCardStatusDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "SIMCardStatusId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new SIMCardStatusModel<SIMCardStatusDetailsViewModel>().Get(requestBody);
        }
        public override void FuncPreInitEditView(object id, ref SIMCardStatus EditItem, ref SIMCardStatusEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new SIMCardStatusModel<SIMCardStatus>().Get(id);
            }
            if (EditItem != null)
            {
                model = new SIMCardStatusEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref SIMCardStatusEditBindModel EditItem)
        {
            id = EditItem.SIMCardStatusId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "SIMCardStatus.xlsx";
            string properties = "SIMCardStatusId,SIMCardStatusName_en,SIMCardStatusName_ar,IsBlock";
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }
    }

    
}