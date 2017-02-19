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
    public class BrandServerPortController : BaseController<BrandServerPort,BrandServerPortViewModel, BrandServerPortIndexViewModel, BrandServerPortDetailsViewModel, BrandServerPortCreateBindModel,BrandServerPortEditBindModel,BrandServerPortEditModel, BrandServerPort, BrandServerPortModel<BrandServerPort>,BrandServerPortModel<BrandServerPortViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<BrandServerPortDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "BrandPortId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters};
            items = new BrandServerPortModel<BrandServerPortDetailsViewModel>().GetView<BrandServerPortDetailsViewModel>(requestBody).PageItems;
        }
        public override void FuncPreInitCreateView()
        {
            //prepare dropdown list for item references
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "IsBlock", Operation = GenericDataFormat.FilterOperations.Equal, Value = false });
            List<Brand> brands = new BrandModel<Brand>().GetAsDDLst("BrandId,BrandName", "BrandName", filters);
            ViewBag.BrandId = brands.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.BrandName, Value = x.BrandId.ToString() });
            List<SystemServerIP> IPs = new SystemServerIPModel<SystemServerIP>().GetAsDDLst("SystemServerId,ServerIP", "ServerIP", filters);
            ViewBag.SystemServerId = IPs.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.ServerIP, Value = x.SystemServerId.ToString() });
        }
        public override void FuncPreCreate(ref BrandServerPortCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref BrandServerPort EditItem, ref BrandServerPortEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new BrandServerPortModel<BrandServerPort>().Get(id);
            }
            if (EditItem != null)
            {
                model = new BrandServerPortEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                List<Brand> brands = new BrandModel<Brand>().GetAsDDLst("BrandId,BrandName", "BrandName");
                model.Brand = brands.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.BrandName, Value = x.BrandId.ToString(),Selected = (selectedItem.BrandId == x.BrandId) });
                List<SystemServerIP> IPs = new SystemServerIPModel<SystemServerIP>().GetAsDDLst("SystemServerId,ServerIP", "ServerIP");
                model.SystemServerIP = IPs.Select(x => new Classes.Helper.CustomSelectListItem() { Text = x.ServerIP, Value = x.SystemServerId.ToString(), Selected = (selectedItem.SystemServerId == x.SystemServerId) });
            }
        }
        public override void FuncPreEdit(ref object id, ref BrandServerPortEditBindModel EditItem)
        {
            id = EditItem.BrandPortId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "BrandServerPorts.xlsx";
            string properties = "BrandPortId,PortNumber,IsBlock";
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }
    }
}