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
    public class CustomerServerStatusController : BaseController<CustomerServerStatus, CustomerServerStatusViewModel, CustomerServerStatusIndexViewModel, CustomerServerStatusDetailsViewModel, CustomerServerStatusCreateBindModel, CustomerServerStatusEditBindModel, CustomerServerStatusEditModel, CustomerServerStatus, CustomerServerStatusModel<CustomerServerStatus>, CustomerServerStatusModel<CustomerServerStatusViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<CustomerServerStatusDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "CustomerServerStatusId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new CustomerServerStatusModel<CustomerServerStatusDetailsViewModel>().Get(requestBody);
        }
        public override void FuncPreCreate(ref CustomerServerStatusCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref CustomerServerStatus EditItem, ref CustomerServerStatusEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new CustomerServerStatusModel<CustomerServerStatus>().Get(id);
            }
            if (EditItem != null)
            {
                model = new CustomerServerStatusEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref CustomerServerStatusEditBindModel EditItem)
        {
            id = EditItem.CustomerServerStatusId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "CustomerServerStatus.xlsx";
            string properties = "CustomerServerStatusId,CustomerServerStatus_en,CustomerServerStatus_ar,IsBlock";
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }
    }
}