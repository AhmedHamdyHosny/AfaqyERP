using Afaqy_Store.Models;
using Classes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Controllers
{
    public class BaseController<TDBModel, TViewModel, TIndexViewModel, TDetailsViewModel, TCreateBindModel, TEditBindModel, TEditModel, TImportModel, TModel_TDBModel, TModel_TViewModel> : GenericContoller<TDBModel, TViewModel, TIndexViewModel, TDetailsViewModel, TCreateBindModel, TEditBindModel, TEditModel, TImportModel, TModel_TDBModel, TModel_TViewModel>
    {
        public UserViewModel User = new UserViewModel().GetUserFromSession();

        //for test
        //public UserViewModel User = new UserModel<UserViewModel>().Get(1);
    }
}