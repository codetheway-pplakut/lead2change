using Lead2Change.Domain.Constants;
using Lead2Change.Domain.Models;
using Lead2Change.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lead2Change.Web.Ui.Controllers
{
    public class AdminController : _BaseController
    {
        public AdminController(IUserService identityService, RoleManager<AspNetRoles> roleManager, UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager) : base(identityService, roleManager, userManager, signInManager)
        {

        }


        public async Task<IActionResult> Index()
        {
            var users = await UserManager.GetUsersInRoleAsync(StringConstants.RoleNameAdmin);
            return View(users);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await UserManager.FindByIdAsync(id.ToString());

            // Check for bad id or student
            if (id == Guid.Empty )
            {
                return Error("400: Bad Request");
            }

            await UserManager.DeleteAsync(user);
            // Delete Student
            
            return RedirectToAction("Index");
        }
    }
}
