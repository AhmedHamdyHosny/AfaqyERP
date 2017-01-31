using Afaqy_Store.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Afaqy_Store.Controllers
{
    public class ApiBranchController : BaseApiController<Branch>
    {
        public ApiBranchController()
        {
        }

        [NonAction]
        public override IHttpActionResult Delete(int id)
        {
            return null;
        }

        [NonAction]
        public override IHttpActionResult Delete(int[] ids)
        {
            return null;
        }

        [NonAction]
        public override IHttpActionResult Deactive(int id)
        {
            return null;
        }

        [NonAction]
        public override IHttpActionResult Deactive(int[] ids)
        {
            return null;
        }
    }
}
