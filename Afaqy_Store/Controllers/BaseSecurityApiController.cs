﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Afaqy_Store.Controllers
{
    public class BaseSecurityApiController<T> : GenericApiController.GenericApiController<T> where T : class
    {
        public BaseSecurityApiController() : base(new Security.DataLayer.SecurityEntities())
        {

        }
    }
}