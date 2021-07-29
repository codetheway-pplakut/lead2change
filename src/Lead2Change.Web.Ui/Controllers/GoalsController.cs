using Lead2Change.Web.Ui.Models;
using Lead2Change.Services.Goals;
using Lead2Change.Services.Students;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Lead2Change.Domain.Models;
using Lead2Change.Services.Identity;
using Microsoft.AspNetCore.Identity;

// TODO: Add auth
namespace Lead2Change.Web.Ui.Controllers
{
    public class GoalsController : _BaseController
    {
        private IGoalsService GoalsService;
        private IStudentService StudentService;
        public GoalsController(IUserService identityService, IGoalsService goalsService, IStudentService studentService, RoleManager<AspNetRoles> roleManager, UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager) : base(identityService, roleManager, userManager, signInManager)
        {
            this.GoalsService = goalsService;
            this.StudentService = studentService;
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await GoalsService.GetGoal(id);
            GoalViewModel goal = new GoalViewModel()
            {
                Id = id,
                StudentId = result.StudentId,
                GoalSet = result.GoalSet,
                DateGoalSet = result.DateGoalSet,
                SEL = result.SEL.Split(","),
                GoalReviewDate = result.GoalReviewDate,
                WasItAccomplished = result.WasItAccomplished,
                Explanation = result.Explanation,             
            };
            return View(goal);
        }

        public async Task<IActionResult> Update(GoalViewModel model)
        {
            // Prevents null exceptions from no SEL input being selected
            string goalSEL = (model.SEL == null) ? "" : String.Join(", ", model.SEL);


            Goal goal = new Goal
            {
                GoalSet = model.GoalSet,
                StudentId = model.StudentId,
                Id = model.Id,
                DateGoalSet = model.DateGoalSet,
                SEL = goalSEL,
                GoalReviewDate = model.GoalReviewDate,
                WasItAccomplished = model.WasItAccomplished,
                Explanation = model.Explanation,
            };
            if (ModelState.IsValid)
            {
                var Goal = await GoalsService.Update(goal);
                return RedirectToAction("Index", new { studentID = model.StudentId });
            }
            return View(model);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var goal = await GoalsService.GetGoal(id);

            if (goal == null)
            {
                return RedirectToAction("Create");
            }

            if (!await CanEditStudent(goal.StudentId))
            {
                return Error("403: Not authorized to view this student.");
            }

            GoalViewModel viewModel = new GoalViewModel()
            {
                Id = id,
                StudentId = goal.StudentId,
                DateGoalSet = goal.DateGoalSet,
                GoalSet = goal.GoalSet,
                SEL = goal.SEL.Split(",") ,
                GoalReviewDate = goal.GoalReviewDate,
                WasItAccomplished = goal.WasItAccomplished,
                Explanation = goal.Explanation
            };
            return View(viewModel);
        }

        /**
         * Accepts the Guid of the student that the goal is being created for
         */
        public async Task<IActionResult> Create(Guid studentID)
        {
            return View(new GoalViewModel()
            {
                StudentId = studentID,
                // This changes the initial date displayed in the chooser
                DateGoalSet = DateTime.Today,
                GoalReviewDate = DateTime.Today,
            }) ;
        }
        [HttpPost]
        public async Task<IActionResult> Register(GoalViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Checks to prevent null exception errors
                string goalSEL = (viewModel.SEL == null) ? "" : string.Join(", ", viewModel.SEL);
                Goal goal = new Goal()
                {
                    GoalSet = viewModel.GoalSet,
                    StudentId = viewModel.StudentId,
                    Id = viewModel.Id,
                    DateGoalSet = viewModel.DateGoalSet,
                    SEL = goalSEL,
                    GoalReviewDate = viewModel.GoalReviewDate,
                    WasItAccomplished = viewModel.WasItAccomplished,
                    Explanation = viewModel.Explanation,
                };
                // TODO: Create StudentId on user Registration
                var result = await GoalsService.Create(goal);
                return RedirectToAction("Index", new { studentID = result.StudentId });
            }
            return View("Create", viewModel);
        }

        /**
         * Note that Index takes in a studentID (it displays the goals for that student)
         * And returns a special view model which contains a list of GoalViewModels
         * And the studentID
         */
        public async Task<IActionResult> Index(Guid studentID)
        {
            List<GoalViewModel> result = new List<GoalViewModel>();
            List<Goal> goals = await GoalsService.GetGoals(studentID);
            var student1 = await StudentService.GetStudent(studentID);

            foreach (Goal goal in goals)
            {
                result.Add(new GoalViewModel()
                {
                    GoalSet = goal.GoalSet,
                    Id = goal.Id,
                    StudentId = goal.StudentId,
                    DateGoalSet = goal.DateGoalSet,
                    SEL = goal.SEL.Split(","),
                    GoalReviewDate = goal.GoalReviewDate,
                    WasItAccomplished = goal.WasItAccomplished,
                    Explanation = goal.Explanation

                });
            }

            return View(new GoalsAndIDViewModel(studentID)
            {
                GoalModels = result,
                Name = student1.StudentFirstName
            }) ;
        
        }

        /**
         * Handles the deletion of a goal based off the goal's id
         */
        public async Task<IActionResult> Delete(Guid id, Guid studentID)
        {
            var goal = await GoalsService.GetGoal(id);
            await GoalsService.Delete(goal);
            return RedirectToAction("Index", new { studentID = goal.StudentId });
        }

    }
}

