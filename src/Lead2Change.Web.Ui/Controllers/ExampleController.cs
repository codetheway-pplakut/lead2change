using Lead2Change.Domain.Models;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Services.Goals;
using Lead2Change.Services.Identity;
using Lead2Change.Services.Students;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Lead2Change.Web.Ui.Controllers
{
    public class ExampleController : _BaseController
    {
        IStudentService _studentService;

        public ExampleController(IUserService identityService, IGoalsService goalsService, IStudentService studentService, RoleManager<AspNetRoles> roleManager, UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager) : base(identityService, roleManager, userManager, signInManager)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// GET /example/getstudents
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetStudents();
            return Json(students);
        }

        /// <summary>
        /// GET /example/getstudent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> GetStudent(Guid id)
        {
            var student = await _studentService.GetStudent(id);
            return Json(student);
        }

        /// <summary>
        /// POST /example/createstudent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreateStudent(StudentInterestFormViewModel model)
        {
            string response = string.Empty;

            try
            {
                Student student = new Student()
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                    StudentFirstName = model.StudentFirstName,
                    StudentLastName = model.StudentLastName,
                    StudentDateOfBirth = model.StudentDateOfBirth,
                    StudentCellPhone = model.StudentCellPhone,
                    StudentEmail = model.StudentEmail
                };

                var created = await _studentService.Create(student);

                if (created != null)
                {
                    response = "Success";
                }
                else
                {
                    response = "Error";
                }
            }
            catch (Exception ex)
            {
                response = String.Format("{0} - {1}", ex.Message, ex.InnerException.ToString());
            }

            return Json(response);
        }

        /// <summary>
        /// POST /example/updatestudent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> UpdateStudent(StudentInterestFormViewModel model)
        {
            var student = await _studentService.GetStudent(model.Id);
            string response = string.Empty;

            if (student != null)
            {
                try
                {
                    Student updatedStudent = new Student()
                    {
                        Id = model.Id,
                        Active = model.Active,
                        StudentFirstName = model.StudentFirstName,
                        StudentLastName = model.StudentLastName,
                        StudentDateOfBirth = model.StudentDateOfBirth,
                        StudentCellPhone = model.StudentCellPhone,
                        StudentEmail = model.StudentEmail
                    };

                    var updated = await _studentService.Update(updatedStudent);

                    if (updated != null)
                    {
                        response = "Success";
                    }
                    else
                    {
                        response = "Error";
                    }
                }
                catch (Exception ex)
                {
                    response = String.Format("{0} - {1}", ex.Message, ex.InnerException.ToString());
                }
            }
            else
            {
                response = "No Student";
            }

            return Json(response);
        }
    }
}
