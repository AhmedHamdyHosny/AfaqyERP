using Classes.Utilities;
using Afaqy_Store.DataLayer;
using Afaqy_Store.Models;
using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Classes.Common;

namespace Afaqy_Store.Controllers
{
    public class CustomerController : BaseController<Customer,CustomerViewModel,CustomerIndexViewModel,CustomerDetailsViewModel, CustomerCreateBindModel, CustomerEditBindModel, CustomerEditModel, CustomerImportModel, CustomerModel<Customer>, CustomerModel<CustomerViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<CustomerDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "CustomerId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters};
            items = new CustomerModel<CustomerDetailsViewModel>().Get(requestBody);
        }
        public void FuncPreInitCreateView(int? CustomerType = null, int? Branch = null)
        {
            //prepare dropdown list for item references
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            if(CustomerType != null)
            {
                filters.Add(new GenericDataFormat.FilterItems() { Property = "CustomerTypeId", Operation = GenericDataFormat.FilterOperations.Equal, Value = (int)CustomerType, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            }
            List<CustomerType> CustomerTypes = new CustomerTypeModel<CustomerType>().GetAsDDLst("CustomerTypeId,CustomerTypeName_en", "CustomerTypeName_en", filters);
            ViewBag.CustomerTypeId = CustomerTypes.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.CustomerTypeName_en, Value = x.CustomerTypeId.ToString() });
            
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            if (Branch != null)
            {
                filters.Add(new GenericDataFormat.FilterItems() { Property = "BranchId", Operation = GenericDataFormat.FilterOperations.Equal, Value = (int)Branch, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            }
            List<Branch> branches = new BranchModel<Branch>().GetAsDDLst("BranchId,BranchName_en", "BranchId", filters);
            ViewBag.BranchId = branches.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.BranchName_en, Value = x.BranchId.ToString() });

            //get only sales employee
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            filters.Add(new GenericDataFormat.FilterItems() { Property = "DepartmentId", Operation = GenericDataFormat.FilterOperations.Equal, Value = (int)DBEnums.Department.Sales, LogicalOperation = GenericDataFormat.LogicalOperations.And });
            List<EmployeeView> salesEmployees = new EmployeeModel<EmployeeView>().GetAsDDLst("EmployeeId,Employee_FullName_en", "Employee_FullName_en", filters,GenericDataFormat.SortType.Asc,true);
            ViewBag.EmployeeId = salesEmployees.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.Employee_FullName_en, Value = x.EmployeeId.ToString() });

            //bind customer contact method for add customer contact
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<ContactMethod> ContactMethods = new ContactMethodModel<ContactMethod>().GetAsDDLst("ContactMethodId,ContactMethodName_en", "ContactMethodName_en", filters);
            ViewBag.ContactMethods = ContactMethods.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.ContactMethodName_en, Value = x.ContactMethodId.ToString() }).ToList();

        }

        [NonAction]
        public override ActionResult Create()
        {
            return null;
        }
        [NonAction]
        public override ActionResult Create(CustomerCreateBindModel model)
        {
            return null;
        }

        [ActionName("Create")]
        public ActionResult Create(int? CustomerType = null, int? Branch = null)
        {
            //for test
            CustomerType = 2;
            //Branch = 1;
            FuncPreInitCreateView(CustomerType, Branch);
            Customer model = new Customer();
            model.CustomerTypeId = CustomerType;
            model.BranchId = Branch;
            //to hide dolphin id textbox
            model.DolphinId = -1;

            return View(model);
        }
        public override void FuncPreCreate(ref CustomerCreateBindModel model)
        {
            model.CustomerStatusId = (int)DBEnums.CustomerStatus.New;
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
            model.CustomerContact = model.CustomerContact.Select(x => 
            {
                x.CreateUserId = User.CreateUserId;
                x.CreateDate = DateTime.Now;
                x.CustomerContactDetails = x.CustomerContactDetails.Select(y=> 
                    {
                        y.CreateUserId = User.UserId;
                        y.CreateDate = DateTime.Now;
                        return y;
                    }).ToList();
                return x;
            }).ToList();
        }

        [ActionName("Create")]
        [HttpPost]
        public JsonResult CreateCustomer(CustomerCreateBindModel model)
        {
            FuncPreCreate(ref model);
            var result = new CustomerModel<Customer>().Insert(model);
            JsonResponse response = new JsonResponse() { Status = 1 , Result= result};
            return Json(response);
        }
        
        public override void FuncPreInitEditView(object id, ref Customer EditItem, ref CustomerEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new CustomerModel<Customer>().Get(id);
            }
            if (EditItem != null)
            {
                model = new CustomerEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                List<Branch> branches = new CustomerModel<Branch>().GetAsDDLst("BranchId,BranchName_en", "BranchName_en");
                model.Branch = branches.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.BranchName_en, Value = x.BranchId.ToString(), Selected = (selectedItem.BranchId == x.BranchId) });
            }
        }
        public override void FuncPreEdit(ref object id, ref CustomerEditBindModel EditItem)
        {
            id = EditItem.CustomerId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override ActionResult Import(FormCollection fc)
        {
            return base.ImportAsEntities(fc);
        }
        public override void FuncPreImportAsEntities(ref List<CustomerImportModel> items, FormCollection formCollection)
        {
            //for test
            //int branchId = GetBranch;
            items = items.Select(x =>
            {
                x.CreateUserId = User.UserId;
                x.CreateDate = DateTime.Now;
                //for test
                //x.BranchId = branchId;
                return x;
            }).ToList();
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "Customers.xlsx";
            string properties = string.Join(",", typeof(CustomerView).GetProperties().Select(x => x.Name).Where(x => !x.Contains("_ar")));
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }
    }
}