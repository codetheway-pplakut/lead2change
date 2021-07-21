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
        public async Task<IActionResult> Edit(Guid id)
        {
            var coach = await _coachService.GetCoach(id);
            CoachViewModel list = new CoachViewModel()
            {
                Id = coach.Id,
                CoachFirstName = coach.CoachFirstName,
                CoachLastName = coach.CoachLastName,
                CoachPhoneNumber = coach.CoachPhoneNumber,
                CoachEmail = coach.CoachEmail,
            };
            return View(list);
        }

        public async Task<IActionResult> Update(Coach model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    Coach list = new Coach()
                    {
                        Id = model.Id,
                        CoachFirstName = model.CoachFirstName,
                        CoachLastName = model.CoachLastName,
                        CoachEmail = model.CoachEmail,
                        CoachPhoneNumber = model.CoachPhoneNumber,

                    };
                    var student = await _coachService.Update(list);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }

}