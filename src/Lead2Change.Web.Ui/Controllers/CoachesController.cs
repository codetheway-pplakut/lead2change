using Lead2Change.Domain.ViewModels;
using Lead2Change.Services.Identity;
using Lead2Change.Services.Coaches;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Lead2Change.Domain.Constants;

namespace Lead2Change.Web.Ui.Controllers
{
    public class CoachesController : _BaseController
    {
        ICoachService _coachService;

        public CoachesController(IUserService identityService, ICoachService coachService, RoleManager<AspNetRoles> roleManager, UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager) : base(identityService, roleManager, userManager, signInManager)
        {
            _coachService = coachService;
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("401: Unauthorized");
            }

            if (User.IsInRole(StringConstants.RoleNameCoach))
            {
                return Error("403: You are not authorized to view this page.");
            }

            var coach = await _coachService.GetCoach(id);

            if (id == Guid.Empty || coach == null)
            {
                return Error("400: Bad Request");
            }

            await _coachService.Delete(coach);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("401: Unauthorized");
            }

            if (User.IsInRole(StringConstants.RoleNameCoach))
            {
                return Error("403: You are not authorized to view this page.");
            }

            return View(new CoachViewModel());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var coachescontainer = await _coachService.GetCoach(id);

            if (id == Guid.Empty || coachescontainer == null)
            {
                return Error("400: Bad Request");
            }

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
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("401: Unauthorized");
            }

            if (User.IsInRole(StringConstants.RoleNameCoach))
            {
                return Error("403: You are not authorized to view this page.");
            }

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
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("401: Unauthorized");
            }

            if (User.IsInRole(StringConstants.RoleNameCoach))
            {
                return Error("403: You are not authorized to view this page.");
            }

            return View(await _coachService.GetCoaches());
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("401: Unauthorized");
            }

            if (User.IsInRole(StringConstants.RoleNameCoach))
            {
                return Error("403: You are not authorized to view this page.");
            }

            var coach = await _coachService.GetCoach(id);

            if (id == Guid.Empty || coach == null)
            {
                return Error("400: Bad Request");
            }

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
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("401: Unauthorized");
            }

            if (User.IsInRole(StringConstants.RoleNameCoach))
            {
                return Error("403: You are not authorized to view this page.");
            }

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