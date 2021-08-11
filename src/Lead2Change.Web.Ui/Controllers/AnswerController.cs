using Lead2Change.Services.Answers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Domain.Models;
using Lead2Change.Services.Interviews;
using Lead2Change.Services.Students;
using Lead2Change.Web.Ui.Models;
using Lead2Change.Services.Goals;
using Lead2Change.Services.Students;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Lead2Change.Domain.Constants;

using Lead2Change.Domain.Models;
using Lead2Change.Services.Identity;
using Microsoft.AspNetCore.Identity;


namespace Lead2Change.Web.Ui.Controllers
{
    [Authorize(Roles = StringConstants.RoleNameAdmin + "," + StringConstants.RoleNameCoach)]
    public class AnswerController : Controller
    {

        private IAnswersService AnswersService;
        private IInterviewService _interviewsService;
        private IStudentService _studentService;

        public AnswerController(IAnswersService answersService, IInterviewService interviewsService, IStudentService studentService)
        {
            this.AnswersService = answersService;
            this._interviewsService = interviewsService;
            this._studentService = studentService;

        }
        public async Task<IActionResult> Index(Guid interviewID, Guid studentId)
        {
            var student1 = await _studentService.GetStudent(studentId);

            List<AnswersViewModel> result = new List<AnswersViewModel>();
            List<Answer> answers = await AnswersService.GetAnswers(interviewID);
            var interview1 = await _interviewsService.GetInterview(interviewID);
            foreach (Answer answer in answers)
            {
                if (answer.StudentId == studentId)
                {
                    result.Add(new AnswersViewModel()
                    {
                        AnswerString = answer.AnswerString,
                        QuestionString = answer.QuestionString,
                        Id = answer.Id,
                        StudentId = studentId,
                        QuestionId = answer.QuestionId,


                        InterviewId = answer.InterviewId,
                        StudentName = student1.StudentFirstName,
                        InterviewName = (await _interviewsService.GetInterview(interviewID)).InterviewName,
                    });
                }
            }
            return View(new StudentInterviewAndIDViewModel(interviewID)
            {
                AnswerModels = result,
                InterviewName = interview1.InterviewName,
                StudentName = student1.StudentFirstName,
                StudentID = studentId,
            }) ;

        }
        public async Task<IActionResult> Create()
        {
            return View(new AnswersViewModel());
          
        }
        public async Task<IActionResult> AnswerQuestion(Guid id, Guid studentID)
        {
           

            var result = await _interviewsService.GetInterviewAndQuestions(id);
            
            AnswerQuestionViewModel answer = new AnswerQuestionViewModel()
            {
                Id = id,
                StudentId = studentID,
                QuestionInInterviews = result,
                InterviewName = (await _interviewsService.GetInterview(id)).InterviewName,

            InterviewId = result.FirstOrDefault().Interview.Id,
              
            };
            return View(answer);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAnswerQuestion(AnswerQuestionViewModel model)
        {
            List<Student> students = await _studentService.GetActiveStudents();
            var questions = await _interviewsService.GetInterviewAndQuestions(model.InterviewId);
      
            if (ModelState.IsValid)
            {
                for (int i = 0; i < model.Answers.Count; i++)
                {
                    Answer answer = new Answer()
                    {
                        AnswerString = model.Answers[i].AnswerString,
                        QuestionString = questions[i].Question.QuestionString,
                        InterviewId = model.InterviewId,
                        StudentId = model.StudentId,
                        QuestionId = questions[i].QuestionId,
                        
                        InterviewName = (await _interviewsService.GetInterview(model.InterviewId)).InterviewName,
                        
                    };
                    var result = await AnswersService.AnswerQuestion(answer);
                    
                }
                return RedirectToAction("Index", new { interviewID = model.InterviewId, studentId = model.StudentId });


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
                    QuestionString = model.QuestionString,
                    Id = model.Id,
                    StudentId = model.StudentId,
                    QuestionId = model.QuestionId,
                    StudentName = model.StudentName,

                };
                var result = await AnswersService.Create(answer);
                return RedirectToAction("Index");
            }
            return View("Create", model);
        }
        public async Task<IActionResult> Delete(Guid id, Guid studentID)
        {
            var answer = await AnswersService.GetAnswer(id);
            await AnswersService.Delete(answer);
            return RedirectToAction("Index", new { interviewID = answer.InterviewId, studentID=answer.StudentId });
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await AnswersService.GetAnswer(id);
            AnswersViewModel answer = new AnswersViewModel()
            {
                AnswerString = result.AnswerString,
                QuestionString = result.QuestionString,
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
                return RedirectToAction("Index", new { interviewID = model.InterviewId, studentID = model.StudentId });
            }

            return View(model);
        }
    }
}
