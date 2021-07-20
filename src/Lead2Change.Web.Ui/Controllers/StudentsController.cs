using Lead2Change.Domain.ViewModels;
using Lead2Change.Services.Identity;
using Lead2Change.Services.Students;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Lead2Change.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Lead2Change.Web.Ui.Controllers
{
    public class StudentsController : _BaseController
    {
        RoleManager<AspNetRoles> _roleManager;
        IStudentService _studentService;
        UserManager<AspNetUsers> _userManager;

        public StudentsController(IUserService identityService, RoleManager<AspNetRoles> roleManager, IStudentService studentService, UserManager<AspNetUsers> userManager) : base(identityService)
        {
            _roleManager = roleManager;
            _studentService = studentService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            await CreateDefaultRoles();
            await CreateNewUser("test0001@test.com", "Testtest@123", StringConstants.RoleNameStudent);

            return View(await _studentService.GetStudents());
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await _studentService.GetStudent(id);
            await _studentService.Delete(student);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Register()
        {
            return View(new RegistrationViewModel());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var studentscontainer = await _studentService.GetStudent(id);
            RegistrationViewModel a = new RegistrationViewModel()
            {
                Id = studentscontainer.Id,
                StudentFirstName = studentscontainer.StudentFirstName,
                StudentLastName = studentscontainer.StudentLastName,
                StudentDateOfBirth = studentscontainer.StudentDateOfBirth,
                StudentAddress = studentscontainer.StudentAddress,
                StudentApartmentNumber = studentscontainer.StudentApartmentNumber,
                StudentCity = studentscontainer.StudentCity,
                StudentState = studentscontainer.StudentState,
                StudentZipCode = studentscontainer.StudentZipCode,
                StudentHomePhone = studentscontainer.StudentHomePhone,
                StudentCellPhone = studentscontainer.StudentCellPhone,
                StudentEmail = studentscontainer.StudentEmail,
                StudentCareerPath = studentscontainer.StudentCareerPath,
                StudentCareerInterest = studentscontainer.StudentCareerInterest,

                ParentFirstName = studentscontainer.ParentFirstName,
                ParentLastName = studentscontainer.ParentLastName,
                Address = studentscontainer.Address,
                ParentApartmentNumber = studentscontainer.ParentApartmentNumber,
                ParentCity = studentscontainer.ParentCity,
                ParentState = studentscontainer.ParentState,
                ParentZipCode = studentscontainer.ParentZipCode,
                ParentHomePhone = studentscontainer.ParentHomePhone,
                ParentCellPhone = studentscontainer.ParentCellPhone,
                ParentEmail = studentscontainer.ParentEmail,

                KnowGuidanceCounselor = studentscontainer.KnowGuidanceCounselor,
                GuidanceCounselorName = studentscontainer.GuidanceCounselorName,
                MeetWithGuidanceCounselor = studentscontainer.MeetWithGuidanceCounselor,
                HowOftenMeetWithGuidanceCounselor = studentscontainer.HowOftenMeetWithGuidanceCounselor,
                DiscussWithGuidanceCounselor = studentscontainer.DiscussWithGuidanceCounselor
            };
            return View(a);
        }



        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.StudentFirstName.Length > 0)
                {
                    Student student = new Student()
                    {
                        Id = model.Id,
                        StudentFirstName = model.StudentFirstName,
                        StudentLastName = model.StudentLastName,
                        StudentDateOfBirth = model.StudentDateOfBirth,
                        StudentAddress = model.StudentAddress,
                        StudentApartmentNumber = model.StudentApartmentNumber,
                        StudentCity = model.StudentCity,
                        StudentState = model.StudentState,
                        StudentZipCode = model.StudentZipCode,
                        StudentHomePhone = model.StudentHomePhone,
                        StudentCellPhone = model.StudentCellPhone,
                        StudentEmail = model.StudentEmail,
                        StudentCareerPath = model.StudentCareerPath,
                        StudentCareerInterest = model.StudentCareerInterest,
                        ParentFirstName = model.ParentFirstName,
                        ParentLastName = model.ParentLastName,
                        Address = model.Address,
                        //ParentAdress
                        ParentApartmentNumber = model.ParentApartmentNumber,
                        ParentCity = model.ParentCity,
                        ParentState = model.ParentState,
                        ParentZipCode = model.ParentZipCode,
                        ParentHomePhone = model.ParentHomePhone,
                        ParentCellPhone = model.ParentCellPhone,
                        ParentEmail = model.ParentEmail,
                        KnowGuidanceCounselor = model.KnowGuidanceCounselor,
                        GuidanceCounselorName = model.GuidanceCounselorName,
                        MeetWithGuidanceCounselor = model.MeetWithGuidanceCounselor,
                        HowOftenMeetWithGuidanceCounselor = model.HowOftenMeetWithGuidanceCounselor,
                        DiscussWithGuidanceCounselor = model.DiscussWithGuidanceCounselor
                    };
                    var abc = await _studentService.Create(student);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _studentService.GetStudent(id);
            RegistrationViewModel list = new RegistrationViewModel()
            {
                Id = student.Id,
                //General Student Info
                StudentFirstName = student.StudentFirstName,
                StudentLastName = student.StudentLastName,
                StudentDateOfBirth = student.StudentDateOfBirth,
                StudentAddress = student.StudentAddress,
                StudentApartmentNumber = student.StudentApartmentNumber,
                StudentCity = student.StudentCity,
                StudentState = student.StudentState,
                StudentZipCode = student.StudentZipCode,
                StudentHomePhone = student.StudentHomePhone,
                StudentCellPhone = student.StudentCellPhone,
                StudentEmail = student.StudentEmail,
                StudentCareerPath = student.StudentCareerPath,
                StudentCareerInterest = student.StudentCareerInterest,
                //Parent Info
                ParentFirstName = student.ParentFirstName,
                ParentLastName = student.ParentLastName,
                Address = student.Address,
                ParentApartmentNumber = student.ParentApartmentNumber,
                ParentCity = student.ParentCity,
                ParentState = student.ParentState,
                ParentZipCode = student.ParentZipCode,
                ParentHomePhone = student.ParentHomePhone,
                ParentCellPhone = student.ParentCellPhone,
                ParentEmail = student.ParentEmail,
                //Guidance Counselor Info
                KnowGuidanceCounselor = student.KnowGuidanceCounselor,
                GuidanceCounselorName = student.GuidanceCounselorName,
                MeetWithGuidanceCounselor = student.MeetWithGuidanceCounselor,
                HowOftenMeetWithGuidanceCounselor = student.HowOftenMeetWithGuidanceCounselor,
                DiscussWithGuidanceCounselor = student.DiscussWithGuidanceCounselor

            };
            return View(list);
        }

        public async Task<IActionResult> Update(Student model)
        {
            if (ModelState.IsValid)
            {
                if (model.StudentFirstName.Length > 0)
                {
                    Student list = new Student()
                    {
                        Id = model.Id,
                        //General Student Info
                        StudentFirstName = model.StudentFirstName,
                        StudentLastName = model.StudentLastName,
                        StudentDateOfBirth = model.StudentDateOfBirth,
                        StudentAddress = model.StudentAddress,
                        StudentApartmentNumber = model.StudentApartmentNumber,
                        StudentCity = model.StudentCity,
                        StudentState = model.StudentState,
                        StudentZipCode = model.StudentZipCode,
                        StudentHomePhone = model.StudentHomePhone,
                        StudentCellPhone = model.StudentCellPhone,
                        StudentEmail = model.StudentEmail,
                        StudentCareerPath = model.StudentCareerPath,
                        StudentCareerInterest = model.StudentCareerInterest,
                        //Parent Info
                        ParentFirstName = model.ParentFirstName,
                        ParentLastName = model.ParentLastName,
                        Address = model.Address,
                        ParentApartmentNumber = model.ParentApartmentNumber,
                        ParentCity = model.ParentCity,
                        ParentState = model.ParentState,
                        ParentZipCode = model.ParentZipCode,
                        ParentHomePhone = model.ParentHomePhone,
                        ParentCellPhone = model.ParentCellPhone,
                        ParentEmail = model.ParentEmail,
                        //Guidance Counselor Info
                        KnowGuidanceCounselor = model.KnowGuidanceCounselor,
                        GuidanceCounselorName = model.GuidanceCounselorName,
                        MeetWithGuidanceCounselor = model.MeetWithGuidanceCounselor,
                        HowOftenMeetWithGuidanceCounselor = model.HowOftenMeetWithGuidanceCounselor,
                        DiscussWithGuidanceCounselor = model.DiscussWithGuidanceCounselor
                    };
                    var student = await _studentService.Update(list);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }


        /// <summary>
        /// This is an example of how to create roles
        /// </summary>
        /// <returns></returns>
        private async Task CreateDefaultRoles()
        {
            var hasAdmin = await _roleManager.FindByNameAsync(StringConstants.RoleNameAdmin);

            if (hasAdmin == null)
                await _roleManager.CreateAsync(new AspNetRoles() { 
                    Name = StringConstants.RoleNameAdmin,
                    NormalizedName = StringConstants.RoleNameAdmin
                });

            var hasCoach = await _roleManager.FindByNameAsync(StringConstants.RoleNameCoach);

            if (hasCoach == null)
                await _roleManager.CreateAsync(new AspNetRoles() {
                    Name = StringConstants.RoleNameCoach,
                    NormalizedName = StringConstants.RoleNameCoach
                });

            var hasStudent = await _roleManager.FindByNameAsync(StringConstants.RoleNameStudent);

            if (hasStudent == null)
                await _roleManager.CreateAsync(new AspNetRoles() {
                    Name = StringConstants.RoleNameStudent,
                    NormalizedName = StringConstants.RoleNameStudent
                });
        }

        /// <summary>
        /// THIS IS NOT CALLED WHEN A USER USES THE WEBSITE TO CREATE A USER
        /// 
        /// This is used for creating a user on the backend, programmatically
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private async Task CreateNewUser(string email, string password, string roleName, bool confirm = true)
        {
            var identityUser = new AspNetUsers() {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(identityUser, password);
            if(confirm)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                _ = _userManager.ConfirmEmailAsync(identityUser, token);
            }

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    var role = await _userManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    //Do something because the user does not exist
                }
            }
            else
            { 
                //Do something because the user could not be created
            }
        }
    }
}