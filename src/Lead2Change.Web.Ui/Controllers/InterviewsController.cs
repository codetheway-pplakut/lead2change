﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using Lead2Change.Services.Interviews;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Domain.Models;

namespace Lead2Change.Web.Ui.Controllers
{
    public class InterviewsController : Controller
    {
        private IInterviewService _interviewsService;

        public InterviewsController(IInterviewService interviewsService)
        {
            this._interviewsService = interviewsService;
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
            return View(new InterviewViewModel()
            {
                QuestionInInterviews = new List<QuestionInInterview>()
            }) ;
        }

        public async Task<IActionResult> Register(InterviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                

                Interview interview = new Interview()
                {
                    QuestionInInterviews = model.QuestionInInterviews,
                    InterviewName = model.InterviewName,
                    Id = model.Id
                    
                };
                // This Code is solely for testing the connection between Interviews and Questions and shouldn't be included in final code
                Question testQuestion = new Question() { QuestionString = "Test Question" };
                interview.QuestionInInterviews.Add(new QuestionInInterview { Interview = interview, InterviewId = interview.Id, Question = testQuestion, QuestionId = testQuestion.Id });
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
            var result = await _interviewsService.GetInterview(id);
            InterviewViewModel interview = new InterviewViewModel()
            {
                Id = id,
                QuestionInInterviews = result.QuestionInInterviews,
                InterviewName=result.InterviewName
            };
            return View(interview);
        }

    }
}
   

