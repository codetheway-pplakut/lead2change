using Lead2Change.Web.Ui.Models;
using Lead2Change.Services.Goals;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Lead2Change.Domain.Models;
using Lead2Change.Services.Identity;
using Microsoft.AspNetCore.Identity;

namespace Lead2Change.Web.Ui.Controllers
{
    public class GoalsController : _BaseController
    {
        private IGoalsService GoalsService;
        public GoalsController(IUserService identityService, IGoalsService goalsService, RoleManager<AspNetRoles> roleManager, UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager) : base(identityService, roleManager, userManager, signInManager)
        {
            this.GoalsService = goalsService;
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
                SEL = result.SEL,
                GoalReviewDate = result.GoalReviewDate,
                WasItAccomplished = result.WasItAccomplished,
                Explanation = result.Explanation,             
            };
            return View(goal);
        }

        public async Task<IActionResult> Update(Goal model)
        {
            if (ModelState.IsValid)
            {
                var Goal = await GoalsService.Update(model);
                return RedirectToAction("Index", new { studentID = model.StudentId });
            }
            return View(model);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var goal = await GoalsService.GetGoal(id);

            if (goal == null)
            {
                return RedirectToAction("Register");
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
                SEL = goal.SEL,
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
                Goal model = new Goal()
                {
                    GoalSet = viewModel.GoalSet,
                    StudentId = viewModel.StudentId,
                    Id = viewModel.Id,
                    DateGoalSet = viewModel.DateGoalSet,
                    SEL = viewModel.SEL,
                    GoalReviewDate = viewModel.GoalReviewDate,
                    WasItAccomplished = viewModel.WasItAccomplished,
                    Explanation = viewModel.Explanation,

                };
                // TODO: Create StudentId on user Registration
                var result = await GoalsService.Create(model);
                return RedirectToAction("Index", new { studentID = model.StudentId });
                                

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
            foreach (Goal goal in goals)
            {
                result.Add(new GoalViewModel()
                {
                    GoalSet = goal.GoalSet,
                    Id = goal.Id,
                    StudentId = goal.StudentId,
                    DateGoalSet = goal.DateGoalSet,
                    SEL = goal.SEL,
                    GoalReviewDate = goal.GoalReviewDate,
                    WasItAccomplished = goal.WasItAccomplished,
                    Explanation = goal.Explanation

                });
            }

            return View(new GoalsAndIDViewModel(studentID)
            {
                GoalModels = result
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

