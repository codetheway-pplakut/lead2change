using Lead2Change.Domain.ViewModels;
using Lead2Change.Services.Identity;
using Lead2Change.Services.Coaches;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lead2Change.Web.Ui.Controllers
{
    public class CoachesController : _BaseController
    {
        ICoachService _coachService;
        public CoachesController(IIdentityService identityService, ICoachService coachService) : base(identityService)
        {
            _coachService = coachService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View(new CoachViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CoachViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    Coach coach = new Coach()
                    {
                        Students = new List<Student>(),
                        CoachFirstName = model.CoachFirstName,
                        CoachLastName = model.CoachLastName,
                        CoachEmail = model.CoachEmail,
                        CoachPhoneNumber = model.CoachPhoneNumber
                    };
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
