using Lead2Change.Domain.Constants;
using Lead2Change.Domain.Models;
using Lead2Change.Services.Identity;
using Lead2Change.Web.Ui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Services.Questions;

namespace Lead2Change.Web.Ui.Controllers
{
    public class HomeController : _BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private IQuestionsService QuestionsService;            

        public HomeController(ILogger<HomeController> logger, IUserService identityService, IQuestionsService questionsService, RoleManager<AspNetRoles> roleManager, UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager) : base(identityService, roleManager, userManager, signInManager)
        {
            _logger = logger;
            this.QuestionsService = questionsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Configure()
        {
            _logger = logger;
        }

            return RedirectToAction("Index");
        }     

        protected async Task CreateQuestions(string QuestionString)
        {
            Question question = new Question()
            {
                QuestionString = QuestionString              
            };
            var result = await QuestionsService.Create(question);

        }
    }
}
