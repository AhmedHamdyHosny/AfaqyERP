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
    public class CustomerController : BaseController<Customer,CustomerViewModel,CustomerIndexViewModel,CustomerDetailsViewModel, CustomerCreateBindModel, CustomerEditBindModel, CustomerEditModel, CustomerImportModel, CustomerModel<Customer>, CustomerModel<CustomerViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<CustomerDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "CustomerId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters};
            items = new CustomerModel<CustomerDetailsViewModel>().Get(requestBody);
        }
        public override void FuncPreInitCreateView()
        {
            //prepare dropdown list for item references
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<Branch> branches = new BranchModel<Branch>().GetAsDDLst("BranchId,BranchName_en", "BranchName_en", filters);
            ViewBag.BranchId = branches.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.BranchName_en, Value = x.BranchId.ToString() });
        }
        public override void FuncPreCreate(ref CustomerCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref Customer EditItem, ref CustomerEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new CustomerModel<Customer>().Get(id);
            }
            if (EditItem != null)
            {
                model = new CustomerEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                List<Branch> branches = new CustomerModel<Branch>().GetAsDDLst("BranchId,BranchName_en", "BranchName_en");
                model.Branch = branches.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.BranchName_en, Value = x.BranchId.ToString(), Selected = (selectedItem.BranchId == x.BranchId) });
            }
        }
        public override void FuncPreEdit(ref object id, ref CustomerEditBindModel EditItem)
        {
            id = EditItem.CustomerId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override ActionResult Import(FormCollection fc)
        {
            return base.ImportAsEntities(fc);
        }
        public override void FuncPreImportAsEntities(ref List<CustomerImportModel> items, FormCollection formCollection)
        {
            //for test
            //int branchId = GetBranch;
            items = items.Select(x =>
            {
                x.CreateUserId = User.UserId;
                x.CreateDate = DateTime.Now;
                //for test
                //x.BranchId = branchId;
                return x;
            }).ToList();
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "Customers.xlsx";
            string properties = string.Join(",", typeof(CustomerView).GetProperties().Select(x => x.Name).Where(x => !x.Contains("_ar")));
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }
    }
}