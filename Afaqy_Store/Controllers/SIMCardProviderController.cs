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
        
        public SIMCardProviderController()
        {
            PK_PropertyName = "ProviderId";
            List<GenericDataFormat.FilterItems> filters = null;
            ActionItemsPropertyValue = new List<ActionItemPropertyValue>();
            var user = new UserViewModel().GetUserFromSession();
            var userId = user.UserId;

            #region Index
            
            #endregion

            #region Details
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "ProviderId", Operation = GenericDataFormat.FilterOperations.Equal });
            DetailsRequestBody = new GenericDataFormat() { Filters = filters};
            #endregion

            #region Create
            
            #endregion

            #region Edit
           
            #endregion

            #region Export
            ExportFileName = "SIMCardProviders.xlsx";
            #endregion
        }
    }
}