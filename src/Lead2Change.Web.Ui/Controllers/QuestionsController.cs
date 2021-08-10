using Lead2Change.Web.Ui.Models;
using Lead2Change.Services.Questions;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;
using Lead2Change.Domain.Constants;
using Lead2Change.Domain.Models;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Services.CareerDeclarationService;
using Lead2Change.Services.Identity;
using Lead2Change.Services.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Lead2Change.Web.Ui.Controllers
{
    [Authorize(Roles = StringConstants.RoleNameAdmin + "," + StringConstants.RoleNameCoach)]
    public class QuestionsController : _BaseController
    {
        private IQuestionsService QuestionsService;
        private IStudentService _studentService;

        public QuestionsController(IQuestionsService questionsService, IUserService identityService, ICareerDeclarationService careerDeclarationService, IStudentService studentService, RoleManager<AspNetRoles> roleManager, UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager) : base(identityService, roleManager, userManager, signInManager)
        {
            this.QuestionsService = questionsService;
            _studentService = studentService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await QuestionsService.GetQuestions());
        }
        public async Task<IActionResult> Create()
        {
            if (User.IsInRole(StringConstants.RoleNameCoach)) {
                return Error("403: Forbidden");
            }

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
                if (Request.Form["RedirectCreateView"].Equals("Create and New")) { return RedirectToAction("Create"); }
                else {return RedirectToAction("Index");}

            }
            return View("Create", model);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            if (User.IsInRole(StringConstants.RoleNameCoach))
            {
                return Error("403: Forbidden");
            }

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
            if (User.IsInRole(StringConstants.RoleNameCoach))
            {
                return Error("403: Forbidden");
            }
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
            if (User.IsInRole(StringConstants.RoleNameCoach))
            {
                return Error("403: Forbidden");
            }
            var question = await QuestionsService.GetQuestion(id);
            await QuestionsService.Delete(question);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> PermanentDelete(Guid id)
        {
            if (User.IsInRole(StringConstants.RoleNameCoach))
            {
                return Error("403: Forbidden");
            }
            var question = await QuestionsService.GetQuestion(id);
            await QuestionsService.PermanentDelete(question);
            return RedirectToAction("ArchivedQuestions", new { questionID = question.Id });
        }
    }
}
