using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using Lead2Change.Services.Interviews;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Domain.Models;
using Lead2Change.Services.Questions;
using Lead2Change.Services.QuestionInInterviews;

namespace Lead2Change.Web.Ui.Controllers
{
    public class InterviewsController : Controller
    {
        private IInterviewService _interviewsService;
        private IQuestionsService _questionService;
        private IQuestionInInterviewService _questionInInterviewService;

        public InterviewsController(IInterviewService interviewsService, IQuestionsService questionService, IQuestionInInterviewService questionInInterviewService)
        {
            this._interviewsService = interviewsService;
            this._questionService = questionService;
            this._questionInInterviewService = questionInInterviewService;
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
                // Step 1: Update/Create interview if needed
                if (model.Id == Guid.Empty)
                {
                    Interview interview = await _interviewsService.Create(new Interview { InterviewName = model.InterviewName });
                    // Update the viewModel's ID
                    model.Id = interview.Id;
                }
                else
                {
                    // Update database if title has changed
                    Interview interview = await _interviewsService.GetInterview(model.Id);
                    if (!interview.InterviewName.Equals(model.InterviewName))
                    {
                        interview.InterviewName = model.InterviewName;
                        interview = await _interviewsService.Update(interview);
                    }
                }
                // Step 2: Create and Add Question
                if (!String.IsNullOrEmpty(model.QuestionText))
                {
                    // Add Question to Database
                    Question newQuestion = new Question() { QuestionString = model.QuestionText };
                    newQuestion = await _questionService.Create(newQuestion);
                    model.QuestionInInterviews.Add(new QuestionInInterview { InterviewId = model.Id, Question = newQuestion, QuestionId = newQuestion.Id });
                    // Question Text is Reset after a corresponding question was added    
                    model.QuestionText = null;
                    // Add Relationship to database through a QuestionInInterview()
                    await _questionInInterviewService.Create(new QuestionInInterview()
                    {
                        InterviewId = model.Id,
                        QuestionId = newQuestion.Id
                    });
                }


                // Step 3: Return User to Correct View Based on the Button Pushed
                if (submitButton.Equals("addQuestion"))
                {
                    ModelState.Clear();
                    return View("Create", model);

                }
                else
                {
                    return RedirectToAction("Index");

                }
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

        public async Task<IActionResult> QuestionSelect(InterviewQuestionCreateViewModel model)
        {
            return View(model);
        }

    }
}
   

