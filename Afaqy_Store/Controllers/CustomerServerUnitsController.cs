using Afaqy_Store.DataLayer;
using Afaqy_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Controllers
{
    public class CustomerServerUnitsController : BaseController<CustomerServerUnitsCountView, CustomerServerUnitsCountView, CustomerServerUnitsCountIndexViewModel, CustomerServerUnitsDetailsViewModel, CustomerServerUnitsCountView, CustomerServerUnitsCountView, CustomerServerUnitsCountView, CustomerServerUnitsCountView, CustomerServerUnitsCountModel<CustomerServerUnitsCountView>, CustomerServerUnitsCountModel<CustomerServerUnitsCountViewModel>>
    {
       
        public override ActionResult Details(object id)
        {
            var model = id;
            return View(model);
        }

        [HttpPost]
        public JsonResult GetCustomerServerUnitsDetailsView(string customerId, GenericApiController.Utilities.GenericDataFormat options)
        {
            var model = new GenericApiController.Utilities.PaginationResult<CustomerServerUnitsDetailsViewModel>();
            if (options.Sorts != null && options.Sorts.Count > 1)
            {
                options.Sorts = options.Sorts.Where(x => x.Priority == 1).ToList();
            }
            if(options.Filters == null)
            {
                options.Filters = new List<GenericApiController.Utilities.GenericDataFormat.FilterItems>();
            }
            options.Filters.Add(new GenericApiController.Utilities.GenericDataFormat.FilterItems() { Property = "CustomerId", Operation = GenericApiController.Utilities.GenericDataFormat.FilterOperations.Equal, Value = customerId });
            model = new CustomerServerUnitsModel<CustomerServerUnitsDetailsViewModel>().GetView<CustomerServerUnitsDetailsViewModel>(options);
            return Json(model);
        }
        
    }
}