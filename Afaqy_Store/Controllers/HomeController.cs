using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Afaqy_Store.Models;
using Afaqy_Store.DataLayer;
using Classes.Helper;

namespace Afaqy_Store.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new DeviceEditModel() { EditItem = new DeviceModel<Device>().Get(5) };
            List<DeviceModelType> deviceModels = new DeviceModelTypeModel<DeviceModelType>().Get();
            var lstItems = deviceModels.Select(x => new CustomSelectListItem() { Selected = (x.ModelTypeId == model.EditItem.ModelTypeId), Value = x.ModelTypeId.ToString(), Text = x.ModelTypeName });
            model.ModelType= lstItems;
            ViewBag.Title = "Home Page";

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(DeviceEditBindModel EditItem)
        {
            var id = EditItem.ModelTypeId;
            return Index();
        }
    }
}
