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
    public class BrandController : BaseController<Brand,BrandViewModel, BrandViewModel, BrandViewModel, BrandCreateBindModel,BrandEditBindModel,BrandEditModel,BrandModel<Brand>,BrandModel<BrandViewModel>>
    {
        public override void FuncPreIndexView(ref List<BrandViewModel> model)
        {
            model = new BrandModel<BrandViewModel>().Get();
        }
        public override void FuncPreDetailsView(object id, ref List<BrandViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "BrandId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new BrandModel<BrandViewModel>().Get(requestBody);
        }
        public override void FuncPreCreate(ref BrandCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref Brand EditItem, ref BrandEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new BrandModel<Brand>().Get(id);
            }
            if (EditItem != null)
            {
                model = new BrandEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref BrandEditBindModel EditItem)
        {
            id = EditItem.BrandId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "Brands.xlsx";
            //filters
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = "BrandId,BrandName,ProtocolName" } };
        }
    }
}