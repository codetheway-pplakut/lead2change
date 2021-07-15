using Lead2Change.Services.Answers;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Index()
        {
            return View();
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
