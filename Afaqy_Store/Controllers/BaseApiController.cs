using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Afaqy_Store.DataLayer;

namespace Afaqy_Store.Controllers
{
    public class BaseApiController<T> : GenericApiController.GenericApiController<T> where T : class
    {
        public BaseApiController() : base(new AfaqyStoreEntities())
        {

        }
    }
}