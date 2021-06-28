using Lead2Change.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lead2Change.Web.Ui.Controllers
{
    public class _BaseController : Controller
    {
        IIdentityService _identityService;

        public _BaseController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
    }
}
