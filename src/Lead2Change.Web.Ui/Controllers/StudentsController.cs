using Lead2Change.Domain.ViewModels;
using Lead2Change.Services.Identity;
using Lead2Change.Services.Students;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Lead2Change.Web.Ui.Models;
using Lead2Change.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Lead2Change.Web.Ui.Controllers
{
    public class StudentsController : _BaseController
    {
        IStudentService _studentService;

        public StudentsController(IUserService identityService, IStudentService studentService, RoleManager<AspNetRoles> roleManager, UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager) : base(identityService, roleManager, userManager, signInManager)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            if (!SignInManager.IsSignedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }

            if (User.IsInRole(StringConstants.RoleNameStudent))
            {
                return Error("403: You are not authorized to view this page.");
            }

            var user = await UserManager.GetUserAsync(User);

            if (User.IsInRole(StringConstants.RoleNameCoach))
            {
                // StudentId being used as assosiation to coach

                // TODO: Replace to use list of students in coach model
                // return View(await _studentService.GetStudentsByCoachId(user.StudentId));
                return Error("Coach access of index is not currently functioning");
            }
            else if (User.IsInRole(StringConstants.RoleNameAdmin))
            {
                return View(await _studentService.GetStudents());
            }

            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await CanEditStudent(id))
            {
                return Error("403: You are not authorized to delete this student.");
            }

            var student = await _studentService.GetStudent(id);
            await _studentService.Delete(student);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Register()
        {
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("403: Must be signed in to access the student registration form.");
            }

            var user = await UserManager.GetUserAsync(User);

            if (user == null || user.StudentId == Guid.Empty)
            {
                return Error("403: Not authorized to view this student.");
            }

            return View(new RegistrationViewModel());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Register");
            }

            var student = await _studentService.GetStudent(id);

            if (!await CanEditStudent(id) || student == null)
            {
                return Error("403: Not authorized to view this student.");
            }

            RegistrationViewModel viewModel = new RegistrationViewModel()
            {
                Id = student.Id,
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

                KnowGuidanceCounselor = student.KnowGuidanceCounselor,
                GuidanceCounselorName = student.GuidanceCounselorName,
                MeetWithGuidanceCounselor = student.MeetWithGuidanceCounselor,
                HowOftenMeetWithGuidanceCounselor = student.HowOftenMeetWithGuidanceCounselor,
                DiscussWithGuidanceCounselor = student.DiscussWithGuidanceCounselor
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel viewModel)
        {
            if (SignInManager.IsSignedIn(User) && ModelState.IsValid)
            {
                var user = await UserManager.GetUserAsync(User);
                if (user.StudentId == Guid.Empty)
                {
                    return Error("Cannot register a user.");
                }
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
                    user.StudentId = student.Id;
                    await UserManager.UpdateAsync(user);
                }
                return RedirectToAction("Details");
            }
            return Error("403: Not signed in, cannot register a user.");
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _studentService.GetStudent(id);

            if (student == null || !await CanEditStudent(id))
            {
                return Error("403: Not authorized to edit the details of this student or this student does not exist.");
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
            var user = await UserManager.GetUserAsync(User);

            if (!await CanEditStudent(viewModel.Id))
            {
                return Error("403: Not authorized to edit the details of this student.");
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
    }
}