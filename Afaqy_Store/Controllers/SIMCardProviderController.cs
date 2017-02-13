using Afaqy_Store.DataLayer;
using Afaqy_Store.Models;
using Classes.Common;
using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Controllers
{
    public class SIMCardProviderController : BaseController<SIMCardProvider,SIMCardProviderViewModel, SIMCardProviderIndexViewModel, SIMCardProviderDetailsViewModel,
        SIMCardProviderCreateBindModel, SIMCardProviderEditBindModel,
        SIMCardProviderEditModel,
        SIMCardProviderModel<SIMCardProvider>,SIMCardProviderModel<SIMCardProviderViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<SIMCardProviderDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "ProviderId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new SIMCardProviderModel<SIMCardProviderDetailsViewModel>().Get(requestBody);
        }
        public override void FuncPreCreate(ref SIMCardProviderCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref SIMCardProvider EditItem, ref SIMCardProviderEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new SIMCardProviderModel<SIMCardProvider>().Get(id);
            }
            if (EditItem != null)
            {
                model = new SIMCardProviderEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref SIMCardProviderEditBindModel EditItem)
        {
            id = EditItem.ProviderId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "SIMCardProviders.xlsx";
            string properties = "ProviderId,ProviderName_en,ProviderName_ar,IsBlock";
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }
    }
}