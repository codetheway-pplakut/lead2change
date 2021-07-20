using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using Lead2Change.Services.Interviews;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Domain.Models;
using Lead2Change.Services.Questions;

namespace Lead2Change.Web.Ui.Controllers
{
    public class InterviewsController : Controller
    {
        private IInterviewService _interviewsService;
        private IQuestionsService _questionService;

        public InterviewsController(IInterviewService interviewsService, IQuestionsService questionService)
        {
            this._interviewsService = interviewsService;
            this._questionService = questionService;
        }
        public async Task<IActionResult> Index()
        {
            List<InterviewViewModel> result = new List<InterviewViewModel>();
            List<Interview> interviews = await _interviewsService.GetInterviews();
            foreach (Interview interview in interviews)
            {
                result.Add(new InterviewViewModel
                {
                    InterviewName = interview.InterviewName,
                    Id = interview.Id,
                    QuestionInInterviews = interview.QuestionInInterviews
                });
            }
            return View(result);

        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var interview = await _interviewsService.GetInterview(id);
            await _interviewsService.Delete(interview);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            return View(new InterviewQuestionCreateViewModel()) ;
        }

        public async Task<IActionResult> Register(InterviewQuestionCreateViewModel model, String submitButton)
        {
            if (ModelState.IsValid)
            {
                // The value of the submitButton tells the code which button (add question or save) was pushed
                if (submitButton.Equals("addQuestion"))
                {
                    if(model.QuestionText != null && !model.QuestionText.Equals(""))
                    {
                        Question newQuestion = new Question() { QuestionString = model.QuestionText };
                        model.QuestionInInterviews.Add(new QuestionInInterview { InterviewId = model.Id, Question = newQuestion, QuestionId = newQuestion.Id });
                        await _questionService.Create(newQuestion);
                        model.QuestionText = null;
                    }
                    return View("Create", model);


                }
                // Creates the interview
                Interview interview = new Interview()
                {
                    QuestionInInterviews = model.QuestionInInterviews,
                    InterviewName = model.InterviewName,
                    Id = model.Id
                    
                };
                // Handles Question Creation
                Question addedQuestion = new Question() { QuestionString = model.QuestionText };
                // Links the question and interview
                interview.QuestionInInterviews.Add(new QuestionInInterview { Interview = interview, InterviewId = interview.Id, Question = addedQuestion, QuestionId = addedQuestion.Id });
                // Adds them to the database
                await _questionService.Create(addedQuestion);
                var result = await _interviewsService.Create(interview);
                return RedirectToAction("Index");


            }
            return View("Create", model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _interviewsService.GetInterview(id);
            InterviewViewModel interview = new InterviewViewModel()
            {
                Id = id,
                QuestionInInterviews=result.QuestionInInterviews,
                InterviewName=result.InterviewName
            };
            return View(interview);
        }

        public async Task<IActionResult> Update(InterviewViewModel model)
        {
            


            Interview interview = new Interview
            {
                Id = model.Id,
                QuestionInInterviews = model.QuestionInInterviews,
                InterviewName = model.InterviewName
            };
            if (ModelState.IsValid)
            {
                interview = await _interviewsService.Update(interview);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _interviewsService.GetInterviewAndQuestions(id);
            InterviewViewModel interview = new InterviewViewModel()
            {
                Id = id,
                QuestionInInterviews = result,
                InterviewName = result.FirstOrDefault().Interview.InterviewName
            };
            return View(interview);
        }

    }
}
   

