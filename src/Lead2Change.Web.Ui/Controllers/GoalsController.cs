using Lead2Change.Web.Ui.Models;
using Lead2Change.Services.Goals;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;

namespace Lead2Change.Web.Ui.Controllers
{
    public class GoalsController : Controller
    {
        private IGoalsService GoalsService;
        public GoalsController(IGoalsService goalsService)
        {
            this.GoalsService = goalsService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return View(new GoalViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(GoalViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Goal.Length > 0)
                {
                    if (model.SEL.Length > 0)
                    {
                        if (model.WasItAccomplished.Length > 0)
                        {
                            if(model.Explanation.Length > 0)
                            {
                                
                                    Goal goal = new Goal()
                                    {
                                        GoalSet = model.Goal,
                                        Id = model.Id,
                                        StudentId = model.StudentId,
                                        DateGoalSet = model.DateGoalSet,
                                        SEL = model.SEL,
                                        GoalReviewDate = model.GoalReviewDate,
                                        WasItAccomplished = model.WasItAccomplished,
                                        Explanation = model.Explanation,
                                       
                                    };
                                    var result = await GoalsService.Create(goal);
                                    return RedirectToAction("Index");
                                }
                            }
                    }
                }

            }
            return View("Create", model);
        }
    }
}

