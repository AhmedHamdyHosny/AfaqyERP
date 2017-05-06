using Afaqy_Store.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Afaqy_Store.Controllers
{
    public class ApiServerUnitController : BaseApiController<ServerUnit>
    {
        public override IHttpActionResult Import(List<ServerUnit> entities)
        {
            try
            {
                int count = 0;
                do
                {
                    var items = entities.Skip(count).Take(100);
                    count += 100;
                    // This is optional
                    using (AfaqyStoreEntities context = new AfaqyStoreEntities())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false;
                        //context.Configuration.ValidateOnSaveEnabled = false;
                        context.Set<ServerUnit>().AddRange(items);
                        context.SaveChanges();
                        context.Dispose();
                    }

                } while (count < entities.Count);
                return Content(HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            
        }
        [HttpGet]
        public IHttpActionResult Clear()
        {
            
            GetAuthorization();
            if (!IsAuthorize(GenericApiController.Utilities.Actions.Clear))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            repo.Repo.ExceuteSql("TRUNCATE TABLE [afqy].[ServerUnit]");
            return Content(HttpStatusCode.OK, "Success");
           
        }
    }
}
