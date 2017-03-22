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

        public PartialViewResult Partial_Paging()
        {
            return PartialView();
        }

        public PartialViewResult Partial_Grid(string GridId = "grid",string UiGrid = "gridOptions", bool RowSelection = true, bool EnablePagination = true)
        {
            ViewBag.GridId = GridId;
            ViewBag.UiGrid = UiGrid;
            ViewBag.RowSelection = RowSelection;
            ViewBag.EnablePagination = EnablePagination;
            return PartialView();
        }
    }
}