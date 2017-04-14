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
    public class CustomerContactController : BaseController<rpauxname, rpauxname, rpauxname, rpauxname, rpauxname, rpauxname, rpauxname, rpauxname, CustomerContactModel<rpauxname>, CustomerContactModel<rpauxname>>
    {
        //BaseController<CustomerContact, CustomerContactViewModel, CustomerContactIndexViewModel, CustomerContactDetailsViewModel, CustomerContactCreateBindModel, CustomerContactEditBindModel, CustomerContactEditModel, CustomerContact, CustomerContactModel<CustomerContact>, CustomerContactModel<CustomerContactViewModel>>

        //public override void FuncPreDetailsView(object id, ref List<CustomerContactDetailsViewModel> items)
        //{
        //    filters = new List<GenericDataFormat.FilterItems>();
        //    filters.Add(new GenericDataFormat.FilterItems() { Property = "CustomerContactId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
        //    var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "CustomerContactDetails" } };
        //    items = new CustomerContactModel<CustomerContactDetailsViewModel>().Get(requestBody);
        //}
        //public override void FuncPreInitCreateView()
        //{

        //}
        //public override void FuncPreCreate(ref CustomerContactCreateBindModel model)
        //{
        //    model.CreateUserId = User.UserId;
        //    model.CreateDate = DateTime.Now;
        //}
        //public override void FuncPreInitEditView(object id, ref CustomerContact EditItem, ref CustomerContactEditModel model)
        //{
        //    if (EditItem == null)
        //    {
        //        //get the item by id
        //        EditItem = new CustomerContactModel<CustomerContact>().Get(id);
        //    }
        //    if (EditItem != null)
        //    {
        //        model = new CustomerContactEditModel();
        //        model.EditItem = EditItem;            }
        //}
        //public override void FuncPreEdit(ref object id, ref CustomerContactEditBindModel EditItem)
        //{
        //    id = EditItem.CustomerContactId;
        //    EditItem.ModifyUserId = User.UserId;
        //    EditItem.ModifyDate = DateTime.Now;
        //}
        //public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        //{
        //    ExportFileName = "CustomerContact.xlsx";
        //    string properties = "CustomerContactId,DolphinId,CustomerId,ContactName_en,ContactName_ar,Position_en,Position_ar,IsDefault,IsBlock";
        //    ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        //}
    }
}