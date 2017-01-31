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
    public class SIMCardProviderController : BaseController<SIMCardProvider,SIMCardProviderViewModel, 
        SIMCardProviderCreateBindModel, SIMCardProviderEditBindModel,
        SIMCardProviderEditModel,
        SIMCardProviderModel<SIMCardProvider>,SIMCardProviderModel<SIMCardProviderViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<SIMCardProviderViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "ProviderId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new SIMCardProviderModel<SIMCardProviderViewModel>().Get(requestBody);
        }

        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "SIMCardProviders.xlsx";
        }
    }
}