using Microsoft.AspNetCore.Mvc;
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

        public GoalsController(IGoalsService goalService)
        {
            this.GoalsService = goalService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await GoalsService.GetGoal(id);
            GoalViewModel barrel = new GoalViewModel()
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
            return View(barrel);
        }

        public async Task<IActionResult> Update(Goal model)
        {
            if (ModelState.IsValid)
            {
                var Barrel = await GoalsService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await GoalsService.GetGoals(id);
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
    }
}
