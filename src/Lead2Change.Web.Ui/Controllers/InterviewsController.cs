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
using Lead2Change.Services.Students;
using Lead2Change.Services.Answers;
using Microsoft.AspNetCore.Authorization;
using Lead2Change.Domain.Constants;

namespace Lead2Change.Web.Ui.Controllers
{
    [Authorize(Roles = StringConstants.RoleNameAdmin + "," + StringConstants.RoleNameCoach)]
    public class InterviewsController : Controller
    {
        private IInterviewService _interviewsService;
        private IQuestionsService _questionService;
        private IQuestionInInterviewService _questionInInterviewService;
        private IStudentService _studentService;

        public InterviewsController(IInterviewService interviewsService, IQuestionsService questionService, IQuestionInInterviewService questionInInterviewService, IStudentService studentService)
        {
            this._interviewsService = interviewsService;
            this._questionService = questionService;
            this._questionInInterviewService = questionInInterviewService;
            this._studentService = studentService;
        }
        public async Task<IActionResult> Index(Guid studentID)
        {
            string studentName = string.Empty;
            if (studentID != Guid.Empty)
            {
                var student1 = await _studentService.GetStudent(studentID);
                studentName = student1.StudentFirstName;
            }
            List<InterviewViewModel> result = new List<InterviewViewModel>();
            List<Interview> interviews = await _interviewsService.GetInterviews();
            foreach (Interview interview in interviews)
            {
                result.Add(new InterviewViewModel
                {
                    InterviewName = interview.InterviewName,
                    Id = interview.Id,
                    QuestionInInterviews = interview.QuestionInInterviews,
                    StudentId = studentID,
                   

           
                        
                }) ;
            }
            return View(new InterviewAndIDViewModel(studentID)
            {
                InterviewViewModels = result,
                StudentId = studentID,
                StudentName = studentName 
            });
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var interview = await _interviewsService.GetInterview(id);
            await _interviewsService.Delete(interview);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            return View(new InterviewWithQuestionsViewModel()) ;
        }
        public async Task<IActionResult> Register(InterviewWithQuestionsViewModel model, String submitButton)
        {
            if (ModelState.IsValid)
            {
                // If everything is empty, then there is nothing to create and program should return to index
                if(String.IsNullOrEmpty(model.InterviewName) && String.IsNullOrEmpty(model.QuestionText) && (String.IsNullOrEmpty(submitButton) || submitButton.Equals("Create")))
                {
                    return RedirectToAction("Index");
                }
                // Interviews with no set names default to Untitled
                model.InterviewName = (String.IsNullOrEmpty(model.InterviewName)) ? "Untitled" : model.InterviewName;
                Interview interview = await _interviewsService.Create(new Interview { InterviewName = model.InterviewName });
                // Update the viewModel's ID
                model.Id = interview.Id;
                // After the model is created, the update method handles the rest of the funcionality
                return await Update(model, submitButton, true);
            }
            return RedirectToAction("Create");
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            List<QuestionInInterview> questionInInterviews = await _interviewsService.GetInterviewAndQuestions(id);
            InterviewWithQuestionsViewModel newModel = new InterviewWithQuestionsViewModel
            {
                AddedQuestions = questionInInterviews.Select(questionInInterview => questionInInterview.Question).ToList(),
                Id = id,
                InterviewName = await GetInterviewName(id),
                UnselectedQuestions = await _questionService.GetAllExcept(id)
            };
            return View("Edit", newModel);
        }

        public async Task<IActionResult> Update(InterviewWithQuestionsViewModel model, String submitButton, bool fromCreate = false)
        {
            if (ModelState.IsValid)
            {
                if (!fromCreate)
                {
                    await _interviewsService.Update(new Interview
                    {
                        Id = model.Id,
                        InterviewName = model.InterviewName
                    });
                }
                
            
                List<QuestionInInterview> questionInInterviews = await _interviewsService.GetInterviewAndQuestions(model.Id);
                model.AddedQuestions = questionInInterviews.Select(questionInInterview => questionInInterview.Question).ToList();

                // Step 2: Create and Add Question
                if (!String.IsNullOrEmpty(model.QuestionText))
                {
                    QuestionInInterview question = await AddQuestion(model.Id, model.QuestionText, model.AddedQuestions.Count + 1);
                    model.AddedQuestions.Add(question.Question);
                    model.QuestionText = null;
                }
                // Step 3: Return User to Correct View Based on the Button Pushed
                ModelState.Clear();
                return submitButton switch
                {
                    "addQuestion" => View("Edit", model),
                    "selectQuestion" => RedirectToAction("QuestionSelect", new { id = model.Id }),
                    _ => RedirectToAction("Index"),
                };
            }
            return View("Edit", model);
        }

        public async Task<IActionResult> Details(Guid id, Guid studentID)
        {
            var result = await _interviewsService.GetInterviewAndQuestions(id);
            InterviewViewModel interviewModel;
            // If there are no questions in the interview, we can't get the title from QuestionInInterviews0
            if(result == null || result.Count == 0)
            {
                var interview = await _interviewsService.GetInterview(id);
                interviewModel = new InterviewViewModel {
                    Id = id,
                    InterviewName = interview.InterviewName,
                    StudentId = studentID
                };
            }
            else
            {
                interviewModel = new InterviewViewModel()
                {
                    Id = id,
                    QuestionInInterviews = result,
                    InterviewName = result.FirstOrDefault().Interview.InterviewName,
                    StudentId = studentID
                };
            }
            return View(interviewModel);
        }

        public async Task<IActionResult> QuestionSelect(Guid id)
        {
            List<QuestionInInterview> interviewAndQuestions = await _interviewsService.GetInterviewAndQuestions(id);
            List<Question> remainingQuestions = await _questionService.GetAllExcept(id);
            InterviewWithQuestionsViewModel interview;
            if(interviewAndQuestions != null && interviewAndQuestions.Count != 0)
            {
                interview = new InterviewWithQuestionsViewModel
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
                interview = new InterviewWithQuestionsViewModel
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
                QuestionId = questionId,
                Order = (await _interviewsService.GetInterviewAndQuestions(interviewId)).Count + 1
            });
            return RedirectToAction("QuestionSelect", new { id = interviewId });
        }
        public async Task<IActionResult> StudentsInInterview(Guid Id)
        {
            List<Student> students = await _studentService.GetActiveStudents();
            List<AnswerQuestionViewModel> answerQuestion = new List<AnswerQuestionViewModel>();
            var result = await _interviewsService.GetInterviewAndQuestions(Id);
            foreach (Student studenti in students)
            {
                answerQuestion.Add(new AnswerQuestionViewModel()
                {
                    StudentId=studenti.Id,
                    QuestionInInterviews = result,
                    InterviewName = (await _interviewsService.GetInterview(result.FirstOrDefault().Interview.Id)).InterviewName,
                    InterviewId = result.FirstOrDefault().Interview.Id,
                });
            }

            StudentInterviewViewModel student = new StudentInterviewViewModel()
            {
                Students = students,            
                StudentAnswer = answerQuestion
            };
            return View(student);
            
        }

        public async Task<IActionResult> RemoveQuestion(Guid interviewId, Guid questionId)
        {
            await _questionInInterviewService.Delete(new QuestionInInterview()
            {
                InterviewId = interviewId,
                QuestionId = questionId
            });
            return await Edit(interviewId);
        }
        private async Task<String> GetInterviewName(Guid id)
        {
            return (await _interviewsService.GetInterview(id)).InterviewName;
        }
        private async Task<QuestionInInterview> AddQuestion(Guid interviewId, String questionText, int order)
        {
            // Add Question to Database
            Question newQuestion = await _questionService.Create(new Question() { QuestionString = questionText });
            // Question Text is Reset after a corresponding question was added    
            // Add Relationship to database through a QuestionInInterview()
            return await _questionInInterviewService.Create(new QuestionInInterview()
            {
                InterviewId = interviewId,
                QuestionId = newQuestion.Id,
                Order = order
            });
        }
    }
}
   

