using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Controllers
{
    public class CommonController : Controller
    {
        public PartialViewResult Partial_DropDownList(string id, string name,IEnumerable<SelectListItem> list,string placeHolder, bool multipleSelect = false , bool readOnly=false)
        {
            //DropDownList list = new DropDownList();
            return PartialView();
        }
    }
}