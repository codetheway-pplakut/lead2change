using Lead2Change.Web.Ui.Models;
using Lead2Change.Services.Questions;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;

namespace Lead2Change.Web.Ui.Controllers
{
    public class QuestionsController : Controller
    {
        private IQuestionsService QuestionsService;

        public QuestionsController(IQuestionsService questionsService)
        {
            this.QuestionsService = questionsService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await QuestionsService.GetQuestions());
        }
        public async Task<IActionResult> Create()
        {
            return View(new QuestionsViewModel());
        }
        public async Task<IActionResult> ArchivedQuestions()
        {
            return View(await QuestionsService.GetArchivedQuestions());
        }

        [HttpPost]

        public async Task<IActionResult> Register(QuestionsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Question question = new Question()
                {
                    QuestionString = model.QuestionString,
                    Id = model.Id,
                    QuestionInInterviews = model.QuestionInInterviews
                };
                var result = await QuestionsService.Create(question);
                return RedirectToAction("Index");
            }
            return View("Create", model);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await QuestionsService.GetQuestion(id);
            QuestionsViewModel question = new QuestionsViewModel()
            {
                QuestionString = result.QuestionString,
                Id = id,
                QuestionInInterviews = result.QuestionInInterviews
            };
            return View(question);
        }
        public async Task<IActionResult> ArchiveEdit(Guid id)
        {
            var result = await QuestionsService.GetQuestion(id);
            QuestionsViewModel question = new QuestionsViewModel()
            {
                QuestionString = result.QuestionString,
                Id = id,
                QuestionInInterviews = result.QuestionInInterviews
            };
            return View(question);
        }

        public async Task<IActionResult> Update(Question model)
        {
            if (ModelState.IsValid)
            {
                var Question = await QuestionsService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await QuestionsService.GetQuestion(id);
            QuestionsViewModel question = new QuestionsViewModel()
            {
                QuestionString = result.QuestionString,
                Id = id,
                QuestionInInterviews = result.QuestionInInterviews
            };
            return View(question);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var question = await QuestionsService.GetQuestion(id);
            await QuestionsService.Delete(question);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> PermanentDelete(Guid id)
        {
            var question = await QuestionsService.GetQuestion(id);
            await QuestionsService.PermanentDelete(question);
            return RedirectToAction("ArchivedQuestions", new { questionID = question.Id });
        }
    }
}
