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
            return View(new CareerDeclaration());
        }
        [HttpPost]
        public async Task<IActionResult> Register(CareerDeclaration model)
        {
            if (ModelState.IsValid)
            {
                var careerDeclaration = await _service.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var careerDeclaration = await _service.GetCareerDeclaration(id);
            return View(careerDeclaration);
        }
        [HttpPost]
        public async Task<IActionResult> UpDate(CareerDeclaration model)
        {
            if (ModelState.IsValid)
            {
                var careerDeclaration = await _service.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var careerDeclaration = await _service.GetCareerDeclaration(id);
            return View(careerDeclaration);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var careerDeclaration = await _service.GetCareerDeclaration(id);
            await _service.Delete(careerDeclaration);
            return RedirectToAction("Index");
        }

    }
}
