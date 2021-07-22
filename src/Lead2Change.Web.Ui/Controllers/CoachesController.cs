using Lead2Change.Domain.ViewModels;
using Lead2Change.Services.Identity;
using Lead2Change.Services.Coaches;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Services.Students;

namespace Lead2Change.Web.Ui.Controllers
{
    public class CoachesController : _BaseController
    {
        ICoachService _coachService;
        IStudentService _studentService;
        public CoachesController(IIdentityService identityService, ICoachService coachService, IStudentService studentService) : base(identityService)
        {
            _coachService = coachService;
            _studentService = studentService;
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
        public async Task<IActionResult> AssignStudent(Guid studentId, Guid coachId)
        {
            var coach = await _coachService.GetCoach(coachId);
            var student = await _studentService.GetStudent(studentId);
            coach.Students.Add(student);
            var coach1 = await _coachService.Update(coach);
            return RedirectToAction("Index");
        }
        //Need to add "AddStudent" method to Service
        //Change above method^^^
        //Change StudentService to change the CoachId
        public async Task<IActionResult> AssignStudentIndex(Guid coachId)
        {
            AssignStudentViewModel assignStudentViewModel = new AssignStudentViewModel()
            {
                UnassignedStudents = await _studentService.GetUnassignedStudents(),
                CurrentCoach = await _coachService.GetCoach(coachId)
            };
            return View(assignStudentViewModel);
        }
    }

}