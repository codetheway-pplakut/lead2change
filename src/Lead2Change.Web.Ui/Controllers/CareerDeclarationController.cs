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

        public CareerDeclarationController(IIdentityService identityService, ICareerDeclarationService careerDeclarationService) : base(identityService)
        {
            _service = careerDeclarationService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetCareerDeclarations());
        }

        public async Task<IActionResult> Create()
        {
            return View(new CareerDeclarationViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(CareerDeclarationViewModel model)
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
                await _service.Create(careerDeclaration);
                return RedirectToAction("Index");
            }
            return View(model);
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
