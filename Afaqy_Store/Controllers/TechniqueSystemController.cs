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
    public class TechniqueSystemController : BaseController<TechniqueSystem,TechniqueSystemViewModel, TechniqueSystemViewModel, TechniqueSystemViewModel, TechniqueSystemCreateBindModel,TechniqueSystemEditBindModel,TechniqueSystemEditModel,TechniqueSystemModel<TechniqueSystem>,TechniqueSystemModel<TechniqueSystemViewModel>>
    {
        public override void FuncPreIndexView(ref List<TechniqueSystemViewModel> model)
        {
            var requestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { References = "TechniqueCompany" } };
            model = new TechniqueSystemModel<TechniqueSystemViewModel>().Get(requestBody);
        }
        public override void FuncPreDetailsView(object id, ref List<TechniqueSystemViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "SystemId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "TechniqueCompany" } };
            items = new TechniqueSystemModel<TechniqueSystemViewModel>().Get(requestBody);
        }
        public override void FuncPreInitCreateView()
        {
            //prepare dropdown list for item references
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false});
            List<TechniqueCompany> techniqueCompanies = new TechniqueCompanyModel<TechniqueCompany>().GetAsDDLst("CompanyId,CompanyName", "CompanyName",filters);
            ViewBag.CompanyId = techniqueCompanies.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.CompanyName, Value = x.CompanyId.ToString() });
        }
        public override void FuncPreCreate(ref TechniqueSystemCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref TechniqueSystem EditItem, ref TechniqueSystemEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new TechniqueSystemModel<TechniqueSystem>().Get(id);
            }
            if (EditItem != null)
            {
                model = new TechniqueSystemEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                List<TechniqueCompany> techniqueCompanies = new TechniqueCompanyModel<TechniqueCompany>().GetAsDDLst("CompanyId,CompanyName", "CompanyName");
                model.TechniqueCompany = techniqueCompanies.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.CompanyName, Value = x.CompanyId.ToString(), Selected = (selectedItem.CompanyId == x.CompanyId) });
            }
        }
        public override void FuncPreEdit(ref object id, ref TechniqueSystemEditBindModel EditItem)
        {
            id = EditItem.SystemId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "TechniqueSystems.xlsx";
            //filters
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = "SystemId,SystemName", References = "TechniqueCompany" } };
        }
    }
}