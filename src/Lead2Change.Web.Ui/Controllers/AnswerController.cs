using Lead2Change.Services.Answers;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Services.Answers;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Domain.Models;
using Lead2Change.Web.Ui.Models;

namespace Lead2Change.Web.Ui.Controllers
{
    public class AnswerController : Controller
    {
        private IAnswersService AnswersService;
        public AnswerController(IAnswersService answersService)
        {
            this.AnswersService = answersService;
        }
        public IActionResult Index()
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
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await AnswersService.GetAnswer(id);
            AnswersViewModel answer = new AnswersViewModel()
            {
                AnswerString = result.AnswerString,
                Id = id,
                StudentId = result.StudentId,
                QuestionId = result.QuestionId
            };
            return View(answer);
        }

        public async Task<IActionResult> Update(Answer model)
        {
            if (ModelState.IsValid)
            {
                var Answer = await AnswersService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
