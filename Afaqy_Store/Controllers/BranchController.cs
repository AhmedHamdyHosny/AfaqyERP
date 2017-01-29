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
        public BranchController()
        {
            PK_PropertyName = "BranchId";
            List<GenericDataFormat.FilterItems> filters = null;
            ActionItemsPropertyValue = new List<ActionItemPropertyValue>();
            var userId = User.UserId;

            #region Index
            IndexRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { References = "Country" } };
            #endregion

            #region Details
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "BranchId", Operation = GenericDataFormat.FilterOperations.Equal });
            DetailsRequestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "Country" } };
            #endregion

            #region Create
            
            #endregion

            #region Edit
            //edit view Dropdown Lists
            EditReferences = new List<Reference>();
            EditReferences.Add(new Reference() { TypeModel = typeof(CountryModel<Country>), ViewDataName = "CountryId", DataValueField = "CountryId", DataTextField = "CountryName_en", SelectColumns = "CountryId,CountryName_en,CountryName_ar", PropertyName = "Country" });

            //on edit dependences
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Edit, PropertyName = "ModifyUserId", Value = userId });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Edit, PropertyName = "ModifyDate", Value = DateTime.Now });
            #endregion

            #region Export
            ExportFileName = "Branches.xlsx";
            //filters
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "Active", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = "BranchId,BranchName_en,BranchName_ar", References = "Coutry" } };
            #endregion

        }

        public override bool HideConfirmed(object[] ids)
        {
            var instance = new BranchModel<Branch>();
            instance.Hide(ids);
            TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = ids.Count(), Transaction = Transactions.Deactive };
            return true;
        }
    }
}