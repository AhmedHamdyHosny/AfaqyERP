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
            List<im_itema> modelTypes = new ModelTypeModel<im_itema>().Get();
            var lstItems = modelTypes.Select(x => new CustomSelectListItem() { Selected = (x.ia_item_id == model.EditItem.ModelType_ia_item_id), Value = x.ia_item_id.ToString(), Text = x.ia_name });
            model.ModelType = lstItems;
            ViewBag.Title = "Home Page";

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(DeviceEditBindModel EditItem)
        {
            var id = EditItem.ModelType_ia_item_id;
            return Index();
        }
    }
}
