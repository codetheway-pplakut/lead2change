using Lead2Change.Services.Answers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Domain.Models;
using Lead2Change.Services.Interviews;

namespace Lead2Change.Web.Ui.Controllers
{
    public class AnswerController : Controller
    {

        private IAnswersService AnswersService;
        private IInterviewService _interviewsService;
   
        public AnswerController(IAnswersService answersService, IInterviewService interviewsService)
        {
            this.AnswersService = answersService;
            this._interviewsService = interviewsService;
        }
        public async Task<IActionResult> Index(Guid interviewID)
        {
          
            List<AnswersViewModel> result = new List<AnswersViewModel>();
            List<Answer> answers = await AnswersService.GetAnswers(interviewID);
            foreach (Answer answer in answers)
            {
                result.Add(new AnswersViewModel()
                {
                    AnswerString = answer.AnswerString,
                    Id = answer.Id,
                    StudentId = answer.StudentId,
                    QuestionId = answer.QuestionId,
                    InterviewId = answer.InterviewId,
                });
            }
            return View(result);
     
        }
        public async Task<IActionResult> Create()
        {
            return View(new AnswersViewModel());
          
        }
        public async Task<IActionResult> AnswerQuestion(Guid id)
        {
           
            var result = await _interviewsService.GetInterviewAndQuestions(id);
            AnswerQuestionViewModel answer = new AnswerQuestionViewModel()
            {
                Id = id,
                QuestionInInterviews = result,
                InterviewName = result.FirstOrDefault().Interview.InterviewName,
                InterviewId = result.FirstOrDefault().Interview.Id,
            };
            return View(answer);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAnswerQuestion(AnswerQuestionViewModel model)
        { 
            
           var questions = await _interviewsService.GetInterviewAndQuestions(model.InterviewId);
      
            if (ModelState.IsValid)
            {
                for (int i = 0; i < model.Answers.Count; i++)
                {
                    Answer answer = new Answer()
                    {
                        AnswerString = model.Answers[i].AnswerString,
                        InterviewId = model.InterviewId,
                        StudentId = model.StudentId,
                        QuestionId = questions[i].QuestionId,
                    };
                    var result = await AnswersService.AnswerQuestion(answer);
                    
                }
                return RedirectToAction("Index", new { interviewID = model.InterviewId });


            }
            return View("Create", model);
        }
        
        public async Task<IActionResult> Register(AnswersViewModel model)
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
                return RedirectToAction("Index");
            }
            return View("Create", model);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var answer = await AnswersService.GetAnswer(id);
            await AnswersService.Delete(answer);
            return RedirectToAction("Index", new { interviewID = answer.InterviewId });
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await AnswersService.GetAnswer(id);
            AnswersViewModel answer = new AnswersViewModel()
            {
                AnswerString = result.AnswerString,
                Id = id,
                StudentId = result.StudentId,
                QuestionId = result.QuestionId,
                InterviewId = result.InterviewId,
            };
            return View(answer);
        }
        public async Task<IActionResult> Update(Answer model)
        {
            if (ModelState.IsValid)
            {
                var Answer = await AnswersService.Update(model);
                return RedirectToAction("Index", new { interviewID = model.InterviewId });
            }

            return View(model);
        }
    }
}
