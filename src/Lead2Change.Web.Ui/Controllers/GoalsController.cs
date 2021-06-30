using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Services.Goals;

namespace Lead2Change.Web.Ui.Controllers
{
    public class GoalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private IGoalsService GoalsService;

        public GoalsController(IGoalsService goalsService)
        {
            this.GoalsService = goalsService;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await GoalsService.GetGoals(id);
            GoalViewModel goal = new GoalViewModel()
            {
                Id = id,
                StudentId = result.StudentId,
                GoalSet = result.GoalSet,
                DateGoalSet = result.DateGoalSet,
                SEL = result.SEL,
                GoalReviewDate = result.GoalReviewDate,
                WasItAccomplished = result.WasItAccomplished,
                Explanation = result.Explanation
            };
            return View(goal);
        }
    }
}
