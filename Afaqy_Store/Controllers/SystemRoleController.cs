using GenericApiController.Utilities;
using Security.DataLayer;
using Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Controllers
{
    public class SystemRoleController : BaseController<Role,SystemRoleViewModel, SystemRoleIndexViewModel, SystemRoleDetailsViewModel, SystemRoleCreateBindModel, SystemRoleEditBindModel, SystemRoleEditModel, Role, SystemRoleModel<Role>,SystemRoleModel<SystemRoleViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<SystemRoleDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "RoleId", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new SystemRoleModel<SystemRoleDetailsViewModel>().Get(requestBody);
        }
        public override void FuncPreCreate(ref SystemRoleCreateBindModel model)
        {
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
            if(model.SystemRoleServiceAccess.Any(x=>x.Any(y=>y.Checked == true)))
            {
                List<SystemServiceAccessViewModel.CustSystemAccess> roleServiceAccess = model.SystemRoleServiceAccess.SelectMany(x=>x).ToList();
                //Buffer.BlockCopy(model.SystemRoleServiceAccess, 0, roleServiceAccess, 0, model.SystemRoleServiceAccess.Length);
                model.RoleServiceAccess = roleServiceAccess.Where(x => x.Checked == true)
                                        .Select(x => new RoleServiceAccess()
                                        {
                                            ServiceAccessId = x.ServiceAccessId,
                                            CreateUserId = User.UserId,
                                            CreateDate = DateTime.Now
                                        }).ToList();
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("CreateRole")]
        //public ActionResult Create(SystemRoleCreateBindModel model, FormCollection fc)
        //{
        //    //return base.Create();
        //    return RedirectToAction("Index");
        //}


        public override void FuncPreInitEditView(object id, ref Role EditItem, ref SystemRoleEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new SystemRoleModel<Role>().Get(id);
            }
            if (EditItem != null)
            {
                model = new SystemRoleEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref SystemRoleEditBindModel EditItem)
        {
            id = EditItem.RoleId;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "SystemRoles.xlsx";
            string properties = "RoleId,RoleName_en,RoleName_ar,Active";
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }

        [HttpPost]
        public JsonResult GetServiceAccessView(GenericDataFormat options)
        { 
            if (options.Sorts != null && options.Sorts.Count > 1)
            {
                options.Sorts = options.Sorts.Where(x => x.Priority == 1).ToList();
            }
            var result = new SystemServiceAccessModel<SystemServiceAccessViewModel>().GetView<SystemServiceAccessViewModel>(options).PageItems ;
            var model = new PaginationResult<SystemServiceAccessViewModel.CustSystemService>(); 
            model.PageItems = result.Select(x=> x.ServiceId).Distinct().Select(serviceId => new SystemServiceAccessViewModel.CustSystemService(serviceId, result) ).ToList();
            model.TotalItemsCount = model.PageItems.Count();
            return Json(model);
        }
    }
}