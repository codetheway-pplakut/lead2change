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
using Lead2Change.Web.Ui.Models;

namespace Lead2Change.Web.Ui.Controllers
{
    public class StudentsController : _BaseController
    {
        RoleManager<AspNetRoles> _roleManager;
        IStudentService _studentService;
        UserManager<AspNetUsers> _userManager;
        SignInManager<AspNetUsers> _signInManager;

        public StudentsController(IUserService identityService, IStudentService studentService, RoleManager<AspNetRoles> roleManager, UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager) : base(identityService)
        {
            _roleManager = roleManager;
            _studentService = studentService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            await CreateDefaultRoles();
            await CreateNewUser("test0001@test.com", "Testtest@123", StringConstants.RoleNameStudent);
            await CreateNewUser("admin@admin.net", "Testtest@123", StringConstants.RoleNameAdmin);

            return View(await _studentService.GetStudents());
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await CanEditStudent(id))
            {
                // TODO: Change
                return RedirectToAction("Error", "Home");
            }

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
            RegistrationViewModel viewModel = new RegistrationViewModel()
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
            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel viewModel)
        {
            if (_signInManager.IsSignedIn(User) && ModelState.IsValid)
            {
                if (viewModel.StudentFirstName.Length > 0)
                {
                    Student model = new Student()
                    {
                        Id = viewModel.Id,
                        StudentFirstName = viewModel.StudentFirstName,
                        StudentLastName = viewModel.StudentLastName,
                        StudentDateOfBirth = viewModel.StudentDateOfBirth,
                        StudentAddress = viewModel.StudentAddress,
                        StudentApartmentNumber = viewModel.StudentApartmentNumber,
                        StudentCity = viewModel.StudentCity,
                        StudentState = viewModel.StudentState,
                        StudentZipCode = viewModel.StudentZipCode,
                        StudentHomePhone = viewModel.StudentHomePhone,
                        StudentCellPhone = viewModel.StudentCellPhone,
                        StudentEmail = viewModel.StudentEmail,
                        StudentCareerPath = viewModel.StudentCareerPath,
                        StudentCareerInterest = viewModel.StudentCareerInterest,
                        ParentFirstName = viewModel.ParentFirstName,
                        ParentLastName = viewModel.ParentLastName,
                        Address = viewModel.Address,
                        //ParentAdress
                        ParentApartmentNumber = viewModel.ParentApartmentNumber,
                        ParentCity = viewModel.ParentCity,
                        ParentState = viewModel.ParentState,
                        ParentZipCode = viewModel.ParentZipCode,
                        ParentHomePhone = viewModel.ParentHomePhone,
                        ParentCellPhone = viewModel.ParentCellPhone,
                        ParentEmail = viewModel.ParentEmail,
                        KnowGuidanceCounselor = viewModel.KnowGuidanceCounselor,
                        GuidanceCounselorName = viewModel.GuidanceCounselorName,
                        MeetWithGuidanceCounselor = viewModel.MeetWithGuidanceCounselor,
                        HowOftenMeetWithGuidanceCounselor = viewModel.HowOftenMeetWithGuidanceCounselor,
                        DiscussWithGuidanceCounselor = viewModel.DiscussWithGuidanceCounselor
                    };
                    var student = await _studentService.Create(model);

                    // Registers a relation in user to studentId
                    var user = await _userManager.GetUserAsync(User);
                    user.StudentId = student.Id;
                    await _userManager.UpdateAsync(user);
                }
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _studentService.GetStudent(id);

            if (student == null || !await CanEditStudent(id))
            {
                // TODO: Change
                return RedirectToAction("Error", "Home");
            }

            RegistrationViewModel viewModel = new RegistrationViewModel()
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
            return View(viewModel);
        }

        public async Task<IActionResult> Update(RegistrationViewModel viewModel)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!await CanEditStudent(viewModel.Id))
            {
                // TODO: Change
                return RedirectToAction("Error", "Home");
            }

            if (ModelState.IsValid)
            {
                if (viewModel.StudentFirstName.Length > 0)
                {
                    Student model = new Student()
                    {
                        Id = viewModel.Id,
                        //General Student Info
                        StudentFirstName = viewModel.StudentFirstName,
                        StudentLastName = viewModel.StudentLastName,
                        StudentDateOfBirth = viewModel.StudentDateOfBirth,
                        StudentAddress = viewModel.StudentAddress,
                        StudentApartmentNumber = viewModel.StudentApartmentNumber,
                        StudentCity = viewModel.StudentCity,
                        StudentState = viewModel.StudentState,
                        StudentZipCode = viewModel.StudentZipCode,
                        StudentHomePhone = viewModel.StudentHomePhone,
                        StudentCellPhone = viewModel.StudentCellPhone,
                        StudentEmail = viewModel.StudentEmail,
                        StudentCareerPath = viewModel.StudentCareerPath,
                        StudentCareerInterest = viewModel.StudentCareerInterest,
                        //Parent Info
                        ParentFirstName = viewModel.ParentFirstName,
                        ParentLastName = viewModel.ParentLastName,
                        Address = viewModel.Address,
                        ParentApartmentNumber = viewModel.ParentApartmentNumber,
                        ParentCity = viewModel.ParentCity,
                        ParentState = viewModel.ParentState,
                        ParentZipCode = viewModel.ParentZipCode,
                        ParentHomePhone = viewModel.ParentHomePhone,
                        ParentCellPhone = viewModel.ParentCellPhone,
                        ParentEmail = viewModel.ParentEmail,
                        //Guidance Counselor Info
                        KnowGuidanceCounselor = viewModel.KnowGuidanceCounselor,
                        GuidanceCounselorName = viewModel.GuidanceCounselorName,
                        MeetWithGuidanceCounselor = viewModel.MeetWithGuidanceCounselor,
                        HowOftenMeetWithGuidanceCounselor = viewModel.HowOftenMeetWithGuidanceCounselor,
                        DiscussWithGuidanceCounselor = viewModel.DiscussWithGuidanceCounselor
                    };
                    var student = await _studentService.Update(model);
                }
                return RedirectToAction("Index");
            }
            return View(viewModel);
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
        
        private async Task<bool> CanEditStudent(Guid studentId)
        {
            if (_signInManager.IsSignedIn(User))
            {
                if(User.IsInRole(StringConstants.RoleNameStudent))
                {
                    var user = await _userManager.GetUserAsync(User);
                    return studentId == user.StudentId;
                }
                else if (User.IsInRole(StringConstants.RoleNameCoach))
                {
                    // TODO: Return only if coach "owns" this student
                    return true;
                }
                else if (User.IsInRole(StringConstants.RoleNameAdmin))
                {
                    return true;
                }
            }
            return false;
        }
    }
}