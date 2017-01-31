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
    public class TechniqueCompanyController : BaseController<TechniqueCompany, TechniqueCompanyViewModel, TechniqueCompanyCreateBindModel, TechniqueCompanyEditBindModel, TechniqueCompanyEditModel, TechniqueCompanyModel<TechniqueCompany>, TechniqueCompanyModel<TechniqueCompanyViewModel>>
    {
        public override void FuncPreIndexView(ref List<TechniqueCompanyViewModel> model)
        {
            model = new TechniqueCompanyModel<TechniqueCompanyViewModel>().Get();
        }
        public override void FuncPreDetailsView(object id, ref List<TechniqueCompanyViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "CompanyId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters};
            items = new TechniqueCompanyModel<TechniqueCompanyViewModel>().Get(requestBody);
        }
        public override void FuncPreCreate(ref TechniqueCompanyCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref TechniqueCompany EditItem, ref TechniqueCompanyEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new TechniqueCompanyModel<TechniqueCompany>().Get(id);
            }
            if (EditItem != null)
            {
                model = new TechniqueCompanyEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref TechniqueCompanyEditBindModel EditItem)
        {
            id = EditItem.CompanyId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "TechniqueCompanies.xlsx";
            //filters
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = "CompanyId,CompanyName"} };
        }
    }
}