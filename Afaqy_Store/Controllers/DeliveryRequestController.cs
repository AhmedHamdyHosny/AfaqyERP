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
    public class DeliveryRequestController : BaseController<DeliveryRequest,DeliveryRequestViewModel,DeliveryRequestIndexViewModel,DeliveryRequestDetailsViewModel,DeliveryRequestCreateBindModel,DeliveryRequestEditBindModel,DeliveryRequestEditModel,DeliveryRequest,DeliveryRequestModel<DeliveryRequest>,DeliveryRequestModel<DeliveryRequestViewModel>>
    {
        public override void FuncPreIndexView(ref List<DeliveryRequestIndexViewModel> model)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<ItemFamily> itemFamilies = new ItemFamilyModel<ItemFamily>().GetAsDDLst("ItemFamilyId,ItemFamilyName_en", "ItemFamilyId", filters);
            ViewBag.ItemFamilies = itemFamilies.Select(x => new SelectListItem() { Text = x.ItemFamilyName_en, Value = x.ItemFamilyId.ToString() }).ToList();

            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            var sorts = new List<GenericDataFormat.SortItems>();
            sorts.Add(new GenericDataFormat.SortItems() { Property = "DeviceModelTypeName" });
            var requestBody = new GenericApiController.Utilities.GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { Properties = "DeviceModelTypeId,DeviceModelTypeName,ItemFamilyId" }, Sorts = sorts };
            List<DeviceModelType> modelTypes = new DeviceModelTypeModel<DeviceModelType>().Get(requestBody);
            ViewBag.ModelTypes = modelTypes.Select(x => new SelectListItem() { Text = x.DeviceModelTypeName, Value = x.DeviceModelTypeId.ToString(), Group = new SelectListGroup() { Name = x.ItemFamilyId.ToString() } }).ToList();
            
            base.FuncPreIndexView(ref model);
        }
        public override void FuncPreDetailsView(object id, ref List<DeliveryRequestDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new DeliveryRequestModel<DeliveryRequestDetailsViewModel>().Get(requestBody);
        }
        public override void FuncPreInitCreateView()
        {
            //prepare dropdown list for item references
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<Customer> customers = new CustomerModel<Customer>().GetAsDDLst("CustomerId,CustomerName_en", "CustomerName_en", filters);
            ViewBag.CustomerId = customers.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.CustomerName_en, Value = x.CustomerId.ToString() });

            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<SaleTransactionType> SaleTransactionTypes = new SaleTransactionTypeModel<SaleTransactionType>().GetAsDDLst("SaleTransactionTypeId,SaleTransactionType_en", "SaleTransactionType_en", filters);
            ViewBag.SaleTransactionTypeId = SaleTransactionTypes.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.SaleTransactionType_en, Value = x.SaleTransactionTypeId.ToString() });

           
        }
        
        public override void FuncPreCreate(ref DeliveryRequestCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
            model.DeliveryRequestStatusId = (int)Classes.Common.DBEnums.DeliveryRequestStatus.New;
            DateTime deliveryDateTime = (DateTime)Classes.Utilities.Utility.ParseDateTime(model.DeliveryDate_Str + " " + model.DeliveryTime_Str);
            model.DeliveryDateTime = deliveryDateTime;
            //for test
            model.POSId = 1;
            model.DeliveryRequestDetails = model.DeliveryRequestDetails.Select(x => { x.CreateUserId = User.UserId; x.CreateDate = DateTime.Now; return x; }).ToList();

            var lst = model.DeliveryRequestDetails.ToList();
            var count = model.DeliveryRequestDetails.Count();
        }
        public override void FuncPreInitEditView(object id, ref DeliveryRequest EditItem, ref DeliveryRequestEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new DeliveryRequestModel<DeliveryRequest>().Get(id);
            }
            if (EditItem != null)
            {
                model = new DeliveryRequestEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                List<Customer> customers = new CustomerModel<Customer>().GetAsDDLst("CustomerId,CustomerName_en", "CustomerName_en");
                model.Customer = customers.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.CustomerName_en, Value = x.CustomerId.ToString(), Selected = (selectedItem.CustomerId == x.CustomerId) });
            }
        }
        public override void FuncPreEdit(ref object id, ref DeliveryRequestEditBindModel EditItem)
        {
            id = EditItem.DeliveryRequestId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "DeliveryRequests.xlsx";
            string properties = "SystemId,SystemName,IsBlock";
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }
    }
}