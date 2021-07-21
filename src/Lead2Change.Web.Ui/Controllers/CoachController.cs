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
    public class CoachController : Controller
    {
        private ICoachService _coachService;
        public IActionResult Index()
        {
            return View();
        }
        public CoachController(IIdentityService identityService, ICoachService coachService) : base(identityService)
        {
            _coachService = coachService;
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _coachService.GetStudent(id);
            RegistrationViewModel list = new RegistrationViewModel()
            {
                Id = student.Id,
                //General Student Info
                StudentFirstName = student.StudentFirstName,
                StudentLastName = student.StudentLastName,
                StudentDateOfBirth = student.StudentDateOfBirth,
                StudentAddress = student.StudentAddress,
                
            };
            return View(list);
        }

        public async Task<IActionResult> Update(Student model)
        {
            if (ModelState.IsValid)
            {
                if (model.StudentFirstName.Length > 0)
                {
                    Student list = new Student()
                    {
                        Id = model.Id,
                        //General Student Info
                        StudentFirstName = model.StudentFirstName,
                        StudentLastName = model.StudentLastName,
                        StudentDateOfBirth = model.StudentDateOfBirth,
                        StudentAddress = model.StudentAddress,
                        StudentApartmentNumber = model.StudentApartmentNumber,
                        StudentCity = model.StudentCity,
                        
                    };
                    var student = await _studentService.Update(list);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }

}
