using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Afaqy_Store.DataLayer;

namespace Afaqy_Store.Controllers
{
    public class ApiSIMCardController : BaseApiController<SIMCard>
    {

        //public override IHttpActionResult Import(List<SIMCard> entities)
        //{
        //    List<SIMCardStatusHistory> simStatusHistory = entities.SelectMany(x => x.SIMCardStatusHistory).ToList();
        //    base.Import(entities);
        //    return new ApiSIMCardStatusHistoryController().Import(simStatusHistory);
        //}
    }


}
