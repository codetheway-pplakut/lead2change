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

        public async Task<IActionResult> Create(Guid? interviewId)
        {
            if (interviewId.HasValue)
            {
                List<QuestionInInterview> questionInInterviews = await _interviewsService.GetInterviewAndQuestions(interviewId.Value);
                InterviewQuestionCreateViewModel newModel = new InterviewQuestionCreateViewModel
                {
                    AddedQuestions = questionInInterviews.Select(questionInInterview => questionInInterview.Question).ToList(),
                    Id = interviewId.Value,
                    InterviewName = questionInInterviews.FirstOrDefault().Interview.InterviewName,
                    UnselectedQuestions = await _questionService.GetAllExcept(interviewId.Value)
                };
                return View(newModel);
            }
            return View(new InterviewQuestionCreateViewModel()) ;
        }
        public async Task<IActionResult> Register(InterviewQuestionCreateViewModel model, String submitButton)
        {
            if (ModelState.IsValid)
            {
                // Step 1: Update/Create interview if needed
                
                if (model.Id.Equals(Guid.Empty))
                {
                    // If everything is empty, then there is nothing to create and program should return to index
                    if(String.IsNullOrEmpty(model.InterviewName) && String.IsNullOrEmpty(model.QuestionText) && (String.IsNullOrEmpty(submitButton) || submitButton.Equals("Create")))
                    {
                        return RedirectToAction("Index");
                    }
                    Interview interview = await _interviewsService.Create(new Interview { InterviewName = (String.IsNullOrEmpty(model.InterviewName)) ? "Untitled" : model.InterviewName });
                    // Update the viewModel's ID
                    model.Id = interview.Id;
                   
                }
                
                else
                {
                    // Get the interview's information in the database
                    List<QuestionInInterview> questionInInterviews = await _interviewsService.GetInterviewAndQuestions(model.Id);
                    Interview interview = new Interview {
                        Id = model.Id,
                        InterviewName = questionInInterviews.FirstOrDefault().Interview.InterviewName
                    };
                    model.AddedQuestions = questionInInterviews.Select(questionInInterview => questionInInterview.Question).ToList();

                    // Update if the interview name changed
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
                    model.AddedQuestions.Add(newQuestion);
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
                if (submitButton != null && submitButton.Equals("addQuestion"))
                {
                    // Prepares View Model
                    ModelState.Clear();
                    return View("Create", model);

                }
                else if(submitButton != null && submitButton.Equals("selectQuestion"))
                {
                    ModelState.Clear();
                    return RedirectToAction("QuestionSelect", new { id = model.Id });
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
            InterviewViewModel interviewModel;
            // If there are no questions in the interview, we can't get the title from QuestionInInterviews0
            if(result == null || result.Count == 0)
            {
                var interview = await _interviewsService.GetInterview(id);
                interviewModel = new InterviewViewModel {
                    Id = id,
                    InterviewName = interview.InterviewName
                };
            }
            else
            {
                interviewModel = new InterviewViewModel()
                {
                    Id = id,
                    QuestionInInterviews = result,
                    InterviewName = result.FirstOrDefault().Interview.InterviewName
                };
            }
            return View(interviewModel);
        }

        public async Task<IActionResult> QuestionSelect(Guid id)
        {
            List<QuestionInInterview> interviewAndQuestions = await _interviewsService.GetInterviewAndQuestions(id);
            List<Question> remainingQuestions = await _questionService.GetAllExcept(id);
            InterviewQuestionCreateViewModel interview;
            if(interviewAndQuestions != null && interviewAndQuestions.Count != 0)
            {
                interview = new InterviewQuestionCreateViewModel
                {
                    AddedQuestions = interviewAndQuestions.Select(questionInInterview => questionInInterview.Question).ToList(),
                    InterviewName = interviewAndQuestions.FirstOrDefault().Interview.InterviewName,
                    Id = id,
                    UnselectedQuestions = remainingQuestions
                };
            }
            else
            {
                // If there arent any QuestionInInterviews, info must be taken from the actual interview
                var tempInterview = await _interviewsService.GetInterview(id);
                interview = new InterviewQuestionCreateViewModel
                {
                    Id = id,
                    InterviewName = tempInterview.InterviewName,
                    UnselectedQuestions = remainingQuestions
                };
            }
            
            return View(interview);
        }

        public async Task<IActionResult> SelectQuestion(Guid interviewId, Guid questionId)
        {
            // Adds an existing question to an interview
            QuestionInInterview result = await _questionInInterviewService.Create(new QuestionInInterview()
            {
                InterviewId = interviewId,
                QuestionId = questionId
            });
            return RedirectToAction("QuestionSelect", new { id = interviewId });
        }

    }
}
   

