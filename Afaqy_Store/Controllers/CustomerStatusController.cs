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
    public class CustomerStatusController : BaseController<CustomerStatus,CustomerStatusViewModel,CustomerStatusIndexViewModel,CustomerStatusDetailsViewModel,CustomerStatusCreateBindModel,CustomerStatusEditBindModel,CustomerStatusEditModel,CustomerStatusModel<CustomerStatus>,CustomerStatusModel<CustomerStatusViewModel>>
    {
        public override void FuncPreIndexView(ref List<CustomerStatusIndexViewModel> model)
        {
            model = new CustomerStatusModel<CustomerStatusIndexViewModel>().Get();
        }
        public override void FuncPreDetailsView(object id, ref List<CustomerStatusDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "CustomerStatusId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters};
            items = new CustomerStatusModel<CustomerStatusDetailsViewModel>().Get(requestBody);
        }
        public override void FuncPreCreate(ref CustomerStatusCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref CustomerStatus EditItem, ref CustomerStatusEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new CustomerStatusModel<CustomerStatus>().Get(id);
            }
            if (EditItem != null)
            {
                model = new CustomerStatusEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref CustomerStatusEditBindModel EditItem)
        {
            id = EditItem.CustomerStatusId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "CustomerStatus.xlsx";
            //filters
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = "CustomerStatusId,CustomerStatus_en,CustomerStatus_ar"} };
        }
    }
}