using Lead2Change.Web.Ui.Models;
using Lead2Change.Services.Goals;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Web.Ui.Models;
using Lead2Change.Services.Goals;
using Lead2Change.Domain.Models;
using Lead2Change.Domain.ViewModels;


namespace Lead2Change.Web.Ui.Controllers
{
    public class GoalsController : Controller
    {
        private IGoalsService GoalsService;
        public GoalsController(IGoalsService goalsService)
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
            var result = await GoalsService.GetGoal(id);
            GoalViewModel goal = new GoalViewModel()
            {
                Id = id,
                StudentId = result.StudentId,
                DateGoalSet = result.DateGoalSet,
                GoalSet = result.GoalSet,
                SEL = result.SEL,
                GoalReviewDate = result.GoalReviewDate,
                WasItAccomplished = result.WasItAccomplished,
                Explanation = result.Explanation
            };
            return View(goal);
        }
        public async Task<IActionResult> Create(Guid studentID)
        {
            return View(new GoalViewModel()
            {
                StudentId = studentID
                DateGoalSet = DateTime.Today,
                GoalReviewDate = DateTime.Today,
            }) ;
        }
        [HttpPost]
        public async Task<IActionResult> Register(GoalViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                                    Goal goal = new Goal()
                                    {
                                        GoalSet = model.GoalSet,
                                        StudentId = model.StudentId,
                                        Id = model.Id,
                                        DateGoalSet = model.DateGoalSet,
                                        SEL = model.SEL,
                                        GoalReviewDate = model.GoalReviewDate,
                                        WasItAccomplished = model.WasItAccomplished,
                                        Explanation = model.Explanation,

                                    };
                                    var result = await GoalsService.Create(goal);
                                    return RedirectToAction("Index", new { studentID = goal.StudentId });
                                

            }
            return View("Create", model);
        }
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

    }
}

