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
    public class SIMCardContractController : BaseController<SIMCardContract,SIMCardContractViewModel,SIMCardContractCreateBindModel,SIMCardContractEditBindModel,SIMCardContractEditModel,SIMCardContractModel<SIMCardContract>,SIMCardContractModel<SIMCardContractViewModel>>
    {
        public SIMCardContractController()
        {
            PK_PropertyName = "SIMCardContractId";
            List<GenericDataFormat.FilterItems> filters = null;
            ActionItemsPropertyValue = new List<ActionItemPropertyValue>();
            var userId = User.UserId;

            #region Index
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsDeleted", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            IndexRequestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "SIMCardProvider,Currency" } };
            #endregion

            #region Details
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "SIMCardContractId", Operation = GenericDataFormat.FilterOperations.Equal });
            DetailsRequestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "SIMCardProvider,Currency" } };
            #endregion

            #region Create
            //Create view Dropdown Lists
            CreateReferences = new List<Reference>();
            CreateReferences.Add(new Reference() { TypeModel = typeof(SIMCardProviderModel<SIMCardProvider>), ViewDataName = "SIMCardProviderId", DataValueField = "ProviderId", DataTextField = "ProviderName_en", SelectColumns = "ProviderId,ProviderName_en,ProviderName_ar" });
            CreateReferences.Add(new Reference() { TypeModel = typeof(CurrencyModel<Currency>), ViewDataName = "CurrencyId", DataValueField = "CurrencyId", DataTextField = "CurrencyName_en", SelectColumns = "CurrencyId,CurrencyName_en,CurrencyName_ar" });

            //on create dependences
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Create, PropertyName = "CreateUserId", Value = userId });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Create, PropertyName = "CreateDate", Value = DateTime.Now });
            
            #endregion

            #region Edit
            //edit view Dropdown Lists
            EditReferences = new List<Reference>();
            EditReferences.Add(new Reference() { TypeModel = typeof(SIMCardProviderModel<SIMCardProvider>), PropertyName = "SIMCardProvider", ViewDataName = "SIMCardProviderId", DataValueField = "ProviderId", DataTextField = "ProviderName_en", SelectColumns = "ProviderId,ProviderName_en,ProviderName_ar" });
            EditReferences.Add(new Reference() { TypeModel = typeof(CurrencyModel<Currency>), PropertyName= "Currency", ViewDataName = "CurrencyId", DataValueField = "CurrencyId", DataTextField = "CurrencyName_en", SelectColumns = "CurrencyId,CurrencyName_en,CurrencyName_ar" });

            //on edit dependences
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Edit, PropertyName = "ModifyUserId", Value = userId });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Edit, PropertyName = "ModifyDate", Value = DateTime.Now });
            #endregion

            #region Export
            ExportFileName = "SIMCardContracts.xlsx";
            //filters
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsDeleted", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = "SIMCardContractId,CurrentCost,ContractDate,ExpiryDate", References = "SIMCardProvider,Currency" } };
            #endregion
        }

    }
}