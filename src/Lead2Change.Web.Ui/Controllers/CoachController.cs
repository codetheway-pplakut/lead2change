using Microsoft.AspNetCore.Mvc;
using Lead2Change.Services.Coaches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Domain.Models;
using Lead2Change.Services.Identity;

namespace Lead2Change.Web.Ui.Controllers
{
    public class CoachController : _BaseController
    {
        ICoachService _coachService; 
        public CoachController(IIdentityService identityService, ICoachService coachService) : base(identityService)
        {
            _coachService = coachService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _coachService.GetCoaches());
        }
    }
}
