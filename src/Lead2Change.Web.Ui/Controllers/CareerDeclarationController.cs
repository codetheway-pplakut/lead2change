using Lead2Change.Domain.Models;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Services.CareerDeclarationService;
using Lead2Change.Services.Identity;
using Lead2Change.Services.Students;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lead2Change.Web.Ui.Controllers
{
    public class CareerDeclarationController : _BaseController
    {
        private ICareerDeclarationService _service;
        private IStudentService _studentService;

        public CareerDeclarationController(IIdentityService identityService, ICareerDeclarationService careerDeclarationService, IStudentService studentService) : base(identityService)
        {
            _service = careerDeclarationService;
            _studentService = studentService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetCareerDeclarations());
        }

        public async Task<IActionResult> Create(Guid studentId)
        {
            CareerDeclarationViewModel careerDeclarationViewModel = new CareerDeclarationViewModel()
            {
                StudentId = studentId,
            };
            return View(careerDeclarationViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Register(CareerDeclarationViewModel model)
        {
            var student = await _studentService.GetStudent(model.StudentId);
            if (ModelState.IsValid && student.CareerDeclarationId == Guid.Empty)
            {
                CareerDeclaration careerDeclaration = new CareerDeclaration()
                {
                    Id = model.Id,
                    StudentId = model.StudentId,
                    CollegeBound = model.CollegeBound,
                    CareerCluster = model.CareerCluster,
                    SpecificCareer = model.SpecificCareer,
                    TechnicalCollegeBound = model.TechnicalCollegeBound,
                };
                careerDeclaration = await _service.Create(careerDeclaration);
                student.CareerDeclarationId = careerDeclaration.Id;
                await _studentService.Update(student);
            }
            return RedirectToAction("Index");
            // return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var careerDeclaration = await _service.GetCareerDeclaration(id);
            CareerDeclarationViewModel careerDeclarationViewModel = new CareerDeclarationViewModel()
            {
                Id = careerDeclaration.Id,
                StudentId = careerDeclaration.StudentId,
                CollegeBound = careerDeclaration.CollegeBound,
                CareerCluster = careerDeclaration.CareerCluster,
                SpecificCareer = careerDeclaration.SpecificCareer,
                TechnicalCollegeBound = careerDeclaration.TechnicalCollegeBound,
            };
            return View(careerDeclarationViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CareerDeclarationViewModel model)
        {
            if (ModelState.IsValid)
            {
                CareerDeclaration careerDeclaration = new CareerDeclaration()
                {
                    Id = model.Id,
                    StudentId = model.StudentId,
                    CollegeBound = model.CollegeBound,
                    CareerCluster = model.CareerCluster,
                    SpecificCareer = model.SpecificCareer,
                    TechnicalCollegeBound = model.TechnicalCollegeBound,
                };
                await _service.Update(careerDeclaration);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var careerDeclaration = await _service.GetCareerDeclaration(id);
            CareerDeclarationViewModel careerDeclarationViewModel = new CareerDeclarationViewModel()
            {
                Id = careerDeclaration.Id,
                StudentId = careerDeclaration.StudentId,
                CollegeBound = careerDeclaration.CollegeBound,
                CareerCluster = careerDeclaration.CareerCluster,
                SpecificCareer = careerDeclaration.SpecificCareer,
                TechnicalCollegeBound = careerDeclaration.TechnicalCollegeBound,
            };
            return View(careerDeclarationViewModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var careerDeclaration = await _service.GetCareerDeclaration(id);
            await _service.Delete(careerDeclaration);
            return RedirectToAction("Index");
        }

    }
}
