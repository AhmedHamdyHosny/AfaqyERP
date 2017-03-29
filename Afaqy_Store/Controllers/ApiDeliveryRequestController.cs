﻿using Afaqy_Store.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace Afaqy_Store.Controllers
{
    public class ApiDeliveryRequestController : BaseApiController<DeliveryRequest>
    {
        public override IHttpActionResult GetView(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiDeliveryRequestViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }

        public override IHttpActionResult Export(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiDeliveryRequestViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.Export(data);
        }

        public override IHttpActionResult Put(int id, [FromBody] DeliveryRequest value)
        {
            GetAuthorization();
            if (!IsAuthorize(GenericApiController.Utilities.Actions.Put))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            //var TEntityId = Repository<T>.GetId(id, repo.Repo._context);
            var item = repo.Repo.GetByID(id, filter: GetDataConstrains());
            if (item != null)
            {
                using (var context = new AfaqyStoreEntities())
                {
                    var originalItem = context.DeliveryRequest.Include(j => j.DeliveryRequestDetails)
                        .Include(j => j.DeliveryRequestTechnician)
                        .Single(j => j.DeliveryRequestId == value.DeliveryRequestId);

                    // Update scalar/complex properties
                    context.Entry(originalItem).CurrentValues.SetValues(value);

                    // Update references
                    // Update DeliveryRequestDetails
                    if(value.DeliveryRequestDetails != null)
                    {
                        foreach (var childItem in value.DeliveryRequestDetails)
                        {
                            var originalDetailsItem = originalItem.DeliveryRequestDetails
                                .Where(c => c.DeliveryRequestDetailsId == childItem.DeliveryRequestDetailsId && c.DeliveryRequestDetailsId != 0)
                                .SingleOrDefault();
                            // Is original child item with same ID in DB?
                            if (originalDetailsItem != null)
                            {
                                context.Entry(originalDetailsItem).CurrentValues.SetValues(childItem);
                            }
                            else
                            {
                                childItem.DeliveryRequestDetailsId = 0;
                                originalItem.DeliveryRequestDetails.Add(childItem);
                            }
                        }

                        // Don't consider the child items we have just added above.
                        // (We need to make a copy of the list by using .ToList() because
                        // _dbContext.ChildItems.Remove in this loop does not only delete
                        // from the context but also from the child collection. Without making
                        // the copy we would modify the collection we are just interating
                        // through - which is forbidden and would lead to an exception.)
                        foreach (var originalChildItem in
                                     originalItem.DeliveryRequestDetails.Where(c => c.DeliveryRequestDetailsId != 0).ToList())
                        {
                            // Are there child items in the DB which are NOT in the
                            // new child item collection anymore?
                            if (!value.DeliveryRequestDetails.Any(c => c.DeliveryRequestDetailsId == originalChildItem.DeliveryRequestDetailsId))
                                // Yes -> It's a deleted child item -> Delete
                                context.DeliveryRequestDetails.Remove(originalChildItem);
                        }
                    }
                    

                    //update DeliveryRequestTechnician
                    if(value.DeliveryRequestTechnician != null)
                    {
                        foreach (var childItem in value.DeliveryRequestTechnician)
                        {
                            var originalDetailsItem = originalItem.DeliveryRequestTechnician
                                .Where(c => c.DeliveryRequestId == childItem.DeliveryRequestId && c.EmployeeId == childItem.EmployeeId)
                                .SingleOrDefault();
                            // Is original child item with same ID in DB?
                            if (originalDetailsItem != null)
                            {
                                childItem.DeliveryRequestTechnicalId = originalDetailsItem.DeliveryRequestTechnicalId;
                                context.Entry(originalDetailsItem).CurrentValues.SetValues(childItem);
                            }
                            else
                            {
                                childItem.DeliveryRequestTechnicalId = 0;
                                originalItem.DeliveryRequestTechnician.Add(childItem);
                            }
                        }

                        foreach (var originalChildItem in
                                     originalItem.DeliveryRequestTechnician.Where(c => c.DeliveryRequestTechnicalId != 0).ToList())
                        {
                            // Are there child items in the DB which are NOT in the
                            // new child item collection anymore?
                            if (!value.DeliveryRequestTechnician.Any(c => c.DeliveryRequestTechnicalId == originalChildItem.DeliveryRequestTechnicalId))
                                // Yes -> It's a deleted child item -> Delete
                                context.DeliveryRequestTechnician.Remove(originalChildItem);
                        }
                    }
                    context.SaveChanges();
                }
                return Content(HttpStatusCode.OK, value);
            }
            else
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            
        }
    }
    public class ApiDeliveryRequestViewController : BaseApiController<DeliveryRequestView>
    {
    }
}
