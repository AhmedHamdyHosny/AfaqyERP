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
        public override void FuncPreDetailsView(object id, ref List<DeliveryRequestDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new DeliveryRequestModel<DeliveryRequestDetailsViewModel>().GetView<DeliveryRequestDetailsViewModel>(requestBody).PageItems;

            foreach (var item in items)
            {
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, Value = item.DeliveryRequestId });
                var detailsRequestBody = new GenericDataFormat() { Filters = filters };
                item.DeliveryRequestDetails = new DeliveryRequestDetailsModel<RequestDetails_DetailsViewModel>().GetView<RequestDetails_DetailsViewModel>(requestBody).PageItems;
            }

        }
        public override void FuncPreInitCreateView()
        {
            //prepare dropdown list for item references
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<SaleTransactionType> SaleTransactionTypes = new SaleTransactionTypeModel<SaleTransactionType>().GetAsDDLst("SaleTransactionTypeId,SaleTransactionType_en", "SaleTransactionTypeId", filters);
            ViewBag.SaleTransactionTypeId = SaleTransactionTypes.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.SaleTransactionType_en, Value = x.SaleTransactionTypeId.ToString(), Selected = (x.SaleTransactionTypeId == (int)Classes.Common.DBEnums.SaleTransactionType.Sales) });
            
            //get all customers
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<Customer> customers = new CustomerModel<Customer>().GetAsDDLst("CustomerId,CustomerName_en", "CustomerName_en", filters);
            ViewBag.CustomerId = customers.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.CustomerName_en, Value = x.CustomerId.ToString() });

            //get all Points of sale
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            //filter by branch 
            if(User.BranchId != null)
            {
                filters.Add(new GenericDataFormat.FilterItems() { Property = "BranchId", Operation = GenericDataFormat.FilterOperations.Equal, Value = User.BranchId });
            }
            List<PointOfSale> pos = new PointOfSaleModel<PointOfSale>().GetAsDDLst("POSId,POSName_en", "POSId", filters);
            ViewBag.POSId = pos.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.POSName_en, Value = x.POSId.ToString() });

            //get all Warehouse
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            //filter by branch 
            if (User.BranchId != null)
            {
                filters.Add(new GenericDataFormat.FilterItems() { Property = "BranchId", Operation = GenericDataFormat.FilterOperations.Equal, Value = User.BranchId });
            }
            List<Warehouse> warehouse = new WarehouseModel<Warehouse>().GetAsDDLst("WarehouseId,WarehouseName_en", "WarehouseId", filters);
            ViewBag.WarehouseId = warehouse.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.WarehouseName_en, Value = x.WarehouseId.ToString() });


            //get all technique systems
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<TechniqueSystem> systems = new TechniqueSystemModel<TechniqueSystem>().GetAsDDLst("SystemId,SystemName", "SystemName", filters);
            ViewBag.SystemId = systems.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.SystemName, Value = x.SystemId.ToString() });

            //get all Item Families
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<ItemFamily> itemFamilies = new ItemFamilyModel<ItemFamily>().GetAsDDLst("ItemFamilyId,ItemFamilyName_en", "ItemFamilyId", filters);
            ViewBag.ItemFamilies = itemFamilies.Select(x => new SelectListItem() { Text = x.ItemFamilyName_en, Value = x.ItemFamilyId.ToString() }).ToList();

            //get all model types
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            var sorts = new List<GenericDataFormat.SortItems>();
            sorts.Add(new GenericDataFormat.SortItems() { Property = "DeviceModelTypeName" });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { Properties = "DeviceModelTypeId,DeviceModelTypeName,ItemFamilyId" }, Sorts = sorts };
            List<DeviceModelType> modelTypes = new DeviceModelTypeModel<DeviceModelType>().Get(requestBody);
            ViewBag.ModelTypes = modelTypes.Select(x => new SelectListItem() { Text = x.DeviceModelTypeName, Value = x.DeviceModelTypeId.ToString(), Group = new SelectListGroup() { Name = x.ItemFamilyId.ToString() } }).ToList();
        }
        public override void FuncPreCreate(ref DeliveryRequestCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
            model.DeliveryRequestStatusId = (int)Classes.Common.DBEnums.DeliveryRequestStatus.New;
            DateTime deliveryDateTime = (DateTime)Classes.Utilities.Utility.ParseDateTime(model.DeliveryRequestDate_Str + " " + model.DeliveryRequestTime_Str);
            model.DeliveryRequestDateTime = deliveryDateTime;
            model.DeliveryRequestDetails = model.DeliveryRequestDetails.Select(x => { x.CreateUserId = User.UserId; x.CreateDate = DateTime.Now; return x; }).ToList();

            var lst = model.DeliveryRequestDetails.ToList();
            var count = model.DeliveryRequestDetails.Count();
        }
        public override void FuncPreInitEditView(object id, ref DeliveryRequest EditItem, ref DeliveryRequestEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = id });
                var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeliveryRequestDetails,DeliveryRequestDetails.DeviceModelType" } };
                EditItem = new DeliveryRequestModel<DeliveryRequest>().Get(requestBody).SingleOrDefault();
            }
            if (EditItem != null)
            {
                model = new DeliveryRequestEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                //prepare dropdown list for item references
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
                List<SaleTransactionType> SaleTransactionTypes = new SaleTransactionTypeModel<SaleTransactionType>().GetAsDDLst("SaleTransactionTypeId,SaleTransactionType_en", "SaleTransactionTypeId", filters);
                model.SaleTransactionType = SaleTransactionTypes.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.SaleTransactionType_en, Value = x.SaleTransactionTypeId.ToString(), Selected = (x.SaleTransactionTypeId == selectedItem.SaleTransactionTypeId) });

                //get all customers
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
                List<Customer> customers = new CustomerModel<Customer>().GetAsDDLst("CustomerId,CustomerName_en", "CustomerName_en", filters);
                model.Customer = customers.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.CustomerName_en, Value = x.CustomerId.ToString(), Selected = (x.CustomerId == selectedItem.CustomerId) });

                //get all Points of sale
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
                //filter by branch 
                if (User.BranchId != null)
                {
                    filters.Add(new GenericDataFormat.FilterItems() { Property = "BranchId", Operation = GenericDataFormat.FilterOperations.Equal, Value = User.BranchId });
                }
                List<PointOfSale> pos = new PointOfSaleModel<PointOfSale>().GetAsDDLst("POSId,POSName_en", "POSId", filters);
                model.PointOfSale = pos.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.POSName_en, Value = x.POSId.ToString(), Selected = (x.POSId == selectedItem.POSId) });

                //get all Warehouse
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
                //filter by branch 
                if (User.BranchId != null)
                {
                    filters.Add(new GenericDataFormat.FilterItems() { Property = "BranchId", Operation = GenericDataFormat.FilterOperations.Equal, Value = User.BranchId });
                }
                List<Warehouse> warehouse = new WarehouseModel<Warehouse>().GetAsDDLst("WarehouseId,WarehouseName_en", "WarehouseId", filters);
                model.Warehouse = warehouse.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.WarehouseName_en, Value = x.WarehouseId.ToString(), Selected = (x.WarehouseId == selectedItem.WarehouseId) });
                
                //get all technique systems
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
                List<TechniqueSystem> systems = new TechniqueSystemModel<TechniqueSystem>().GetAsDDLst("SystemId,SystemName", "SystemName", filters);
                model.TechniqueSystem = systems.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.SystemName, Value = x.SystemId.ToString(), Selected = (x.SystemId == selectedItem.SystemId) });

                //get all Item Families
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
                List<ItemFamily> itemFamilies = new ItemFamilyModel<ItemFamily>().GetAsDDLst("ItemFamilyId,ItemFamilyName_en", "ItemFamilyId", filters);
                ViewBag.ItemFamilies = itemFamilies.Select(x => new SelectListItem() { Text = x.ItemFamilyName_en, Value = x.ItemFamilyId.ToString() }).ToList();

                //get all model types
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false, LogicalOperation = GenericDataFormat.LogicalOperations.And });
                var sorts = new List<GenericDataFormat.SortItems>();
                sorts.Add(new GenericDataFormat.SortItems() { Property = "DeviceModelTypeName" });
                var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { Properties = "DeviceModelTypeId,DeviceModelTypeName,ItemFamilyId" }, Sorts = sorts };
                List<DeviceModelType> modelTypes = new DeviceModelTypeModel<DeviceModelType>().Get(requestBody);
                ViewBag.ModelTypes = modelTypes.Select(x => new SelectListItem() { Text = x.DeviceModelTypeName, Value = x.DeviceModelTypeId.ToString(), Group = new SelectListGroup() { Name = x.ItemFamilyId.ToString() } }).ToList();
            }
        }
        public override void FuncPreEdit(ref object id, ref DeliveryRequestEditBindModel EditItem)
        {
            id = EditItem.DeliveryRequestId;
            DateTime deliveryDateTime = (DateTime)Classes.Utilities.Utility.ParseDateTime(EditItem.DeliveryRequestDate_Str + " " + EditItem.DeliveryRequestTime_Str);
            EditItem.DeliveryRequestDateTime = deliveryDateTime;
            //get delivery request details 
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeliveryRequestDetails,DeliveryRequestDetails.DeviceModelType" } };
            var deliveryRequest = new DeliveryRequestModel<DeliveryRequest>().Get(requestBody).SingleOrDefault();
            var originalDeliveryRequestDetails = deliveryRequest.DeliveryRequestDetails;
            foreach (var item in EditItem.DeliveryRequestDetails)
            {
                var originalItem = originalDeliveryRequestDetails.SingleOrDefault(x => x.DeviceModelTypeId == item.DeviceModelTypeId);
                if (originalItem != null)
                {
                    item.CreateUserId = originalItem.CreateUserId;
                    item.CreateDate = originalItem.CreateDate;
                    item.ModifyUserId = User.UserId;
                    item.ModifyDate = DateTime.Now;
                }
                else
                {
                    item.CreateUserId = User.UserId;
                    item.CreateDate = DateTime.Now;
                }
            }
            
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "DeliveryRequests.xlsx";
            string properties = "SystemId,SystemName,IsBlock";
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }

        public ActionResult Assign(object id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var items = new List<DeliveryRequestDetailsViewModel>();
            FuncPreDetailsView(id, ref items);
            if (items.Count < 1 || items.ElementAt(0) == null)
            {
                return HttpNotFound();
            }
            var model = (DeliveryRequestDetailsViewModel)items.ElementAt(0);
            //get all technicians 
            //for test get all employee
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            List<EmployeeView> salesEmployees = new EmployeeModel<EmployeeView>().GetAsDDLst("EmployeeId,Employee_FullName_en", "Employee_FullName_en", filters, GenericDataFormat.SortType.Asc, true);
            ViewBag.Technician = salesEmployees.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.Employee_FullName_en, Value = x.EmployeeId.ToString() });

            return View(model);
            //return View();
        }

        [HttpPost]
        public ActionResult Assign(FormCollection fc)
        {
            int id = int.Parse(fc.Get("DeliveryRequestId"));
            //get delivery request details 
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeliveryRequestDetails" } };
            DeliveryRequest EditItem = new DeliveryRequestModel<DeliveryRequest>().Get(requestBody).SingleOrDefault();

            string ActualDeliveryRequestDate_Str = fc.Get("ActualDeliveryRequestDate_Str");
            string ActualDeliveryRequestTime_Str = fc.Get("ActualDeliveryRequestTime_Str");
            //set actual delivery date time 
            DateTime actualDeliveryDateTime = (DateTime)Classes.Utilities.Utility.ParseDateTime(ActualDeliveryRequestDate_Str + " " + ActualDeliveryRequestTime_Str);
            //set delivery request status
            if (actualDeliveryDateTime > EditItem.DeliveryRequestDateTime)
            {
                EditItem.DeliveryRequestStatusId = (int)Classes.Common.DBEnums.DeliveryRequestStatus.Technician_Assignation_With_Delay;
            }
            else
            {
                EditItem.DeliveryRequestStatusId = (int)Classes.Common.DBEnums.DeliveryRequestStatus.Technician_Assignation;
            }
            EditItem.ActualDeliveryDateTime = actualDeliveryDateTime;
            //set assign technician
            string[] techicians = fc.GetValues("Technician");
            EditItem.DeliveryRequestTechnician = techicians.Select(x => new DeliveryRequestTechnician() { DeliveryRequestId = EditItem.DeliveryRequestId, EmployeeId = int.Parse(x), CreateUserId = User.UserId, CreateDate = DateTime.Now }).ToList();
            //create instance to update object
            var instance = new DeliveryRequestModel<DeliveryRequest>();
            var item = instance.Update(EditItem, id);

            TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = Classes.Common.Enums.AlertMessageType.Success, TransactionCount = 1,  MessageContent = Resources.Store.DeliveryRequestAssignSuccessMessage   };
            return RedirectToAction("Index");
        }
    }
}