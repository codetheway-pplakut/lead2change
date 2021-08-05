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
using Lead2Change.Services.Students;
using Microsoft.AspNetCore.Authorization;

namespace Lead2Change.Web.Ui.Controllers
{
    [Authorize(Roles = StringConstants.RoleNameAdmin + "," + StringConstants.RoleNameCoach)]
    public class CoachesController : _BaseController
    {
        ICoachService _coachService;
        IStudentService _studentService;

        public CoachesController(IUserService identityService, ICoachService coachService, IStudentService studentService, RoleManager<AspNetRoles> roleManager, UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager) : base(identityService, roleManager, userManager, signInManager)
        {
            _coachService = coachService;
            _studentService = studentService;
        }

        [Authorize(Roles = StringConstants.RoleNameAdmin)]
        public async Task<IActionResult> Index()
        {
            return View(await _coachService.GetCoaches());
        }
        public async Task<IActionResult> AssignedIndex()
        {
            return View(await _coachService.GetCoaches());
        }


        [Authorize(Roles = StringConstants.RoleNameAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var coach = await _coachService.GetCoach(id);
            if (id == Guid.Empty || coach == null)
            {
                return Error("400: Bad Request");
            }

            coach.Students = await _studentService.GetCoachStudents(id);
            foreach (var item in coach.Students)
            {
                item.CoachId = null;
            }

            await _coachService.Delete(coach);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var coachescontainer = await _coachService.GetCoach(id);

            if (id == Guid.Empty || coachescontainer == null)
            {
                return Error("400: Bad Request");
            }

            CoachViewModel model = new CoachViewModel();

            if (coachescontainer != null)
            {
                model.Id = coachescontainer.Id;
                model.CoachFirstName = coachescontainer.CoachFirstName;
                model.CoachLastName = coachescontainer.CoachLastName;
                model.CoachEmail = coachescontainer.CoachEmail;
                model.CoachPhoneNumber = coachescontainer.CoachPhoneNumber;
                model.Students = new List<Student>();
                model.Active = coachescontainer.Active;
            }

            model.Students = await _studentService.GetCoachStudents(id);

            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
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
                Active = coach.Active
            };
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var user = await UserManager.GetUserAsync(User);
            if (User.IsInRole(StringConstants.RoleNameCoach) && user.AssociatedId != Guid.Empty)
            {
                return Error("403: You are not authorized to view this page.");
            }

            return View(new CoachViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CoachViewModel model)
        {
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("401: Unauthorized");
            }

            var user = await UserManager.GetUserAsync(User);
            if (User.IsInRole(StringConstants.RoleNameCoach) && user.AssociatedId != Guid.Empty)
            {
                return Error("403: You are not authorized to view this page.");
            }

            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    Coach coach = new Coach()
                    {
                        CoachFirstName = model.CoachFirstName,
                        CoachLastName = model.CoachLastName,
                        CoachEmail = model.CoachEmail,
                        CoachPhoneNumber = model.CoachPhoneNumber,
                        Students = new List<Student>(),
                        Active = true
                    };
                    var result = await _coachService.Create(coach);
                    if (User.IsInRole(StringConstants.RoleNameCoach))
                    {
                        user.AssociatedId = result.Id;
                        await _coachService.Update(result);
                        return RedirectToAction("Index", "Students");
                    }
                }

                return RedirectToAction("Index");
            }

            return View(model);
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
                        Active = model.Active
                    };
                    var student = await _coachService.Update(list);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // TODO: Need to add "AddStudent" method to Service
        // Change StudentService to change the CoachId
        public async Task<IActionResult> AssignStudent(Guid studentId, Guid coachId)
        {
            var coach = await _coachService.GetCoach(coachId); //check coach exists
            var student = await _studentService.GetStudent(studentId);

            student.CoachId = coachId;

            var student1 = await _studentService.Update(student);
            return RedirectToAction("AssignStudentIndex", new { id = coach.Id });
        }

        public async Task<IActionResult> AssignStudentIndex(Guid id)
        {
            Coach tempCoach = await _coachService.GetCoach(id);
            AssignStudentViewModel assignStudentViewModel = new AssignStudentViewModel()
            {
                UnassignedStudents = await _studentService.GetUnassignedStudents(),
                CurrentCoach = tempCoach
            };
            return View(assignStudentViewModel);
        }

        public async Task<IActionResult> UnassignStudent(Guid studentId)
        {
            var student = await _studentService.GetStudent(studentId);
            var tempCoachId = student.CoachId;
            student.CoachId = null;
            var student1 = await _studentService.Update(student);
            return RedirectToAction("CoachesStudents", new { id = tempCoachId });
        }
        public async Task<IActionResult> CoachesStudents(Guid id)
        {
            var user = await UserManager.GetUserAsync(User);
            if (id == Guid.Empty) id = user.AssociatedId;

            CoachViewModel model = new CoachViewModel();
            var coachescontainer = await _coachService.GetCoach(id); //check for coachescontainer=null, and all other fields

            if (id == Guid.Empty || coachescontainer == null)
            {
                return Error("400: Bad Request");
            }

            model.Id = coachescontainer.Id;
            model.CoachFirstName = coachescontainer.CoachFirstName;
            model.CoachLastName = coachescontainer.CoachLastName;
            model.CoachEmail = coachescontainer.CoachEmail;
            model.CoachPhoneNumber = coachescontainer.CoachPhoneNumber;
            model.Students = new List<Student>();
            model.Active = coachescontainer.Active;


            model.Students = await _studentService.GetCoachStudents(id);


            return View(model);
        }
        public async Task<IActionResult> InactiveCoach(Guid coachId)
        {
            var coach = await _coachService.GetCoach(coachId);
            coach.Active = false;
            var coach1 = await _coachService.Update(coach);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ActiveCoach(Guid coachId)
        {
            var coach = await _coachService.GetCoach(coachId);
            coach.Active = true;
            var coach1 = await _coachService.Update(coach);
            return RedirectToAction("Index");
        }
    }
}