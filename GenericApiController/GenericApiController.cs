﻿using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Linq.Expressions;
using static GenericApiController.Utilities.GenericDataFormat;
using System.Net.Http;

using System.Data.Entity.Validation;

namespace GenericApiController
{
    public abstract class GenericApiController<T> :  ApiController, IAuthorizationGenericApi<T> where T : class
    {

        protected UnitOfWork<T> repo;

        public GenericAuthorizRoles<T> AuthorizRole { get; set; }

        private Expression<Func<T, bool>> DataConstrains { get; set; }

        public string[] UserRoles { get; set; }
        
        public GenericApiController(DbContext context)
        {
            repo = new UnitOfWork<T>(context);
        }
        public GenericApiController(DbContext context,string[] userRoles)
        {
            UserRoles = userRoles;
            repo = new UnitOfWork<T>(context);
        }

        // GET api/controller
        public virtual IHttpActionResult Get()
        {
            GetAuthorization();
            if(!IsAuthorize(Actions.Get))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized"); 
            }
            
            var result = repo.Repo.Get(filter:DataConstrains);
            return Content(HttpStatusCode.OK, result);
        }
        // GET api/controller/5
        public virtual IHttpActionResult Get(int id)
        {
            GetAuthorization();
            if (!IsAuthorize(Actions.GetById))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            var result = repo.Repo.GetByID(id, filter: DataConstrains);
            return Content(HttpStatusCode.OK, result);
        }
        //POST api/controller
        public virtual IHttpActionResult Post(T value)
        {
            GetAuthorization();
            if (!IsAuthorize(Actions.Post))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            dynamic result;
            result = repo.Repo.Insert(value);
            repo.Save();

            return Content(HttpStatusCode.OK, result);
        }
        // PUT api/controller/5
        public virtual IHttpActionResult Put(int id, [FromBody]T value)
        {
            GetAuthorization();
            if (!IsAuthorize(Actions.Put))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            
            var item = repo.Repo.GetByID(id,filter:DataConstrains);
            if (item != null)
            {
                repo.Repo.Detach(item);
                repo.Repo.Update(value);
                repo.Save();
                return Content(HttpStatusCode.OK, value);
            }
            else
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
           
            
        }
        // DELETE api/controller/5
        public virtual IHttpActionResult Delete(int id)
        {
            GetAuthorization();
            if (!IsAuthorize(Actions.Delete))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            var item = repo.Repo.GetByID(id, filter: DataConstrains);
            if (item != null)
            {
                repo.Repo.Detach(item);
                repo.Repo.Delete(id);
                repo.Save();
                return Content(HttpStatusCode.OK, "Success");
            }
            else
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            
            
        }
        // POST api/controller/get
        [HttpPost]
        public virtual IHttpActionResult Get(GenericDataFormat data)
        {
            GetAuthorization();
            if (!IsAuthorize(Actions.GetByOptions))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            if (data != null)
            {
                dynamic result = GetWithOptions(data);
                return Content(HttpStatusCode.OK, result);
                //if (!(result is IQueryable<T>))
                //{
                //    return 
                //}
                //else
                //{
                //    return Content(HttpStatusCode.OK, (List<object>)result);
                //}

            }
            else
            {
                return Get();
            }
        }
        //POST api/controller/import
        [HttpPost]
        public virtual IHttpActionResult Import(List<T> entities)
        {
            GetAuthorization();
            if (!IsAuthorize(Actions.Import))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            return ImportData(entities);
        }
        //POST api/controller/import/file
        [HttpPost]
        public virtual IHttpActionResult ImportFile()
        {
            GetAuthorization();
            if (!IsAuthorize(Actions.Import))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            return ImportData();
        }
        //POST api/controller/export
        [HttpPost]
        public virtual IHttpActionResult Export(GenericDataFormat data)
        {
            GetAuthorization();
            if (!IsAuthorize(Actions.Export))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }
            List<object> result = null;
            if (data != null)
            {
                var tempResult = GetWithOptions(data);
                if (!(tempResult is List<object>))
                {
                    result = ((IQueryable<T>)tempResult).ToList<object>();
                }
                else
                {
                    result = tempResult;
                }
            }
            else
            {
                result = repo.Repo.Get(filter: DataConstrains).ToList<object>();
            }
            //export data to file
            var exportFilePath = new Export<T>(result).ExportData();
            if (!System.IO.File.Exists(exportFilePath))
            {
                return Content(HttpStatusCode.InternalServerError, "Export process failed");
            }
            else
            {
                HttpResponseMessage responseMessage = null;
                // Serve the file to the client
                responseMessage = Request.CreateResponse(HttpStatusCode.OK);
                responseMessage.Content = new StreamContent(new System.IO.FileStream(exportFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                responseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                responseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                responseMessage.Content.Headers.ContentDisposition.FileName = typeof(T).Name;
                return ResponseMessage(responseMessage);
            }

        }

        private dynamic GetWithOptions(GenericDataFormat data)
        {
            dynamic result = null;
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null;
            List<SortItems> thenByOrders = null;
            dynamic includeProperties = null;
            dynamic includeReference = null;
            Expression<Func<T, bool>> filterExpr = null;
            int? pageNumber = null;
            int? pageSize = null;

            //include reference
            if (data.Includes != null)
            {
                includeProperties = data.Includes.Properties != null ?
                    data.Includes.Properties.Split(',') :
                    null;
                includeReference = data.Includes.References;
            }
            //filters
            filterExpr = Repository<T>.GetFilter(data.Filters);
            // check if there is adata access constrains 
            if (DataConstrains != null)
            {
                //check if there is custom filterations
                if (filterExpr != null)
                {
                    //combine data access constrains to filters
                    filterExpr = filterExpr.AndAlso(DataConstrains);
                }
                else
                {
                    //set filteration as data access constrains
                    filterExpr = DataConstrains;
                }
            }
            //orderby
            if (data.Sorts != null && data.Sorts.Count > 0)
            {
                var sortItem = data.Sorts[0];
                orderBy = Repository<T>.GetOrderBy(sortItem.Property, sortItem.SortType);
                if (data.Sorts.Count > 1)
                {
                    thenByOrders = data.Sorts.Skip(1).ToList();
                }
            }
            //paging
            if (data.Paging != null)
            {
                pageNumber = data.Paging.PageNumber;
                pageSize = data.Paging.PageSize;
                if (orderBy == null)
                {
                    IEnumerable<string> keyNames = Repository<T>.GetPKColumns(repo.Repo._context);
                    orderBy = Repository<T>.GetOrderBy(keyNames.ElementAt(0), GenericDataFormat.SortType.Asc);
                }
            }

            //order by
            //List<Expression<Func<T,object>>> thenSortsSelectors = null;
            //if (data.Sorts != null && data.Sorts.Count > 0)
            //{
            //    orderBy = Repository<T>.GetOrderBy(data.Sorts[0].Property, data.Sorts[0].SortType);
            //    thenSortsSelectors = new List<Expression<Func<T, object>>>();
            //    foreach (var sort in data.Sorts.Skip(1))
            //    {
            //        thenSortsSelectors.Add(Repository<T>.GetSelector(sort.Property));
            //    }
            //}
            //order by
            //if (data.Sorts != null && data.Sorts.Count > 0)
            //{
            //    orderBy = Repository<T>.GetOrderBy(data.Sorts[0].Property, data.Sorts[0].SortType);
            //    thenSorts = new List<Func<IQueryable<T>, IOrderedQueryable<T>>>();
            //    foreach (var sort in data.Sorts.Skip(1))
            //    {
            //        thenSorts.Add(Repository<T>.GetOrderBy(sort.Property, sort.SortType));
            //    }
            //}

            result = repo.Repo.GetWithOptions(includeProperties: includeProperties, includeReferences: includeReference,
                filter: filterExpr, orderBy: orderBy, thenByOrders: thenByOrders,
                pageNumber: pageNumber, pageSize: pageSize);

            return result;
        }
        private IHttpActionResult ImportData(List<T> entities = null)
        {
            var httpRequest = System.Web.HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                // Check if the request contains multipart/form-data.
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    entities = new Import<T>(postedFile, repo.Repo._context).ImportData().ToList<T>();
                }
            }
            //save List to repository
            if (entities != null)
            {
                try
                {
                    Save(entities);
                    return Content(HttpStatusCode.OK, "Success");
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMsg = string.Empty;
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        errorMsg += string.Format("Entity of type {0} in state {1} has the following validation errors: ",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State) + Environment.NewLine;
                        foreach (var ve in eve.ValidationErrors)
                        {
                            errorMsg += string.Format("- Property: {0}, Error: {1} ",
                                ve.PropertyName, ve.ErrorMessage) + Environment.NewLine;
                        }
                    }
                    return Content(HttpStatusCode.InternalServerError, errorMsg);
                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.InternalServerError, "Failed");
                }
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, "Failed");
            }

        }
        private bool Save(List<T> values)
        {
            List<T> result = new List<T>();
            foreach (var value in values)
            {
                result.Add(repo.Repo.Insert(value));
            }
            repo.Save();
            return true;
        }
        private IHttpActionResult SaveGroup(List<T> values)
        {
            List<T> result = new List<T>();
            foreach (var value in values)
            {
                result.Add(repo.Repo.Insert(value));
            }
            repo.Save();
            return Content(HttpStatusCode.OK, result);
        }
        private bool IsAuthorize(Actions action)
        {
            //check if there is authorization roles defined in contorller
            if (AuthorizRole != null)
            {
                //get roles defined for specific action
                var GetActionRole = AuthorizRole.ActionRoles.Find(x => x.Action == action);

                //check if there are premission roles for the action
                if (GetActionRole != null)
                {
                    //check if user has roles
                    if (UserRoles != null)
                    {
                        //get the matched roles of action roles and user roles
                        var ActionPermisionRoles = GetActionRole.Roles.Split(',').Intersect(UserRoles, new CustStringComparer() );
                        //check if there is mateched roles the user have premssion for this action 
                        //and if not then user is unauthroize to this request
                        if (!ActionPermisionRoles.Any())
                        {
                            //user is unauthorize then throw unauthorize exception
                            return false;
                        }

                        // if user is authorized then check about data access security
                        if (AuthorizRole.DataAccess != null)
                        {
                            // get all roles of specific action 
                            var allDataRoles = AuthorizRole.DataAccess.FindAll(x => x.Action == action).SelectMany(x => x.Roles.Split(','));

                            //var uniqueRole = ActionPermisionRoles.Except(allDataRoles, new CustStringComparer()).ToList();
                            //check user has role not exist in all action data access roles then user can access data without any condition
                            //if not then get the constrain filter for data access
                            if (ActionPermisionRoles.Except(allDataRoles, new CustStringComparer()).Any())
                            {
                                //set data constrains to null
                                DataConstrains = null;
                                //user can access data without any condition
                                return true;
                            }
                           else
                            {
                                //get first role for matched action data access role with user permission roles
                                var dataRole = allDataRoles.Intersect(ActionPermisionRoles).First();
                                //get all data access constrains on the role
                                var constrains = AuthorizRole.DataAccess.FindAll(x => x.Action == action && x.Roles.Split(',').Any(y=> y.Equals(dataRole,StringComparison.OrdinalIgnoreCase)));
                                //get the first constrain and store it to apply in data operation
                                DataConstrains = constrains.First().ExpressionFunc;

                            }
                          
                        }
                    }
                    else
                    {
                        return false;
                    }
                    
                }
            }

            return true;
        }
        [NonAction]
        public static bool ValueEquals(object x, object y)
        {
            if (x == null && y == null)
                return true;
            if (x.GetType() != y.GetType())
                return false;
            return x == y;
        }
        [NonAction]
        public virtual GenericAuthorizRoles<T> GetAuthorization()
        {
            //set it to defaul value
            return null;
        }

        /// <summary>
        /// Grant roles to access specific action
        /// </summary>
        /// <param name="action"> Enum (ex: Get,Put,Delete) </param>
        /// <param name="roles"> string of one or more role seperated by ',' as string </param>
        private void SetActionRoles(Actions action, string roles)
        {
            if(AuthorizRole == null)
            {
                AuthorizRole = new GenericAuthorizRoles<T>();
            }
            if(AuthorizRole.ActionRoles == null)
            {
                AuthorizRole.ActionRoles = new List<GenericActionRoles>();
            }
            var actionRoles = AuthorizRole.ActionRoles.Find(x => x.Action == action);
            if(actionRoles == null)
            {
                actionRoles = new GenericActionRoles() { Action = action, Roles = roles };
                AuthorizRole.ActionRoles.Add(actionRoles);
            }
            else
            {
                actionRoles.Roles = roles;
            }
        }

        [NonAction]
        /// <summary>
        /// Grant roles to access Get action
        /// </summary>
        /// <param name="roles"> string of one or more role seperated by ',' as string </param>
        public void SetGetActionRoles(string roles)
        {
            SetActionRoles(Actions.Get, roles);

        }

        [NonAction]
        /// <summary>
        /// Grant roles to access GetById action
        /// </summary>
        /// <param name="roles"> string of one or more role seperated by ',' as string </param>
        public void SetGetByIdActionRoles(string roles)
        {
            SetActionRoles(Actions.GetById, roles);
        }

        [NonAction]
        /// <summary>
        /// Grant roles to access GetByOptions action
        /// </summary>
        /// <param name="roles"> string of one or more role seperated by ',' as string </param>
        public void SetGetByOptionsActionRoles(string roles)
        {
            SetActionRoles(Actions.GetByOptions, roles);
        }

        [NonAction]
        /// <summary>
        /// Grant roles to access Post action
        /// </summary>
        /// <param name="roles"> string of one or more role seperated by ',' as string </param>
        public void SetPostActionRoles(string roles)
        {
            SetActionRoles(Actions.Post, roles);
        }

        [NonAction]
        /// <summary>
        /// Grant roles to access Put action
        /// </summary>
        /// <param name="roles"> string of one or more role seperated by ',' as string </param>
        public void SetPutActionRoles(string roles)
        {
            SetActionRoles(Actions.Put, roles);
        }

        [NonAction]
        /// <summary>
        /// Grant roles to access Delete action
        /// </summary>
        /// <param name="roles"> string of one or more role seperated by ',' </param>
        public void SetDeleteActionRoles(string roles)
        {
            SetActionRoles(Actions.Delete, roles);
        }

        [NonAction]
        /// <summary>
        /// Add constrain in roles when request action
        /// </summary>
        /// <param name="action"> Enum of actions</param>
        /// <param name="roles"> string of one or more role seperated by ',' </param>
        /// <param name="ConstrainsExpression"></param>
        public void AddActionDataConstrains(Actions action, string roles, Expression<Func<T, bool>> ConstrainsExpression)
        {
            if (AuthorizRole == null)
            {
                AuthorizRole = new GenericAuthorizRoles<T>();
            }

            if(AuthorizRole.DataAccess == null)
            {
                AuthorizRole.DataAccess = new List<GenericDataAccess<T>>();
            }
            GenericDataAccess<T> dataAccessRole = new GenericDataAccess<T>() { Action = action, Roles = roles, ExpressionFunc = ConstrainsExpression };
            AuthorizRole.DataAccess.Add(dataAccessRole);
        }
        
    }
}
