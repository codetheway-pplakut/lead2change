using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Web.Ui.Models;
using Lead2Change.Services.Answers;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Domain.Models;

namespace Lead2Change.Web.Ui.Controllers
{
    public class AnswerController : Controller
    {
        private IAnswersService AnswersService;
        public AnswerController(IAnswersService answersService)
        {
            this.AnswersService = answersService;
        }
        public async Task<IActionResult> Create(Guid studentID)
        {
            return View(new AnswerViewModel()
            {
                 StudentId = studentID
            });
        }


        [HttpPost]
        public async Task<IActionResult> Register(AnswerViewModel model)
        {
            if (ModelState.IsValid)
            { 
                Answer answer = new Answer()
                {
                    AnswerString = model.AnswerString,
                    Id = model.Id,
                    StudentId = model.StudentId,
                    QuestionId = model.QuestionId,
                };
                var result = await AnswersService.Create(answer);
                return RedirectToAction("Index", new { studentID = answer.StudentId });


            }
            return View("Create", model);
        }
        public async Task<IActionResult> Delete(Guid id, Guid studentID)
        {
            var answer = await AnswersService.GetAnswer(id);
            await AnswersService.Delete(answer);
            return RedirectToAction("Index", new { studentID = answer.StudentId });
        }
    }
}
