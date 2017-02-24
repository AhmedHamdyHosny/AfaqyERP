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
    public class CustomerServerAccountController : BaseController<CustomerServerAccount, CustomerServerAccountViewModel, CustomerServerAccountIndexViewModel, CustomerServerAccountDetailsViewModel, CustomerServerAccountCreateBindModel, CustomerServerAccountEditBindModel, CustomerServerAccountEditModel, CustomerServerAccountImportModel, CustomerServerAccountModel<CustomerServerAccount>, CustomerServerAccountModel<CustomerServerAccount>>
    {
        public override void FuncPreDetailsView(object id, ref List<CustomerServerAccountDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "CustomerServerAccountId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters};
            items = new CustomerServerAccountModel<CustomerServerAccountDetailsViewModel>().Get(requestBody);
        }
        public override void FuncPreInitCreateView()
        {
            //prepare dropdown list for item references
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<SystemServerIP> systemServerIP = new SystemServerIPModel<SystemServerIP>().GetAsDDLst("SystemServerId,ServerIP", "ServerIP", filters);
            ViewBag.SystemServerIP = systemServerIP.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.ServerIP, Value = x.SystemServerId.ToString() });
        }
        public override void FuncPreCreate(ref CustomerServerAccountCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref CustomerServerAccount EditItem, ref CustomerServerAccountEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new CustomerServerAccountModel<CustomerServerAccount>().Get(id);
            }
            if (EditItem != null)
            {
                model = new CustomerServerAccountEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                List<SystemServerIP> systemServerIP = new SystemServerIPModel<SystemServerIP>().GetAsDDLst("SystemServerId,ServerIP", "ServerIP");
                model.SystemServerIP = systemServerIP.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.ServerIP, Value = x.SystemServerId.ToString(), Selected = (selectedItem.SystemServerId == x.SystemServerId) });
            }
        }
        public override void FuncPreEdit(ref object id, ref CustomerServerAccountEditBindModel EditItem)
        {
            id = EditItem.CustomerServerAccountId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }

        [NonAction]
        public override ActionResult Export()
        {
            return null;
        }
        public override ActionResult Import(FormCollection fc)
        {
            return base.ImportAsEntities(fc);
        }
        public override void FuncPreImportAsEntities(ref List<CustomerServerAccountImportModel> items, FormCollection formCollection)
        {
            //set status 
            //List<Classes.Common.DBEnums.CustomerServerStatus> customerStatus = items.Select(x => (Classes.Common.DBEnums.CustomerServerStatus)Enum.Parse(typeof(Classes.Common.DBEnums.CustomerServerStatus), x.Status)).ToList();


            filters = new List<GenericDataFormat.FilterItems>();
            foreach (var serverIp in items.Select(x => x.ServerIP).Distinct().ToList())
            {
                filters.Add(new GenericDataFormat.FilterItems() { Property = "ServerIP", Operation = GenericDataFormat.FilterOperations.Equal, Value = serverIp, LogicalOperation = GenericDataFormat.LogicalOperations.Or });
            }
            List<SystemServerIP> serversIP = new SystemServerIPModel<SystemServerIP>().Get(new GenericDataFormat() { Filters = filters });
            
            items = items.Select(x =>
            {
                x.CreateUserId = User.UserId;
                x.CreateDate = DateTime.Now;
                x.SystemServerId = serversIP.SingleOrDefault(y => y.ServerIP == x.ServerIP).SystemServerId;
                Classes.Common.DBEnums.CustomerServerStatus accountStatus = (Classes.Common.DBEnums.CustomerServerStatus)Enum.Parse(typeof(Classes.Common.DBEnums.CustomerServerStatus), x.Status);
                x.CustomerServerStatusId = (int)accountStatus;
                return x;
            }).ToList();
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "CustomerServerAccounts.xlsx";
            string properties = "CustomerId,CustomerName_en,CustomerName_ar,BranchName_en,BranchName_ar,ServerIP,AccountId,SeverCustomerName,AccountUserName,CustomerServerStatus_en,CustomerServerStatus_ar,IsBlock";
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }

    }
}