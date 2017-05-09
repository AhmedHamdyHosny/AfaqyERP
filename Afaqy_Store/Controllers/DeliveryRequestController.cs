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
    public class DeliveryRequestController : BaseController<DeliveryRequest, DeliveryRequestViewModel, DeliveryRequestIndexViewModel, DeliveryRequestDetailsViewModel, DeliveryRequestCreateBindModel, DeliveryRequestEditBindModel, DeliveryRequestEditModel, DeliveryRequest, DeliveryRequestModel<DeliveryRequest>, DeliveryRequestModel<DeliveryRequestViewModel>>
    {
        public override void FuncPreIndexView(ref List<DeliveryRequestIndexViewModel> model)
        {
            Classes.Utilities.DeliveryRequestAccessRole permssions = new Classes.Utilities.DeliveryRequestAccessRole() { CreatePremission = true, DetailsPremission = true, EditPremission = true };
            if(User.UserTypeId == (int)DBEnums.UserType.System_Adminstrator)
            {
                permssions.DeliveryPremission = true;
                permssions.AssignPremission = true;
                permssions.RececivedPremission = true;
                permssions.DeletePremission = true;
            }
            else
            {
                //get user employee info
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "UserId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = User.UserId,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
                var userEmp = new EmployeeModel<Employee>().Get(new GenericDataFormat() { Filters = filters }).SingleOrDefault();
                if(userEmp.JobTitleId == (int)DBEnums.JobTitle.Storekeeper)
                {
                    permssions.RececivedPremission = true;
                    permssions.DeliveryPremission = true;
                }
                else if(userEmp.JobTitleId == (int)DBEnums.JobTitle.Technicians_General_Manager || userEmp.JobTitleId == (int)DBEnums.JobTitle.Branch_Technicians_Manager)
                {
                    permssions.AssignPremission = true;
                }
            }
            ViewBag.Permissions = permssions;
            base.FuncPreIndexView(ref model);
        }
        public override void FuncPreDetailsView(object id, ref List<DeliveryRequestDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new DeliveryRequestModel<DeliveryRequestDetailsViewModel>().GetView<DeliveryRequestDetailsViewModel>(requestBody).PageItems;

            foreach (var item in items)
            {
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, Value = item.DeliveryRequestId, LogicalOperation = GenericDataFormat.LogicalOperations.And });
                var detailsRequestBody = new GenericDataFormat() { Filters = filters };
                item.DeliveryRequestDetails = new DeliveryRequestDetailsModel<RequestDetails_DetailsViewModel>().GetView<RequestDetails_DetailsViewModel>(detailsRequestBody).PageItems;

                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, Value = item.DeliveryRequestId, LogicalOperation = GenericDataFormat.LogicalOperations.And });
                var technicianRequestBody = new GenericDataFormat() { Filters = filters };
                item.DeliveryRequestTechnician = new DeliveryRequestTechnicianModel<DeliveryRequestTechnicianViewModel>().GetView<DeliveryRequestTechnicianViewModel>(technicianRequestBody).PageItems;
            }

        }
        public override ActionResult FuncPostDetailsView(ref DeliveryRequestDetailsViewModel model)
        {
            bool showStoreReceivedBtn = false;
            //get warehouse employee
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems { Property = "Warehouse_wa_code", Operation = GenericDataFormat.FilterOperations.Equal, Value = model.Warehouse_wa_code, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            var warehouse = new WarehouseInfoModel<WarehouseInfo>().Get(new GenericDataFormat() { Filters = filters }).SingleOrDefault();
            if (warehouse != null)
            {
                showStoreReceivedBtn = true;
                //check if user received this request before
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, Value = model.DeliveryRequestId, LogicalOperation = GenericDataFormat.LogicalOperations.And });
                filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestStatusId", Operation = GenericDataFormat.FilterOperations.Equal, Value = Classes.Common.DBEnums.DeliveryRequestStatus.Store_Notified, LogicalOperation = GenericDataFormat.LogicalOperations.And });
                filters.Add(new GenericDataFormat.FilterItems() { Property = "CreateUserId", Operation = GenericDataFormat.FilterOperations.Equal, Value = User.UserId, LogicalOperation = GenericDataFormat.LogicalOperations.And });
                var history = new DeliveryRequestStatusHistoryModel<DeliveryRequestStatusHistory>().Get(new GenericDataFormat() { Filters = filters });
                if (history != null && history.Count > 0)
                {
                    showStoreReceivedBtn = false;
                }
            }
            ViewBag.ShowStoreReceivedBtn = showStoreReceivedBtn;
            return View(model);
        }
        public override void FuncPreInitCreateView()
        {
            //prepare dropdown list for item references
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<SaleTransactionType> SaleTransactionTypes = new SaleTransactionTypeModel<SaleTransactionType>().GetAsDDLst("SaleTransactionTypeId,SaleTransactionType_en", "SaleTransactionTypeId", filters);
            ViewBag.SaleTransactionTypeId = SaleTransactionTypes.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.SaleTransactionType_en, Value = x.SaleTransactionTypeId.ToString(), Selected = (x.SaleTransactionTypeId == (int)Classes.Common.DBEnums.SaleTransactionType.Sales) });

            //get customers
            //set unblock customers
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "aux_blocked", Operation = GenericDataFormat.FilterOperations.NotEqual, Value = 1, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            //if user is sales man then get sales man customer
            //and if user is sales man manager then get all customer of sales under this manager
            //else get all customers
            EmployeeViewModel emp = null;
            if (User.EmployeeId != null)
            {
                emp = new EmployeeModel<EmployeeViewModel>().Get(User.EmployeeId);
                if (emp.JobTitleId == (int)Classes.Common.DBEnums.JobTitle.Branch_Sales_Manager)
                {
                    //get sales men under this manager
                    List<GenericDataFormat.FilterItems> salesFilters = new List<GenericDataFormat.FilterItems>();
                    salesFilters.Add(new GenericDataFormat.FilterItems() {
                        Property = "ManagerId",
                        Operation = GenericDataFormat.FilterOperations.Equal,
                        Value = emp.EmployeeId,
                        LogicalOperation = GenericDataFormat.LogicalOperations.And
                    });
                    List<Employee> sales_men = new EmployeeModel<Employee>().Get(new GenericDataFormat() { Filters = salesFilters });
                    foreach (var salesMan in sales_men)
                    {
                        filters.Add(new GenericDataFormat.FilterItems() {
                            Property = "salecode",
                            Operation = GenericDataFormat.FilterOperations.Equal,
                            Value = salesMan.EmployeeId,
                            LogicalOperation = GenericDataFormat.LogicalOperations.Or
                        });
                    }
                }
                else if (emp.JobTitleId == (int)Classes.Common.DBEnums.JobTitle.Sales_Man)
                {
                    filters.Add(new GenericDataFormat.FilterItems() { Property = "salecode", Operation = GenericDataFormat.FilterOperations.Equal, Value = emp.EmployeeId, LogicalOperation = GenericDataFormat.LogicalOperations.And });
                }
            }

            List<rpaux> customers = new CustomerModel<rpaux>().GetAsDDLst("aux_id,name,altname", "name", filters);
            //add customer english name
            var customerSelectListItems = customers.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.name, Value = x.aux_id.ToString() }).ToList();
            //add customer arabic name
            customerSelectListItems.AddRange(customers.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.altname, Value = x.aux_id.ToString() }));
            //get all customer server accounts
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<CustomerServerAccount> customerServerAccounts = new CustomerServerAccountModel<CustomerServerAccount>().GetAsDDLst("CustomerId,SeverCustomerName,AccountUserName", "SeverCustomerName", filters);
            //filter cusromer server accounts by exist in customers list
            if (emp != null && (emp.JobTitleId == (int)Classes.Common.DBEnums.JobTitle.Branch_Sales_Manager || emp.JobTitleId == (int)Classes.Common.DBEnums.JobTitle.Sales_Man))
            {
                customerServerAccounts = customerServerAccounts.Where(x => customers.Any(y => y.aux_id == x.CustomerId)).ToList();
            }
            //add customer name in server 
            customerSelectListItems.AddRange(customerServerAccounts.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.SeverCustomerName, Value = x.CustomerId.ToString() }));
            //get distinct value from list
            customerSelectListItems = customerSelectListItems.Where(x => !string.IsNullOrEmpty(x.Text)).OrderBy(x => x.Text).GroupBy(x => new { x.Text }).Select(x => x.FirstOrDefault()).ToList();
            ViewBag.CustomerId = customerSelectListItems;

            //get all Points of sale
            //filter by branch 
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "ps_branch",
                Operation = GenericDataFormat.FilterOperations.NotEqual,
                Value = null,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            if (emp != null && (emp.JobTitleId == (int)Classes.Common.DBEnums.JobTitle.Branch_Sales_Manager || emp.JobTitleId == (int)Classes.Common.DBEnums.JobTitle.Sales_Man))
            {
                string branchCode = string.IsNullOrEmpty(emp.Branch_br_code) ? emp.Branch_br_code : User.Branch_br_code;
                filters.Add(new GenericDataFormat.FilterItems() { Property = "ps_branch", Operation = GenericDataFormat.FilterOperations.Equal, Value = branchCode, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            }
            List<im_points> pos = new PointOfSaleModel<im_points>().GetAsDDLst("ps_cmp_seq,ps_code,ps_name,ps_altname", "ps_code", filters);
            ViewBag.POSId = pos.Select(x => new Classes.Helper.CustomSelectListItem() { Text = Classes.Utilities.Utility.GetDDLText(x.ps_name, x.ps_altname), Value = x.ps_code });

            //get all Warehouse
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "wa_inactive", Operation = GenericDataFormat.FilterOperations.NotEqual, Value = 1 });
            List<im_warehouse> warehouse = new WarehouseModel<im_warehouse>().GetAsDDLst("wa_cmp_seq,wa_code,wa_name,wa_altname", "wa_code", filters);
            ViewBag.WarehouseId = warehouse.Select(x => new Classes.Helper.CustomSelectListItem() { Text = Classes.Utilities.Utility.GetDDLText(x.wa_name, x.wa_altname), Value = x.wa_code });


            //get all technique systems
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<TechniqueSystem> systems = new TechniqueSystemModel<TechniqueSystem>().GetAsDDLst("SystemId,SystemName", "SystemName", filters);
            ViewBag.SystemId = systems.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.SystemName, Value = x.SystemId.ToString() });

            //get all Item Families
            filters = null;
            List<im_family> itemFamilies = new ItemFamilyModel<im_family>().GetAsDDLst("fa_cmp_seq,fa_code,fa_name,fa_altname", "fa_code", filters);
            ViewBag.ItemFamilies = itemFamilies.Select(x => new SelectListItem() { Text = Classes.Utilities.Utility.GetDDLText(x.fa_name, x.fa_altname), Value = x.fa_code }).ToList();

            //get all model types
            filters = null;
            var sorts = new List<GenericDataFormat.SortItems>();
            sorts.Add(new GenericDataFormat.SortItems() { Property = "ia_item_code" });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { Properties = "ia_item_id,ia_item_code,ia_name,ia_altname,ia_cmp_seq,ia_fa_code" }, Sorts = sorts };
            List<im_itema> modelTypes = new ModelTypeModel<im_itema>().Get(requestBody);
            ViewBag.ModelTypes = modelTypes.Select(x => new SelectListItem() { Text = x.ia_item_code, Value = x.ia_item_id.ToString(), Group = new SelectListGroup() { Name = x.ia_fa_code } }).ToList();
        }
        public override void FuncPreCreate(ref DeliveryRequestCreateBindModel model)
        {
            var companySequence = Classes.Utilities.Utility.GetCompanySequence();
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
            model.cmp_seq = companySequence;
            model.DeliveryRequestStatusId = (int)Classes.Common.DBEnums.DeliveryRequestStatus.Approved;
            DateTime deliveryDateTime = (DateTime)Classes.Utilities.Utility.ParseDateTime(model.DeliveryRequestDate_Str + " " + model.DeliveryRequestTime_Str);
            model.DeliveryRequestDateTime = deliveryDateTime;
            model.DeliveryRequestDetails = model.DeliveryRequestDetails.Select(x => { x.cmp_seq = companySequence; x.CreateUserId = User.UserId; x.CreateDate = DateTime.Now; return x; }).ToList();

        }
        public override ActionResult FuncPostCreate(ref DeliveryRequestCreateBindModel model, ref DeliveryRequest insertedItem)
        {
            if (DeliveryRequestCreateBindModel.SendAfterApproveDeliveryRequestNotification(insertedItem, Url.Action("Details"), Url.Action("Assign"), User.UserId))
            {
                //do nothing
            }
            return base.FuncPostCreate(ref model, ref insertedItem);
        }
        public override void FuncPreInitEditView(object id, ref DeliveryRequest EditItem, ref DeliveryRequestEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = id });
                var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeliveryRequestDetails" } };
                EditItem = new DeliveryRequestModel<DeliveryRequest>().Get(requestBody).SingleOrDefault();

            }
            if (EditItem != null)
            {
                model = new DeliveryRequestEditModel();
                model.EditItem = EditItem;
                //get delivery request details
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, Value = EditItem.DeliveryRequestId });
                var requestBody = new GenericDataFormat() { Filters = filters };
                model.DeliveryRequestDetails = new DeliveryRequestDetailsModel<DeliveryRequestDetailsView>().GetView<DeliveryRequestDetailsView>(requestBody).PageItems;
                var selectedItem = EditItem;

                //prepare dropdown list for item references
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
                List<SaleTransactionType> SaleTransactionTypes = new SaleTransactionTypeModel<SaleTransactionType>().GetAsDDLst("SaleTransactionTypeId,SaleTransactionType_en", "SaleTransactionTypeId", filters);
                model.SaleTransactionType = SaleTransactionTypes.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.SaleTransactionType_en, Value = x.SaleTransactionTypeId.ToString(), Selected = (x.SaleTransactionTypeId == selectedItem.SaleTransactionTypeId) });

                //get all customers
                filters = null;
                List<rpaux> customers = new CustomerModel<rpaux>().GetAsDDLst("aux_id,name,altname", "name", filters);
                //add customer english name
                var customerSelectListItems = customers.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.name, Value = x.aux_id.ToString() }).ToList();
                //add customer arabic name
                customerSelectListItems.AddRange(customers.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.altname, Value = x.aux_id.ToString() }));
                //get all customer server accounts
                filters = null;
                List<CustomerServerAccount> customerServerAccounts = new CustomerServerAccountModel<CustomerServerAccount>().GetAsDDLst("CustomerId,SeverCustomerName,AccountUserName", "SeverCustomerName", filters);
                //add customer name in server 
                customerSelectListItems.AddRange(customerServerAccounts.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.SeverCustomerName, Value = x.CustomerId.ToString() }));
                //get distinct value from list
                customerSelectListItems = customerSelectListItems.Where(x => !string.IsNullOrEmpty(x.Text)).OrderBy(x => x.Text).Distinct().ToList();
                model.Customer = customerSelectListItems.Distinct().Select(x => { x.Selected = (x.Value == selectedItem.Customer_aux_id.ToString() && x.Text == selectedItem.CustomerName); return x; });

                //get all Points of sale
                //filter by branch 
                filters = null;
                filters.Add(new GenericDataFormat.FilterItems() { Property = "ps_code", Operation = GenericDataFormat.FilterOperations.Equal, Value = EditItem.POS_ps_code });
                List<im_points> pos = new PointOfSaleModel<im_points>().GetAsDDLst("ps_cmp_seq,ps_code,ps_name,ps_altname", "ps_code", filters);
                model.PointOfSale = pos.Select(x => new Classes.Helper.CustomSelectListItem() { Text = Classes.Utilities.Utility.GetDDLText(x.ps_name, x.ps_altname), Value = x.ps_code, Selected = (x.ps_code == selectedItem.POS_ps_code && x.ps_cmp_seq == selectedItem.cmp_seq) });

                //get all Warehouse
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "wa_inactive", Operation = GenericDataFormat.FilterOperations.NotEqual, Value = 1 });
                List<im_warehouse> warehouse = new WarehouseModel<im_warehouse>().GetAsDDLst("wa_cmp_seq,wa_code,wa_name,wa_altname", "wa_code", filters);
                model.Warehouse = warehouse.Select(x => new Classes.Helper.CustomSelectListItem() { Text = Classes.Utilities.Utility.GetDDLText(x.wa_name, x.wa_altname), Value = x.wa_code, Selected = (x.wa_code == selectedItem.Warehouse_wa_code && x.wa_cmp_seq == selectedItem.cmp_seq) });

                //get all technique systems
                filters = new List<GenericDataFormat.FilterItems>();
                filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
                List<TechniqueSystem> systems = new TechniqueSystemModel<TechniqueSystem>().GetAsDDLst("SystemId,SystemName", "SystemName", filters);
                model.TechniqueSystem = systems.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.SystemName, Value = x.SystemId.ToString(), Selected = (x.SystemId == selectedItem.SystemId) });

                //get all Item Families
                filters = null;
                List<im_family> itemFamilies = new ItemFamilyModel<im_family>().GetAsDDLst("fa_cmp_seq,fa_code,fa_name,fa_altname", "fa_code", filters);
                ViewBag.ItemFamilies = itemFamilies.Select(x => new SelectListItem() { Text = Classes.Utilities.Utility.GetDDLText(x.fa_name, x.fa_altname), Value = x.fa_code }).ToList();

                //get all model types
                filters = null;
                var sorts = new List<GenericDataFormat.SortItems>();
                sorts.Add(new GenericDataFormat.SortItems() { Property = "ia_item_code" });
                requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { Properties = "ia_item_id,ia_item_code,ia_name,ia_altname,ia_cmp_seq,ia_fa_code" }, Sorts = sorts };
                List<im_itema> modelTypes = new ModelTypeModel<im_itema>().Get(requestBody);
                ViewBag.ModelTypes = modelTypes.Select(x => new SelectListItem() { Text = x.ia_item_code, Value = x.ia_item_id.ToString(), Group = new SelectListGroup() { Name = x.ia_fa_code } }).ToList();

            }
        }
        public override void FuncPreEdit(ref object id, ref DeliveryRequestEditBindModel EditItem)
        {
            id = EditItem.DeliveryRequestId;
            var originalRequest = new DeliveryRequestModel<DeliveryRequest>().Get(id);
            //if (originalRequest.DeliveryRequestStatusId == (int)Classes.Common.DBEnums.DeliveryRequestStatus.New || originalRequest.DeliveryRequestStatusId == (int)Classes.Common.DBEnums.DeliveryRequestStatus.Approved)
            //{
            DateTime deliveryDateTime = (DateTime)Classes.Utilities.Utility.ParseDateTime(EditItem.DeliveryRequestDate_Str + " " + EditItem.DeliveryRequestTime_Str);
            EditItem.DeliveryRequestDateTime = deliveryDateTime;
            //get delivery request details 
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeliveryRequestDetails" } };
            var deliveryRequest = new DeliveryRequestModel<DeliveryRequest>().Get(requestBody).SingleOrDefault();
            var originalDeliveryRequestDetails = deliveryRequest.DeliveryRequestDetails;
            foreach (var item in EditItem.DeliveryRequestDetails)
            {
                var originalItem = originalDeliveryRequestDetails.SingleOrDefault(x => x.ModelType_ia_item_id == item.ModelType_ia_item_id);
                item.cmp_seq = EditItem.cmp_seq;
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
            //}

        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "DeliveryRequests.xlsx";
            string properties = string.Join(",", typeof(DeliveryRequestView).GetProperties().Select(x => x.Name).Where(x => !x.Contains("_ar"))); ;
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
            //get branch technicians 
            //filter by technician is not block
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "IsBlock",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = false,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            //get warehouse branch
            List<GenericDataFormat.FilterItems> warehouseFilters = new List<GenericDataFormat.FilterItems>();
            warehouseFilters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "wa_code",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = model.Warehouse_wa_code,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });

            var branchCode = new WarehouseModel<im_warehouse>().Get(new GenericDataFormat() { Filters = warehouseFilters }).SingleOrDefault().wa_costcenter;
            //filter by branch technicians
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "Branch_br_code",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = branchCode,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            //get technician only
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "JobTitleId",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = (int)Classes.Common.DBEnums.JobTitle.Technician,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            List<EmployeeView> technicianEmployees = new EmployeeModel<EmployeeView>().GetAsDDLst("EmployeeId,Employee_FullName_en,Employee_FullName_ar", "Employee_FullName_en", filters, GenericDataFormat.SortType.Asc, true);
            ViewBag.Technician = technicianEmployees.Select(x => new Classes.Helper.CustomSelectListItem() { Text = Classes.Utilities.Utility.GetDDLText(x.Employee_FullName_en, x.Employee_FullName_ar), Value = x.EmployeeId.ToString(), Selected = (model.DeliveryRequestTechnician.Any(y => y.Employee_aux_id == x.EmployeeId)) });
            return View(model);
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
            EditItem.DeliveryRequestTechnician = fc.GetValues("RequestTechnician").Select(x => new DeliveryRequestTechnician() { cmp_seq = EditItem.cmp_seq,  DeliveryRequestId = EditItem.DeliveryRequestId, Employee_aux_id = int.Parse(x), CreateUserId = User.UserId, CreateDate = DateTime.Now }).ToList();
            //create instance to update object
            var instance = new DeliveryRequestModel<DeliveryRequest>();
            instance.Update(EditItem, id);

            if(DeliveryRequestEditBindModel.SendAfterAssignDeliveryRequestNotification(EditItem, Url.Action("Details"),Url.Action("Index","DeliveryNote"),User.UserId))
            {
                //do nothing
            }

            TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = Classes.Common.Enums.AlertMessageType.Success, TransactionCount = 1,  MessageContent = Resources.Store.DeliveryRequestAssignSuccessMessage   };
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Details(FormCollection fc)
        {
            int id = int.Parse(fc.Get("DeliveryRequestId"));
            //get delivery request
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DeliveryRequestId", Operation = GenericDataFormat.FilterOperations.Equal, LogicalOperation = GenericDataFormat.LogicalOperations.And, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters, Includes = new GenericDataFormat.IncludeItems() { References = "DeliveryRequestDetails,DeliveryRequestTechnician" } };
            DeliveryRequest EditItem = new DeliveryRequestModel<DeliveryRequest>().Get(requestBody).SingleOrDefault();
            //update status
            if (EditItem != null)
            {
                EditItem.DeliveryRequestStatusId = (int)Classes.Common.DBEnums.DeliveryRequestStatus.Store_Notified;
            }
            //update object
            var instance = new DeliveryRequestModel<DeliveryRequest>();
            var item = instance.Update(EditItem, id);

            TempData["AlertMessage"] = new Classes.Utilities.AlertMessage() { MessageType = Classes.Common.Enums.AlertMessageType.Success, TransactionCount = 1, MessageContent = Resources.Store.DeliveryRequestReceivedSuccessMessage };
            return RedirectToAction("Index");
        }
    }
}