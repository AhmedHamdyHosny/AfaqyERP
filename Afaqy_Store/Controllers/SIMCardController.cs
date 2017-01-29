using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Afaqy_Store.DataLayer;
using Afaqy_Store.Models;
using GenericApiController.Utilities;
using static Classes.Common.Enums;
using Classes.Common;

namespace Afaqy_Store.Controllers
{
    public class SIMCardController : BaseController<SIMCard, SIMCardViewModel, SIMCardCreateBindModel, SIMCardEditBindModel, SIMCardEditModel, SIMCardModel<SIMCard>, SIMCardModel<SIMCardViewModel>>
    {
        public SIMCardController()
        {

            PK_PropertyName = "SIMCardId";
            List<GenericDataFormat.FilterItems> filters = null;
            ActionItemsPropertyValue = new List<ActionItemPropertyValue>();
            var userId = User.UserId;

            #region Index
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsDeleted", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            IndexRequestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "SIMCardStatus" } };
            #endregion

            #region Details
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "SIMCardId", Operation = GenericDataFormat.FilterOperations.Equal });
            DetailsRequestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "SIMCardStatus" } };
            #endregion

            #region Create
            //Create view Dropdown Lists
            CreateReferences = new List<Reference>();
            List<GenericDataFormat.FilterItems> contractRefernceFilters = new List<GenericDataFormat.FilterItems>();
            contractRefernceFilters.Add(new GenericDataFormat.FilterItems() { Property = "IsDeleted", Value = false, Operation = GenericDataFormat.FilterOperations.Equal });
            CreateReferences.Add(new Reference() { TypeModel = typeof(SIMCardContractModel<SIMCardContract>),Filters = contractRefernceFilters, ViewDataName = "ContractId", DataValueField = "SIMCardContractId", DataTextField = "ContractNo", SelectColumns = "SIMCardContractId,ContractNo" });

            //on create dependences
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Create, PropertyName = "CreateUserId", Value = userId });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Create, PropertyName = "CreateDate", Value = DateTime.Now });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Create, PropertyName = "SIMCardStatusId", Value = (int)DBEnums.SIMCardStatus.New });
            var simCardStatusHistory = new List<SIMCardStatusHistory>();
            simCardStatusHistory.Add(new SIMCardStatusHistory()
            {
                SIMCardStatusId = (int)DBEnums.SIMCardStatus.New,
                CreateUserId = userId,
                CreateDate = DateTime.Now
            });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Create, PropertyName = "SIMCardStatusHistory", Value = simCardStatusHistory });
            #endregion

            #region Edit
            //edit view Dropdown Lists

            //on edit dependences
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Edit, PropertyName = "ModifyUserId", Value = userId });
            ActionItemsPropertyValue.Add(new ActionItemPropertyValue() { Transaction = Transactions.Edit, PropertyName = "ModifyDate", Value = DateTime.Now });
            #endregion

            #region Export
            ExportFileName = "SIMCards.xlsx";
            //filters
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsDeleted", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            ExportRequestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { Properties = "SIMCardId,SerialNumber,GSM,SIMCardStatus.SIMCardStatusName_en", References = "SIMCardStatus" } };
            #endregion
            
        }

        [HttpPost]
        public override ActionResult Create(SIMCardCreateBindModel[] items , FormCollection fc)
        {
            //for test
            var userId = 1;
            var branchId = 1;

            var purchaseDate = DateTime.Parse(fc["PurchaseDate"]);
            var contractId = fc["ContractId"];
            var simCardContract = new SIMCardContractModel<SIMCardContract>().Get(contractId);
            var cost = simCardContract.CurrentCost;
            var currencyId = simCardContract.CurrencyId;
            

            foreach (var item in items)
            {
                item.SIMCardStatusId = (int)DBEnums.SIMCardStatus.New;
                item.BranchId = branchId;
                item.ContractId = int.Parse(contractId);
                item.Cost = cost;
                item.CurrencyId = currencyId;
                item.PurchaseDate = purchaseDate;
                item.CreateUserId = userId;
                item.CreateDate = DateTime.Now;
                item.SIMCardStatusHistory = new List<SIMCardStatusHistory>();
                item.SIMCardStatusHistory.Add(new SIMCardStatusHistory()
                {
                    SIMCardStatusId = (int)DBEnums.SIMCardStatus.New,
                    BranchId = branchId,
                    CreateUserId = userId,
                    CreateDate = DateTime.Now
                });
            }

            new SIMCardModel<SIMCard>().Import(items);

            return base.Create(items, fc);
        }

    }
}
