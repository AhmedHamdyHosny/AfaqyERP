﻿using Classes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Controllers
{
    public class BaseController<TDBModel, TViewModel, TCreateBindModel, TEditBindModel, TEditModel, TModel_TDBModel, TModel_TViewModel> : GenericContoller<TDBModel, TViewModel, TCreateBindModel, TEditBindModel, TEditModel, TModel_TDBModel, TModel_TViewModel>
    {
    }
}