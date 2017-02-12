using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Afaqy_Store.DataLayer;
using GenericApiController.Utilities;

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

        public override IHttpActionResult GetView(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiSIMCardViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }

        public override IHttpActionResult Export(GenericDataFormat data)
        {
            var controller = new ApiSIMCardViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.Export(data);
        }
    }

    public class ApiSIMCardViewController : BaseApiController<SIMCardView>
    {

    }








}
