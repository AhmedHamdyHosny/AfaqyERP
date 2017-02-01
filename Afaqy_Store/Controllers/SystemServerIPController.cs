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
    public class SystemServerIPController : BaseController<SystemServerIP,SystemServerIPViewModel,SystemServerIPCreateBindModel,SystemServerIPEditBindModel,SystemServerIPEditModel,SystemServerIPModel<SystemServerIP>,SystemServerIPModel<SystemServerIPViewModel>>
    {
        public override void FuncPreIndexView(ref List<SystemServerIPViewModel> model)
        {
            var requestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { References = "TechniqueSystem" } };
            model = new SystemServerIPModel<SystemServerIPViewModel>().Get(requestBody);
        }
        public override void FuncPreDetailsView(object id, ref List<SystemServerIPViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "SystemServerId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { References = "TechniqueSystem" },Filters = filters };
            items = new SystemServerIPModel<SystemServerIPViewModel>().Get(requestBody);
        }
        public override void FuncPreInitCreateView()
        {
            //prepare dropdown list for item references
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<TechniqueSystem> techniqueSystems = new TechniqueSystemModel<TechniqueSystem>().GetAsDDLst("SystemId,SystemName", "SystemName",filters);
            ViewBag.SystemId = techniqueSystems.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.SystemName, Value = x.SystemId.ToString() });
        }
        public override void FuncPreCreate(ref SystemServerIPCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref SystemServerIP EditItem, ref SystemServerIPEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new SystemServerIPModel<SystemServerIP>().Get(id);
            }
            if (EditItem != null)
            {
                model = new SystemServerIPEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                List<TechniqueSystem> techniqueSystems = new TechniqueSystemModel<TechniqueSystem>().GetAsDDLst("SystemId,SystemName", "SystemName");
                model.TechniqueSystem = techniqueSystems.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.SystemName, Value = x.SystemId.ToString(), Selected = (selectedItem.SystemId == x.SystemId) });
            }
        }
        public override void FuncPreEdit(ref object id, ref SystemServerIPEditBindModel EditItem)
        {
            id = EditItem.SystemServerId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "ServerIP.xlsx";
            //filters
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = "ServerIP,TechniqueSystem.SystemName" } };
        }
    }


}