using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Web.Ui.Models;
using Lead2Change.Services.Goals;

namespace Lead2Change.Web.Ui.Controllers
{
    public class GoalsController : Controller
    {

        private IGoalsService GoalsService;

        public GoalsController(IGoalsService goalsService)
        {
            this.GoalsService = goalsService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await GoalsService.GetGoals());
        
        }

    }
}
