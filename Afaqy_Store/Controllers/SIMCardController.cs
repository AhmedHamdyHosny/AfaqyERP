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
        public override void FuncPreIndexView(ref List<SIMCardViewModel> model)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "SIMCardStatus" } };
            model = new SIMCardModel<SIMCardViewModel>().Get(requestBody);
        }
        public override void FuncPreDetailsView(object id, ref List<SIMCardViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "SIMCardId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "SIMCardStatus" } };
            items = new SIMCardModel<SIMCardViewModel>().Get(requestBody);
        }
        public override void FuncPreInitCreateView()
        {
            //prepare dropdown list for item references
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<SIMCardContract> simCardContracts = new SIMCardContractModel<SIMCardContract>().GetAsDDLst("SIMCardContractId,ContractNo", "ContractNo",filters);
            ViewBag.ContractId = simCardContracts.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.ContractNo, Value = x.SIMCardContractId.ToString() });
        }
        public override void FuncPreInitEditView(object id, ref SIMCard EditItem, ref SIMCardEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new SIMCardModel<SIMCard>().Get(id);
            }
            if (EditItem != null)
            {
                model = new SIMCardEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                List<SIMCardContract> simCardContracts = new SIMCardContractModel<SIMCardContract>().GetAsDDLst("SIMCardContractId,ContractNo", "ContractNo", filters);
                model.Contract = simCardContracts.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.ContractNo, Value = x.SIMCardContractId.ToString(), Selected = (selectedItem.ContractId == x.SIMCardContractId) });
            }
        }
        public override void FuncPreEdit(ref object id, ref SIMCardEditBindModel EditItem)
        {
            id = EditItem.SIMCardId;
            EditItem.CreateUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "SIMCards.xlsx";
            //filters
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            ExportRequestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { Properties = "SIMCardId,SerialNumber,GSM,SIMCardStatus.SIMCardStatusName_en", References = "SIMCardStatus" } };
        }
    }
}
