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
            var lstItems = deviceModels.Select(x => new CustomSelectListItem() { Selected = (x.DeviceModelTypeId == model.EditItem.DeviceModelTypeId), Value = x.DeviceModelTypeId.ToString(), Text = x.DeviceModelTypeName });
            model.DeviceModelType = lstItems;
            ViewBag.Title = "Home Page";

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(DeviceEditBindModel EditItem)
        {
            var id = EditItem.DeviceModelTypeId;
            return Index();
        }
    }
}
