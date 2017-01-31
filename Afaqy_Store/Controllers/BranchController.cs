using Afaqy_Store.DataLayer;
using Afaqy_Store.Models;
using Classes.Common;
using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Classes.Common.Enums;

namespace Afaqy_Store.Controllers
{
    public class BranchController : BaseController<Branch,BranchViewModel,BranchCreateBindModel,BranchEditBindModel,BranchEditModel,BranchModel<Branch>,BranchModel<BranchViewModel>>
    {
        public override bool DeactiveGroupConfirmed(object[] ids)
        {
            var instance = new BranchModel<Branch>();
            instance.Deactive(ids);
            TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = ids.Count(), Transaction = Transactions.Deactive };
            return true;
        }

        public override void FuncPreIndexView(ref List<BranchViewModel> model)
        {
            var requestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { References = "Country" } };
            model = new BranchModel<BranchViewModel>().Get(requestBody);
        }
        public override void FuncPreDetailsView(object id, ref List<BranchViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "BranchId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "Country" } };
            items = new BranchModel<BranchViewModel>().Get(requestBody);
        }
        public override void FuncPreInitCreateView()
        {
            //prepare dropdown list for item references
            List<Country> lst = new CountryModel<Country>().GetAsDDLst("CountryId,CountryName_en", "CountryName_en");
            ViewBag.ModelTypeId = lst.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.CountryName_en, Value = x.CountryId.ToString() });
        }
        public override void FuncPreCreate(ref BranchCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref Branch EditItem, ref BranchEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new BranchModel<Branch>().Get(id);
            }

            if (EditItem != null)
            {
                model = new BranchEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                List<Country> lst = new CountryModel<Country>().GetAsDDLst("CountryId,CountryName_en", "CountryName_en");
                model.Country = lst.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.CountryName_en, Value = x.CountryId.ToString(), Selected = (selectedItem.CountryId == x.CountryId) });
            }
        }
        public override void FuncPreEdit(ref object id, ref BranchEditBindModel EditItem)
        {
            id = EditItem.BranchId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "Branches.xlsx";
            //filters
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "Active", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = "BranchId,BranchName_en,BranchName_ar", References = "Coutry" } };
        }
    }
}