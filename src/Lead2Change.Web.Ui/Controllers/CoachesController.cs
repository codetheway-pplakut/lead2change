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

        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await _coachService.GetCoach(id);
            await _coachService.Delete(student);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            return View(new CoachViewModel());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var coachescontainer = await _coachService.GetCoach(id);
            CoachViewModel a = new CoachViewModel()
            {
                Id = coachescontainer.Id,
                CoachFirstName = coachescontainer.CoachFirstName,
                CoachLastName = coachescontainer.CoachLastName,
                CoachEmail = coachescontainer.CoachEmail,
                CoachPhoneNumber = coachescontainer.CoachPhoneNumber
            };
            return View(a);
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
                    var abc = await _coachService.Create(coach);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _coachService.GetCoaches());
        }
    }
}