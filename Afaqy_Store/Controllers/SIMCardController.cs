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
    public class SIMCardController : BaseController<SIMCard, SIMCardViewModel, SIMCardIndexViewModel, SIMCardDetailsViewModel, SIMCardCreateBindModel, SIMCardEditBindModel, SIMCardEditModel, SIMCardModel<SIMCard>, SIMCardModel<SIMCardViewModel>>
    {
        
        public override void FuncPreIndexView(ref List<SIMCardIndexViewModel> model)
        {
            //filters = new List<GenericDataFormat.FilterItems>();
            //filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            var requestBody = new GenericDataFormat() {Includes = new GenericDataFormat.IncludeItems() { References = "SIMCardStatus" }  }; //, Paging = new GenericDataFormat.PagingItem() { PageNumber = 1, PageSize=10}
            model = new SIMCardModel<SIMCardIndexViewModel>().Get(requestBody);
        }
        public override void FuncPreDetailsView(object id, ref List<SIMCardDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "SIMCardId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "SIMCardStatus,SIMCardContract,Branch" } };
            items = new SIMCardModel<SIMCardDetailsViewModel>().Get(requestBody);
        }
        public override void FuncPreInitCreateView()
        {
            //prepare dropdown list for item references
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<SIMCardContract> simCardContracts = new SIMCardContractModel<SIMCardContract>().GetAsDDLst("SIMCardContractId,ContractNo", "ContractNo",filters);
            ViewBag.ContractId = simCardContracts.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.ContractNo, Value = x.SIMCardContractId.ToString() });
        }
        public override void FuncPreCreate(ref SIMCardCreateBindModel[] items, FormCollection formCollection)
        {
            var userId = User.UserId;
            //for test
            var branchId = 1;

            var purchaseDate = DateTime.Parse(formCollection["PurchaseDate"]);
            var contractId = formCollection["ContractId"];
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
            var simCardContract = new SIMCardContractModel<SIMCardContract>().Get(EditItem.ContractId);
            var cost = simCardContract.CurrentCost;
            var currencyId = simCardContract.CurrencyId;
            EditItem.Cost = cost;
            EditItem.CurrencyId = currencyId;
            EditItem.ModifyUserId = User.UserId;
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

        //for test
        public override ActionResult Import(FormCollection fc)
        {
            var file = Request.Files["file"];
            var instance = new SIMCardModel<SIMCard>();

            //var result = instance.Import(file);
            string error = "";
            string path = Server.MapPath(Classes.Utilities.SiteConfig.ImportFilesPath); //"D:\\Afaqy\\Files\\";
            var fileName = DataImportHelper(file, path,"SIMCards");
            var lst = ParseExcelFile(fileName, ref error);
            var result = instance.Import(lst.ToArray());
            if (result)
            {
                TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = AlertMessageType.Success, Transaction = Transactions.Import };
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = AlertMessageType.Error, Transaction = Transactions.Import };
            return RedirectToAction("Index");
        }

        public static string DataImportHelper(HttpPostedFileBase file, string directoryPath, string folderPath)
        {
            string fileName_datetimePart = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_");
            if (!System.IO.File.Exists(directoryPath + "\\" + folderPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath + "\\" + folderPath);
            }
           
            string fileName = fileName_datetimePart + file.FileName;
            string tempFilePath = System.IO.Path.Combine(directoryPath + "\\" + folderPath , fileName);
            (new System.IO.FileInfo(tempFilePath)).Directory.Create();
            file.SaveAs(tempFilePath);
            return tempFilePath;
        }

        public static List<SIMCardImportModel> ParseExcelFile(string fileName, ref string errorMsg)
        {
            var excelFile = new LinqToExcel.ExcelQueryFactory(fileName);
            IEnumerable<string> workSheetNames = excelFile.GetWorksheetNames();
            List<SIMCardImportModel> sheetData = new List<SIMCardImportModel>();
            foreach (string sheetName in workSheetNames)
            {
                List<string> columns = excelFile.GetColumnNames(sheetName).ToList();
                var isEqual = true; //new HashSet<string>(columns, StringComparer.OrdinalIgnoreCase).SetEquals(student_excelHeaders);
                if (isEqual)
                {
                    errorMsg = "";
                    var data = from row in excelFile.Worksheet<SIMCardImportModel>(sheetName)
                               select row;
                    List<string> contractsNo = data.Select(x => x.ContractNo).Distinct().ToList();
                    sheetData = data.ToList();
                    sheetData = sheetData.Select(x => { x.CreateDate = DateTime.Now; x.SIMCardStatusHistory = new List<SIMCardStatusHistory>();
                        x.SIMCardStatusHistory.Add(new SIMCardStatusHistory()
                        {
                            SIMCardStatusId = (int)DBEnums.SIMCardStatus.New,
                            BranchId = x.BranchId,
                            CreateUserId = x.CreateUserId,
                            CreateDate = DateTime.Now
                        }); return x;
                    } ).ToList();
                    foreach (var contractNo in contractsNo)
                    {
                        var filters = new List<GenericDataFormat.FilterItems>();
                        filters.Add(new GenericDataFormat.FilterItems() { Property = "ContractNo", Operation = GenericDataFormat.FilterOperations.Equal, Value = contractNo });
                        var requestBody = new GenericDataFormat() { Filters = filters };
                        var contracts = new SIMCardContractModel<SIMCardContract>().Get(requestBody);
                        if(contracts != null && contracts.Count == 1)
                        {
                            var contractId = contracts[0].SIMCardContractId;
                            var cost = contracts[0].CurrentCost;
                            var currencyId = contracts[0].CurrencyId;
                            sheetData = sheetData.Select(x =>  { x.ContractId = x.ContractNo == contractNo ? contractId : x.ContractId; x.Cost = x.ContractNo == contractNo?  cost : x.Cost; x.CurrencyId = x.ContractNo == contractNo? currencyId : x.CurrencyId; return x; } ).ToList();
                        }
                    }
                }
            }

            return sheetData;
        }
    }
}
