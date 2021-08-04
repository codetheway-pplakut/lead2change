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
using Lead2Change.Services.Coaches;

namespace Lead2Change.Web.Ui.Controllers
{
    public class StudentsController : _BaseController
    {
        IStudentService _studentService;
        ICoachService _coachService;

        public StudentsController(IUserService identityService, IStudentService studentService, ICoachService coachService, RoleManager<AspNetRoles> roleManager, UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager) : base(identityService, roleManager, userManager, signInManager)
        {
            _studentService = studentService;
            _coachService = coachService;
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

                return View(await _studentService.GetActiveStudents());

            }

            return View();
        }

        public async Task<IActionResult> InactiveIndex()
        {
            if (User.IsInRole(StringConstants.RoleNameAdmin))

            {

                return View(await _studentService.GetInactiveStudents());

            }

            return Error("403: You are not authorized to view this page.");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            // Check SignedIn
            if (!SignInManager.IsSignedIn(User))

            {

                return Error("401: Unauthorized");

            }

            // Check Permissions
            /*

             *  Students: Not Allowed

             *  Coach: Not Allowed

             *  Admin: Allowed

             */
            if (!User.IsInRole(StringConstants.RoleNameAdmin))

            {

                return Error("403: Forbidden");

            }

            // Find Student
            var student = await _studentService.GetStudent(id);

            // Check for bad id or student
            if (id == null || student == null)

            {

                return Error("400: Bad Request");

            }

            // Delete Student
            await _studentService.Delete(student);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Register()
        {
            // Check SignedIn
            if (!SignInManager.IsSignedIn(User))

            {

                return Error("401: Unauthorized");

            }



            // Check Permissions

            /*

             *  Students: Allowed

             *  Coach: Not Allowed

             *  Admin: Allowed

             */

            if (User.IsInRole(StringConstants.RoleNameCoach))

            {

                return Error("403: Forbidden");

            }



            // Find user

            var user = await UserManager.GetUserAsync(User);



            // Check if user owns a student

            if (user.StudentId != Guid.Empty)

            {

                return RedirectToAction("Details", new { studentId = user.StudentId });

            }

            return View(new RegistrationViewModel()
            {
                // This changes the initial date displayed in the chooser
                StudentDateOfBirth = DateTime.Today,
                PACTTestDate = DateTime.Today,
                PSATTestDate = DateTime.Today,
                SATTestDate = DateTime.Today,
                ACTTestDate = DateTime.Today,
                StudentSignatureDate = DateTime.Today,
                ParentSignatureDate = DateTime.Today,
            }) ;
        }

        public async Task<IActionResult> Details(Guid studentId)
        {

            // Check SignedIn

            if (!SignInManager.IsSignedIn(User))

            {

                return Error("401: Unauthorized");

            }



            // Redirect to Register if Guid is empty

            if (studentId == Guid.Empty)

            {

                return RedirectToAction("Register");

            }



            // Check Permissions

            /*

             *  Students: Allowed only if it is them

             *  Coach: Allowed only if student is owned by coach

             *  Admin: Allowed

             */

            // TODO: Error only if coach does not own the student

            if (User.IsInRole(StringConstants.RoleNameCoach))

            {

                return Error("403: Forbidden");

            }
            else if (User.IsInRole(StringConstants.RoleNameStudent))

            {

                // Find user

                var user = await UserManager.GetUserAsync(User);



                // Check that studentId is the AssociatedId of the user

                if (user.StudentId == Guid.Empty || user.StudentId != studentId)

                {

                    return Error("403: Forbidden");

                }

            }



            // Find Student

            var student = await _studentService.GetStudent(studentId);



            // Check for bad student

            if (student == null)

            {

                return Error("400: Bad Request");

            }
            
            //check for null
            //Caclculate age:
            var coachcontainer = new Coach();
            if (student.CoachId.HasValue)
            {
                coachcontainer = await _coachService.GetCoach(student.CoachId.Value);
            }
            var age = DateTime.Now.Year - student.StudentDateOfBirth.Year;
            if(DateTime.Now.Month < student.StudentDateOfBirth.Month)
            {
                age--;
            }
            if(DateTime.Now.Month == student.StudentDateOfBirth.Month)
            {
                if(DateTime.Now.Day < student.StudentDateOfBirth.Day)
                {
                    age--;
                }
            }
            // Create a new viewModel
            RegistrationViewModel viewModel = new RegistrationViewModel()
            {
                Id = student.Id,
                StudentFirstName = student.StudentFirstName,
                StudentLastName = student.StudentLastName,
                StudentDateOfBirth = student.StudentDateOfBirth,
                Age = age,
                //using calculated age above
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
                CoachName = student.CoachId.HasValue ? coachcontainer.CoachFirstName + " " + coachcontainer.CoachLastName : "Unassigned",

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
                DiscussWithGuidanceCounselor = student.DiscussWithGuidanceCounselor,
                PlanAfterHighSchool = student.PlanAfterHighSchool,
                CollegeApplicationStatus = student.CollegeApplicationStatus,
                CollegesList = student.CollegesList,
                CollegeEssayStatus = student.CollegeEssayStatus,
                CollegeEssayHelp = student.CollegeEssayHelp,
                FirstChoiceCollege = student.FirstChoiceCollege,
                SecondChoiceCollege = student.SecondChoiceCollege,
                ThirdChoiceCollege = student.ThirdChoiceCollege,
                TradeSchoolStatus = student.TradeSchoolStatus,
                TradeSchoolsList = student.TradeSchoolsList,
                ArmedForcesStatus = student.ArmedForcesStatus,
                ArmedForcesBranch = student.ArmedForcesBranch,
                WorkStatus = student.WorkStatus,
                CareerPathList = student.CareerPathList,
                OtherPlans = student.OtherPlans,
                PACTTestDate = student.PACTTestDate,
                PACTTestScore = student.PACTTestScore,
                PSATTestDate = student.PSATTestDate,
                PSATTestScore = student.PSATTestScore,
                SATTestDate = student.SATTestDate,
                SATTestScore = student.SATTestScore,
                ACTTestDate = student.ACTTestDate,
                ACTTestScore = student.ACTTestScore,
                PrepClassRequired = student.PrepClassRequired,
                AssistanceForForms = student.AssistanceForForms,
                FinancialAidProcessComplete = student.FinancialAidProcessComplete,
                SupportNeeded = student.SupportNeeded,
                StudentSignature = student.StudentSignature,
                StudentSignatureDate = student.StudentSignatureDate,
                ParentSignature = student.ParentSignature,
                ParentSignatureDate = student.ParentSignatureDate,
                CoachId = student.CoachId,
                Active = student.Active
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel viewModel)
        {

            // Check SignedIn

            if (!SignInManager.IsSignedIn(User))

            {

                return Error("401: Unauthorized");

            }



            // Check Permissions

            /*

             *  Students: Allowed

             *  Coach: Not Allowed

             *  Admin: Allowed

             */

            if (User.IsInRole(StringConstants.RoleNameCoach))

            {

                return Error("403: Forbidden");

            }



            // Find User

            var user = await UserManager.GetUserAsync(User);



            if (

                // Check if the user is null

                user == null ||

                // Check if user already has a student assosiation

                user.StudentId != Guid.Empty ||

                // Check for bad viewModel

                !ModelState.IsValid ||

                // Check the length of the first name

                viewModel.StudentFirstName.Length <= 0

                )

            {

                return Error("400: Bad Request");

            }

            // Create model
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
                CoachId = null, // set to unlisted/unknown
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
                DiscussWithGuidanceCounselor = viewModel.DiscussWithGuidanceCounselor,
                PlanAfterHighSchool = viewModel.PlanAfterHighSchool,
                CollegeApplicationStatus = viewModel.CollegeApplicationStatus,
                CollegesList = viewModel.CollegesList,
                CollegeEssayStatus = viewModel.CollegeEssayStatus,
                CollegeEssayHelp = viewModel.CollegeEssayHelp,
                FirstChoiceCollege = viewModel.FirstChoiceCollege,
                SecondChoiceCollege = viewModel.SecondChoiceCollege,
                ThirdChoiceCollege = viewModel.ThirdChoiceCollege,
                TradeSchoolStatus = viewModel.TradeSchoolStatus,
                TradeSchoolsList = viewModel.TradeSchoolsList,
                ArmedForcesStatus = viewModel.ArmedForcesStatus,
                ArmedForcesBranch = viewModel.ArmedForcesBranch,
                WorkStatus = viewModel.WorkStatus,
                CareerPathList = viewModel.CareerPathList,
                OtherPlans = viewModel.OtherPlans,
                PACTTestDate = viewModel.PACTTestDate,
                PACTTestScore = viewModel.PACTTestScore,
                PSATTestDate = viewModel.PSATTestDate,
                PSATTestScore = viewModel.PSATTestScore,
                SATTestDate = viewModel.SATTestDate,
                SATTestScore = viewModel.SATTestScore,
                ACTTestDate = viewModel.ACTTestDate,
                ACTTestScore = viewModel.ACTTestScore,
                PrepClassRequired = viewModel.PrepClassRequired,
                AssistanceForForms = viewModel.AssistanceForForms,
                FinancialAidProcessComplete = viewModel.FinancialAidProcessComplete,
                SupportNeeded = viewModel.SupportNeeded,
                StudentSignature = viewModel.StudentSignature,
                StudentSignatureDate = viewModel.StudentSignatureDate,
                ParentSignature = viewModel.ParentSignature,
                ParentSignatureDate = viewModel.ParentSignatureDate,
                Active = true,
            };
            
            // Add model
            var student = await _studentService.Create(model);

            // Registers a relation in user to the student if their role is student
            if (User.IsInRole(StringConstants.RoleNameStudent))

            {

                user.StudentId = student.Id;

                await UserManager.UpdateAsync(user);
            }
            
            await Email("1joel.kuriakose@gmail.com", model.ParentEmail, "Lead2Change Registration Confirmation: Your student is registered ", "Your student " + model.StudentFirstName + " " + model.StudentLastName + " has registered for Lead2Change!", "Your student " + model.StudentFirstName + " " + model.StudentLastName + " has registered for Lead2Change!", "Lead2Change Student Registration", model.ParentFirstName + " " + model.ParentLastName);
            await Email("1joel.kuriakose@gmail.com", "joeljk2003@gmail.com", "Lead2Change Student Registration Confirmation: A new student has been registered", model.StudentFirstName + " " + model.StudentLastName + " is a new registered student in Lead2Change!", model.StudentFirstName + " " + model.StudentLastName + " is a new registered student in Lead2Change!", "Lead2Change Student Registration", "Lead2Change");
            await Email("1joel.kuriakose@gmail.com", model.StudentEmail, "Lead2Change Registration Confirmation: You are registered", "Congrats, you have sucessfully registered for Lead2Change!", "Congrats, you have sucessfully registered for Lead2Change!", "Lead2Change Student Registration", model.StudentFirstName + " " + model.StudentLastName);

            return RedirectToAction("Details", new { studentId = student.Id });
        }


        public async Task<IActionResult> Edit(Guid studentId)
        {
            // Check SignedIn

            if (!SignInManager.IsSignedIn(User))

            {

                return Error("401: Unauthorized");

            }



            // Check Permissions

            /*

             *  Students: Allowed if owned by user

             *  Coach: Not Allowed

             *  Admin: Allowed

             */

            if (User.IsInRole(StringConstants.RoleNameStudent))

            {

                // Find user

                var user = await UserManager.GetUserAsync(User);



                // Check that studentId is the AssociatedId of the user

                if (user.StudentId == Guid.Empty || user.StudentId != studentId)

                {

                    return Error("403: Forbidden");

                }

            }

            else if (User.IsInRole(StringConstants.RoleNameCoach))

            {

                return Error("403: Forbidden");

            }



            // Find student

            var student = await _studentService.GetStudent(studentId);



            // Check for bad student

            if (student == null || studentId == Guid.Empty)

            {

                return Error("400: Bad Request");

            }

            EditViewModel viewModel = new EditViewModel()
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
                OldStudentEmail = student.StudentEmail,
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
                OldParentEmail = student.ParentEmail,
                //Guidance Counselor Info
                KnowGuidanceCounselor = student.KnowGuidanceCounselor,
                GuidanceCounselorName = student.GuidanceCounselorName,
                MeetWithGuidanceCounselor = student.MeetWithGuidanceCounselor,
                HowOftenMeetWithGuidanceCounselor = student.HowOftenMeetWithGuidanceCounselor,
                DiscussWithGuidanceCounselor = student.DiscussWithGuidanceCounselor,
                PlanAfterHighSchool = student.PlanAfterHighSchool,
                CollegeApplicationStatus = student.CollegeApplicationStatus,
                CollegesList = student.CollegesList,
                CollegeEssayStatus = student.CollegeEssayStatus,
                CollegeEssayHelp = student.CollegeEssayHelp,
                FirstChoiceCollege = student.FirstChoiceCollege,
                SecondChoiceCollege = student.SecondChoiceCollege,
                ThirdChoiceCollege = student.ThirdChoiceCollege,
                TradeSchoolStatus = student.TradeSchoolStatus,
                TradeSchoolsList = student.TradeSchoolsList,
                ArmedForcesStatus = student.ArmedForcesStatus,
                ArmedForcesBranch = student.ArmedForcesBranch,
                WorkStatus = student.WorkStatus,
                CareerPathList = student.CareerPathList,
                OtherPlans = student.OtherPlans,
                PACTTestDate = student.PACTTestDate == DateTime.MinValue ? DateTime.Now : student.PACTTestDate,
                PACTTestScore = student.PACTTestScore,
                PSATTestDate = student.PSATTestDate == DateTime.MinValue ? DateTime.Now : student.PSATTestDate,
                PSATTestScore = student.PSATTestScore,
                SATTestDate = student.SATTestDate == DateTime.MinValue ? DateTime.Now : student.SATTestDate,
                SATTestScore = student.SATTestScore,
                ACTTestDate = student.ACTTestDate == DateTime.MinValue ? DateTime.Now : student.ACTTestDate,
                ACTTestScore = student.ACTTestScore,
                PrepClassRequired = student.PrepClassRequired,
                AssistanceForForms = student.AssistanceForForms,
                FinancialAidProcessComplete = student.FinancialAidProcessComplete,
                SupportNeeded = student.SupportNeeded,
                StudentSignature = student.StudentSignature,
                StudentSignatureDate = student.StudentSignatureDate == DateTime.MinValue ? DateTime.Now : student.StudentSignatureDate,
                ParentSignature = student.ParentSignature,
                ParentSignatureDate = student.ParentSignatureDate == DateTime.MinValue ? DateTime.Now : student.ParentSignatureDate,
                CoachId = student.CoachId,
                Active = student.Active
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Update(RegistrationViewModel viewModel)
        {
            // Check SignedIn

            if (!SignInManager.IsSignedIn(User))

            {

                return Error("401: Unauthorized");

            }



            // Check Permissions

            /*

             *  Students: Allowed if owned by user

             *  Coach: Not Allowed

             *  Admin: Allowed

             */

            if (User.IsInRole(StringConstants.RoleNameStudent))

            {

                // Find user

                var user = await UserManager.GetUserAsync(User);



                // Check that studentId is the AssociatedId of the user

                if (user.StudentId == Guid.Empty || user.StudentId != viewModel.Id)

                {

                    return Error("403: Forbidden");

                }

            }

            else if (User.IsInRole(StringConstants.RoleNameCoach))

            {

                return Error("403: Forbidden");

            }



            if (
                // Check for bad model state
                !ModelState.IsValid
                )
            {
                return Error("400: Bad Request");
            }

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
                DiscussWithGuidanceCounselor = viewModel.DiscussWithGuidanceCounselor,
                PlanAfterHighSchool = viewModel.PlanAfterHighSchool,
                CollegeApplicationStatus = viewModel.CollegeApplicationStatus,
                CollegesList = viewModel.CollegesList,
                CollegeEssayStatus = viewModel.CollegeEssayStatus,
                CollegeEssayHelp = viewModel.CollegeEssayHelp,
                FirstChoiceCollege = viewModel.FirstChoiceCollege,
                SecondChoiceCollege = viewModel.SecondChoiceCollege,
                ThirdChoiceCollege = viewModel.ThirdChoiceCollege,
                TradeSchoolStatus = viewModel.TradeSchoolStatus,
                TradeSchoolsList = viewModel.TradeSchoolsList,
                ArmedForcesStatus = viewModel.ArmedForcesStatus,
                ArmedForcesBranch = viewModel.ArmedForcesBranch,
                WorkStatus = viewModel.WorkStatus,
                CareerPathList = viewModel.CareerPathList,
                OtherPlans = viewModel.OtherPlans,
                PACTTestDate = viewModel.PACTTestDate,
                PACTTestScore = viewModel.PACTTestScore,
                PSATTestDate = viewModel.PSATTestDate,
                PSATTestScore = viewModel.PSATTestScore,
                SATTestDate = viewModel.SATTestDate,
                SATTestScore = viewModel.SATTestScore,
                ACTTestDate = viewModel.ACTTestDate,
                ACTTestScore = viewModel.ACTTestScore,
                PrepClassRequired = viewModel.PrepClassRequired,
                AssistanceForForms = viewModel.AssistanceForForms,
                FinancialAidProcessComplete = viewModel.FinancialAidProcessComplete,
                SupportNeeded = viewModel.SupportNeeded,
                StudentSignature = viewModel.StudentSignature,
                StudentSignatureDate = viewModel.StudentSignatureDate,
                ParentSignature = viewModel.ParentSignature,
                ParentSignatureDate = viewModel.ParentSignatureDate,
                CoachId = viewModel.CoachId,
                Active = viewModel.Active
            };

            var student = await _studentService.Update(model);
            /*if (model.StudentEmail != model.OldStudentEmail)
            {
                model.OldStudentEmail = model.StudentEmail;
                await Email("1joel.kuriakose@gmail.com", model.StudentEmail, "Lead2Change Update Confirmation: Your student email has been changed to this email", "Your email has been updated in the Lead2Change database!", "Your email has been updated in the Lead2Change database!", "Lead2Change", model.StudentFirstName + " " + model.StudentLastName);
            }
            if (model.ParentEmail != model.OldParentEmail)
            {
                await Email("1joel.kuriakose@gmail.com", model.ParentEmail, "Lead2Change Update Confirmation: Your parent email has been changed to this email", "Your email has been updated in the Lead2Change database!", "Your email has been updated in the Lead2Change database!", "Lead2Change", model.ParentFirstName + " " + model.ParentLastName);
                model.OldParentEmail = model.ParentEmail;
            }*/

            return RedirectToAction("Edit", new { studentId = student.Id });
        }
        public async Task<IActionResult> InterestForm()
        {
            // Check SignedIn
            if (!SignInManager.IsSignedIn(User))

            {

                return Error("401: Unauthorized");

            }



            // Check Permissions

            /*

             *  Students: Allowed

             *  Coach: Not Allowed

             *  Admin: Allowed

             */

            if (User.IsInRole(StringConstants.RoleNameCoach))

            {

                return Error("403: Forbidden");

            }



            // Find user

            var user = await UserManager.GetUserAsync(User);



            // Check if user owns a student

            if (user.StudentId != Guid.Empty)

            {

                return RedirectToAction("Details", new { studentId = user.StudentId });

            }

            return View(new StudentInterestFormViewModel()
            {
                // This changes the initial date displayed in the chooser
                StudentDateOfBirth = DateTime.Today,
            });
        }
        public async Task<IActionResult> RegisterInterest(StudentInterestFormViewModel viewModel)
        {

            // Check SignedIn

            if (!SignInManager.IsSignedIn(User))

            {

                return Error("401: Unauthorized");

            }



            // Check Permissions

            /*

             *  Students: Allowed

             *  Coach: Not Allowed

             *  Admin: Allowed

             */

            if (User.IsInRole(StringConstants.RoleNameCoach))

            {

                return Error("403: Forbidden");

            }



            // Find User

            var user = await UserManager.GetUserAsync(User);



            if (

                // Check if the user is null

                user == null ||

                // Check if user already has a student assosiation

                user.StudentId != Guid.Empty ||

                // Check for bad viewModel

                !ModelState.IsValid ||

                // Check the length of the first name

                viewModel.StudentFirstName.Length <= 0

                )

            {

                return Error("400: Bad Request");

            }

            // Create model
            Student model = new Student()
            {
                Id = viewModel.Id,
                Active = true,
                StudentFirstName = viewModel.StudentFirstName,
                StudentLastName = viewModel.StudentLastName,
                StudentDateOfBirth = viewModel.StudentDateOfBirth,
                StudentCellPhone = viewModel.StudentCellPhone,
                StudentEmail = viewModel.StudentEmail,
                Active = true,
            };

            // Add model
            var student = await _studentService.Create(model);

            // Registers a relation in user to the student if their role is student
            if (User.IsInRole(StringConstants.RoleNameStudent))

            {

                user.StudentId = student.Id;

                await UserManager.UpdateAsync(user);
            }

            await Email("1joel.kuriakose@gmail.com", model.ParentEmail, "Lead2Change Registration Confirmation: Your student is registered ", "Your student " + model.StudentFirstName + " " + model.StudentLastName + " has registered for Lead2Change!", "Your student " + model.StudentFirstName + " " + model.StudentLastName + " has registered for Lead2Change!", "Lead2Change Student Registration", model.ParentFirstName + " " + model.ParentLastName);
            await Email("1joel.kuriakose@gmail.com", "joeljk2003@gmail.com", "Lead2Change Student Registration Confirmation: A new student has been registered", model.StudentFirstName + " " + model.StudentLastName + " is a new registered student in Lead2Change!", model.StudentFirstName + " " + model.StudentLastName + " is a new registered student in Lead2Change!", "Lead2Change Student Registration", "Lead2Change");
            await Email("1joel.kuriakose@gmail.com", model.StudentEmail, "Lead2Change Registration Confirmation: You are registered", "Congrats, you have sucessfully registered for Lead2Change!", "Congrats, you have sucessfully registered for Lead2Change!", "Lead2Change Student Registration", model.StudentFirstName + " " + model.StudentLastName);

            return RedirectToAction("ThankYou");
        }
        public async Task<IActionResult> ThankYou()
        {
            return View();
        }
        public async Task<IActionResult> StudentInformationEdit(Guid studentId)
        {
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("401: Unauthorized");
            }
            // Check Permissions
            /*
             *  Students: Allowed if owned by user
             *  Coach: Not Allowed
             *  Admin: Allowed
             */
            if (User.IsInRole(StringConstants.RoleNameStudent))
            {
                // Find user
                var user = await UserManager.GetUserAsync(User);
                // Check that studentId is the AssociatedId of the user

                if (user.StudentId == Guid.Empty || user.StudentId != studentId)

                {
                    return Error("403: Forbidden");
                }
            }
            else if (User.IsInRole(StringConstants.RoleNameCoach))

            {
                return Error("403: Forbidden");
            }
            // Find student
            var student = await _studentService.GetStudent(studentId);
            // Check for bad student

            if (student == null || studentId == Guid.Empty)

            {
                return Error("400: Bad Request");
            }
            EditViewModel viewModel = new EditViewModel()
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
                OldStudentEmail = student.StudentEmail,
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
                OldParentEmail = student.ParentEmail,
                //Guidance Counselor Info
                KnowGuidanceCounselor = student.KnowGuidanceCounselor,
                GuidanceCounselorName = student.GuidanceCounselorName,
                MeetWithGuidanceCounselor = student.MeetWithGuidanceCounselor,
                HowOftenMeetWithGuidanceCounselor = student.HowOftenMeetWithGuidanceCounselor,
                DiscussWithGuidanceCounselor = student.DiscussWithGuidanceCounselor,
                PlanAfterHighSchool = student.PlanAfterHighSchool,
                CollegeApplicationStatus = student.CollegeApplicationStatus,
                CollegesList = student.CollegesList,
                CollegeEssayStatus = student.CollegeEssayStatus,
                CollegeEssayHelp = student.CollegeEssayHelp,
                FirstChoiceCollege = student.FirstChoiceCollege,
                SecondChoiceCollege = student.SecondChoiceCollege,
                ThirdChoiceCollege = student.ThirdChoiceCollege,
                TradeSchoolStatus = student.TradeSchoolStatus,
                TradeSchoolsList = student.TradeSchoolsList,
                ArmedForcesStatus = student.ArmedForcesStatus,
                ArmedForcesBranch = student.ArmedForcesBranch,
                WorkStatus = student.WorkStatus,
                CareerPathList = student.CareerPathList,
                OtherPlans = student.OtherPlans,
                PACTTestDate = student.PACTTestDate,
                PACTTestScore = student.PACTTestScore,
                PSATTestDate = student.PSATTestDate,
                PSATTestScore = student.PSATTestScore,
                SATTestDate = student.SATTestDate,
                SATTestScore = student.SATTestScore,
                ACTTestDate = student.ACTTestDate,
                ACTTestScore = student.ACTTestScore,
                PrepClassRequired = student.PrepClassRequired,
                AssistanceForForms = student.AssistanceForForms,
                FinancialAidProcessComplete = student.FinancialAidProcessComplete,
                SupportNeeded = student.SupportNeeded,
                StudentSignature = student.StudentSignature,
                StudentSignatureDate = student.StudentSignatureDate,
                ParentSignature = student.ParentSignature,
                ParentSignatureDate = student.ParentSignatureDate,
                CoachId = student.CoachId,
                Active = student.Active
            };

            return View(viewModel);
        }
        public async Task<IActionResult> ParentInformationEdit(Guid studentId)
        {
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("401: Unauthorized");
            }
            // Check Permissions
            /*
             *  Students: Allowed if owned by user
             *  Coach: Not Allowed
             *  Admin: Allowed
             */
            if (User.IsInRole(StringConstants.RoleNameStudent))
            {
                // Find user
                var user = await UserManager.GetUserAsync(User);
                // Check that studentId is the AssociatedId of the user

                if (user.StudentId == Guid.Empty || user.StudentId != studentId)

                {
                    return Error("403: Forbidden");
                }
            }
            else if (User.IsInRole(StringConstants.RoleNameCoach))

            {
                return Error("403: Forbidden");
            }
            // Find student
            var student = await _studentService.GetStudent(studentId);
            // Check for bad student

            if (student == null || studentId == Guid.Empty)

            {
                return Error("400: Bad Request");
            }
            EditViewModel viewModel = new EditViewModel()
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
                OldStudentEmail = student.StudentEmail,
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
                OldParentEmail = student.ParentEmail,
                //Guidance Counselor Info
                KnowGuidanceCounselor = student.KnowGuidanceCounselor,
                GuidanceCounselorName = student.GuidanceCounselorName,
                MeetWithGuidanceCounselor = student.MeetWithGuidanceCounselor,
                HowOftenMeetWithGuidanceCounselor = student.HowOftenMeetWithGuidanceCounselor,
                DiscussWithGuidanceCounselor = student.DiscussWithGuidanceCounselor,
                PlanAfterHighSchool = student.PlanAfterHighSchool,
                CollegeApplicationStatus = student.CollegeApplicationStatus,
                CollegesList = student.CollegesList,
                CollegeEssayStatus = student.CollegeEssayStatus,
                CollegeEssayHelp = student.CollegeEssayHelp,
                FirstChoiceCollege = student.FirstChoiceCollege,
                SecondChoiceCollege = student.SecondChoiceCollege,
                ThirdChoiceCollege = student.ThirdChoiceCollege,
                TradeSchoolStatus = student.TradeSchoolStatus,
                TradeSchoolsList = student.TradeSchoolsList,
                ArmedForcesStatus = student.ArmedForcesStatus,
                ArmedForcesBranch = student.ArmedForcesBranch,
                WorkStatus = student.WorkStatus,
                CareerPathList = student.CareerPathList,
                OtherPlans = student.OtherPlans,
                PACTTestDate = student.PACTTestDate,
                PACTTestScore = student.PACTTestScore,
                PSATTestDate = student.PSATTestDate,
                PSATTestScore = student.PSATTestScore,
                SATTestDate = student.SATTestDate,
                SATTestScore = student.SATTestScore,
                ACTTestDate = student.ACTTestDate,
                ACTTestScore = student.ACTTestScore,
                PrepClassRequired = student.PrepClassRequired,
                AssistanceForForms = student.AssistanceForForms,
                FinancialAidProcessComplete = student.FinancialAidProcessComplete,
                SupportNeeded = student.SupportNeeded,
                StudentSignature = student.StudentSignature,
                StudentSignatureDate = student.StudentSignatureDate,
                ParentSignature = student.ParentSignature,
                ParentSignatureDate = student.ParentSignatureDate,
                CoachId = student.CoachId,
                Active = student.Active
            };

            return View(viewModel);
        }
        public async Task<IActionResult> GuidanceCounselorInformation(Guid studentId)
        {
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("401: Unauthorized");
            }
            // Check Permissions
            /*
             *  Students: Allowed if owned by user
             *  Coach: Not Allowed
             *  Admin: Allowed
             */
            if (User.IsInRole(StringConstants.RoleNameStudent))
            {
                // Find user
                var user = await UserManager.GetUserAsync(User);
                // Check that studentId is the AssociatedId of the user

                if (user.StudentId == Guid.Empty || user.StudentId != studentId)

                {
                    return Error("403: Forbidden");
                }
            }
            else if (User.IsInRole(StringConstants.RoleNameCoach))

            {
                return Error("403: Forbidden");
            }
            // Find student
            var student = await _studentService.GetStudent(studentId);
            // Check for bad student

            if (student == null || studentId == Guid.Empty)

            {
                return Error("400: Bad Request");
            }
            EditViewModel viewModel = new EditViewModel()
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
                OldStudentEmail = student.StudentEmail,
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
                OldParentEmail = student.ParentEmail,
                //Guidance Counselor Info
                KnowGuidanceCounselor = student.KnowGuidanceCounselor,
                GuidanceCounselorName = student.GuidanceCounselorName,
                MeetWithGuidanceCounselor = student.MeetWithGuidanceCounselor,
                HowOftenMeetWithGuidanceCounselor = student.HowOftenMeetWithGuidanceCounselor,
                DiscussWithGuidanceCounselor = student.DiscussWithGuidanceCounselor,
                PlanAfterHighSchool = student.PlanAfterHighSchool,
                CollegeApplicationStatus = student.CollegeApplicationStatus,
                CollegesList = student.CollegesList,
                CollegeEssayStatus = student.CollegeEssayStatus,
                CollegeEssayHelp = student.CollegeEssayHelp,
                FirstChoiceCollege = student.FirstChoiceCollege,
                SecondChoiceCollege = student.SecondChoiceCollege,
                ThirdChoiceCollege = student.ThirdChoiceCollege,
                TradeSchoolStatus = student.TradeSchoolStatus,
                TradeSchoolsList = student.TradeSchoolsList,
                ArmedForcesStatus = student.ArmedForcesStatus,
                ArmedForcesBranch = student.ArmedForcesBranch,
                WorkStatus = student.WorkStatus,
                CareerPathList = student.CareerPathList,
                OtherPlans = student.OtherPlans,
                PACTTestDate = student.PACTTestDate,
                PACTTestScore = student.PACTTestScore,
                PSATTestDate = student.PSATTestDate,
                PSATTestScore = student.PSATTestScore,
                SATTestDate = student.SATTestDate,
                SATTestScore = student.SATTestScore,
                ACTTestDate = student.ACTTestDate,
                ACTTestScore = student.ACTTestScore,
                PrepClassRequired = student.PrepClassRequired,
                AssistanceForForms = student.AssistanceForForms,
                FinancialAidProcessComplete = student.FinancialAidProcessComplete,
                SupportNeeded = student.SupportNeeded,
                StudentSignature = student.StudentSignature,
                StudentSignatureDate = student.StudentSignatureDate,
                ParentSignature = student.ParentSignature,
                ParentSignatureDate = student.ParentSignatureDate,
                CoachId = student.CoachId,
                Active = student.Active
            };

            return View(viewModel);
        }
        public async Task<IActionResult> PostSecondaryEdit(Guid studentId)
        {
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("401: Unauthorized");
            }
            // Check Permissions
            /*
             *  Students: Allowed if owned by user
             *  Coach: Not Allowed
             *  Admin: Allowed
             */
            if (User.IsInRole(StringConstants.RoleNameStudent))
            {
                // Find user
                var user = await UserManager.GetUserAsync(User);
                // Check that studentId is the AssociatedId of the user

                if (user.StudentId == Guid.Empty || user.StudentId != studentId)

                {
                    return Error("403: Forbidden");
                }
            }
            else if (User.IsInRole(StringConstants.RoleNameCoach))

            {
                return Error("403: Forbidden");
            }
            // Find student
            var student = await _studentService.GetStudent(studentId);
            // Check for bad student

            if (student == null || studentId == Guid.Empty)

            {
                return Error("400: Bad Request");
            }
            EditViewModel viewModel = new EditViewModel()
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
                OldStudentEmail = student.StudentEmail,
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
                OldParentEmail = student.ParentEmail,
                //Guidance Counselor Info
                KnowGuidanceCounselor = student.KnowGuidanceCounselor,
                GuidanceCounselorName = student.GuidanceCounselorName,
                MeetWithGuidanceCounselor = student.MeetWithGuidanceCounselor,
                HowOftenMeetWithGuidanceCounselor = student.HowOftenMeetWithGuidanceCounselor,
                DiscussWithGuidanceCounselor = student.DiscussWithGuidanceCounselor,
                PlanAfterHighSchool = student.PlanAfterHighSchool,
                CollegeApplicationStatus = student.CollegeApplicationStatus,
                CollegesList = student.CollegesList,
                CollegeEssayStatus = student.CollegeEssayStatus,
                CollegeEssayHelp = student.CollegeEssayHelp,
                FirstChoiceCollege = student.FirstChoiceCollege,
                SecondChoiceCollege = student.SecondChoiceCollege,
                ThirdChoiceCollege = student.ThirdChoiceCollege,
                TradeSchoolStatus = student.TradeSchoolStatus,
                TradeSchoolsList = student.TradeSchoolsList,
                ArmedForcesStatus = student.ArmedForcesStatus,
                ArmedForcesBranch = student.ArmedForcesBranch,
                WorkStatus = student.WorkStatus,
                CareerPathList = student.CareerPathList,
                OtherPlans = student.OtherPlans,
                PACTTestDate = student.PACTTestDate,
                PACTTestScore = student.PACTTestScore,
                PSATTestDate = student.PSATTestDate,
                PSATTestScore = student.PSATTestScore,
                SATTestDate = student.SATTestDate,
                SATTestScore = student.SATTestScore,
                ACTTestDate = student.ACTTestDate,
                ACTTestScore = student.ACTTestScore,
                PrepClassRequired = student.PrepClassRequired,
                AssistanceForForms = student.AssistanceForForms,
                FinancialAidProcessComplete = student.FinancialAidProcessComplete,
                SupportNeeded = student.SupportNeeded,
                StudentSignature = student.StudentSignature,
                StudentSignatureDate = student.StudentSignatureDate,
                ParentSignature = student.ParentSignature,
                ParentSignatureDate = student.ParentSignatureDate,
                CoachId = student.CoachId,
                Active = student.Active
            };

            return View(viewModel);
        }
        public async Task<IActionResult> CollegeEntranceExamEdit(Guid studentId)
        {
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("401: Unauthorized");
            }
            // Check Permissions
            /*
             *  Students: Allowed if owned by user
             *  Coach: Not Allowed
             *  Admin: Allowed
             */
            if (User.IsInRole(StringConstants.RoleNameStudent))
            {
                // Find user
                var user = await UserManager.GetUserAsync(User);
                // Check that studentId is the AssociatedId of the user

                if (user.StudentId == Guid.Empty || user.StudentId != studentId)

                {
                    return Error("403: Forbidden");
                }
            }
            else if (User.IsInRole(StringConstants.RoleNameCoach))

            {
                return Error("403: Forbidden");
            }
            // Find student
            var student = await _studentService.GetStudent(studentId);
            // Check for bad student

            if (student == null || studentId == Guid.Empty)

            {
                return Error("400: Bad Request");
            }
            EditViewModel viewModel = new EditViewModel()
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
                OldStudentEmail = student.StudentEmail,
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
                OldParentEmail = student.ParentEmail,
                //Guidance Counselor Info
                KnowGuidanceCounselor = student.KnowGuidanceCounselor,
                GuidanceCounselorName = student.GuidanceCounselorName,
                MeetWithGuidanceCounselor = student.MeetWithGuidanceCounselor,
                HowOftenMeetWithGuidanceCounselor = student.HowOftenMeetWithGuidanceCounselor,
                DiscussWithGuidanceCounselor = student.DiscussWithGuidanceCounselor,
                PlanAfterHighSchool = student.PlanAfterHighSchool,
                CollegeApplicationStatus = student.CollegeApplicationStatus,
                CollegesList = student.CollegesList,
                CollegeEssayStatus = student.CollegeEssayStatus,
                CollegeEssayHelp = student.CollegeEssayHelp,
                FirstChoiceCollege = student.FirstChoiceCollege,
                SecondChoiceCollege = student.SecondChoiceCollege,
                ThirdChoiceCollege = student.ThirdChoiceCollege,
                TradeSchoolStatus = student.TradeSchoolStatus,
                TradeSchoolsList = student.TradeSchoolsList,
                ArmedForcesStatus = student.ArmedForcesStatus,
                ArmedForcesBranch = student.ArmedForcesBranch,
                WorkStatus = student.WorkStatus,
                CareerPathList = student.CareerPathList,
                OtherPlans = student.OtherPlans,
                PACTTestDate = student.PACTTestDate == DateTime.MinValue ? DateTime.Now : student.PACTTestDate,
                PACTTestScore = student.PACTTestScore,
                PSATTestDate = student.PSATTestDate == DateTime.MinValue ? DateTime.Now : student.PSATTestDate,
                PSATTestScore = student.PSATTestScore,
                SATTestDate = student.SATTestDate == DateTime.MinValue ? DateTime.Now : student.SATTestDate,
                SATTestScore = student.SATTestScore,
                ACTTestDate = student.ACTTestDate == DateTime.MinValue ? DateTime.Now : student.ACTTestDate,
                ACTTestScore = student.ACTTestScore,
                PrepClassRequired = student.PrepClassRequired,
                AssistanceForForms = student.AssistanceForForms,
                FinancialAidProcessComplete = student.FinancialAidProcessComplete,
                SupportNeeded = student.SupportNeeded,
                StudentSignature = student.StudentSignature,
                StudentSignatureDate = student.StudentSignatureDate,
                ParentSignature = student.ParentSignature,
                ParentSignatureDate = student.ParentSignatureDate,
                CoachId = student.CoachId,
                Active = student.Active
            };

            return View(viewModel);
        }

        public async Task<IActionResult> FinancialAidEdit(Guid studentId)
        {
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("401: Unauthorized");
            }
            // Check Permissions
            /*
             *  Students: Allowed if owned by user
             *  Coach: Not Allowed
             *  Admin: Allowed
             */
            if (User.IsInRole(StringConstants.RoleNameStudent))
            {
                // Find user
                var user = await UserManager.GetUserAsync(User);
                // Check that studentId is the AssociatedId of the user

                if (user.StudentId == Guid.Empty || user.StudentId != studentId)

                {
                    return Error("403: Forbidden");
                }
            }
            else if (User.IsInRole(StringConstants.RoleNameCoach))

            {
                return Error("403: Forbidden");
            }
            // Find student
            var student = await _studentService.GetStudent(studentId);
            // Check for bad student

            if (student == null || studentId == Guid.Empty)

            {
                return Error("400: Bad Request");
            }
            EditViewModel viewModel = new EditViewModel()
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
                OldStudentEmail = student.StudentEmail,
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
                OldParentEmail = student.ParentEmail,
                //Guidance Counselor Info
                KnowGuidanceCounselor = student.KnowGuidanceCounselor,
                GuidanceCounselorName = student.GuidanceCounselorName,
                MeetWithGuidanceCounselor = student.MeetWithGuidanceCounselor,
                HowOftenMeetWithGuidanceCounselor = student.HowOftenMeetWithGuidanceCounselor,
                DiscussWithGuidanceCounselor = student.DiscussWithGuidanceCounselor,
                PlanAfterHighSchool = student.PlanAfterHighSchool,
                CollegeApplicationStatus = student.CollegeApplicationStatus,
                CollegesList = student.CollegesList,
                CollegeEssayStatus = student.CollegeEssayStatus,
                CollegeEssayHelp = student.CollegeEssayHelp,
                FirstChoiceCollege = student.FirstChoiceCollege,
                SecondChoiceCollege = student.SecondChoiceCollege,
                ThirdChoiceCollege = student.ThirdChoiceCollege,
                TradeSchoolStatus = student.TradeSchoolStatus,
                TradeSchoolsList = student.TradeSchoolsList,
                ArmedForcesStatus = student.ArmedForcesStatus,
                ArmedForcesBranch = student.ArmedForcesBranch,
                WorkStatus = student.WorkStatus,
                CareerPathList = student.CareerPathList,
                OtherPlans = student.OtherPlans,
                PACTTestDate = student.PACTTestDate,
                PACTTestScore = student.PACTTestScore,
                PSATTestDate = student.PSATTestDate,
                PSATTestScore = student.PSATTestScore,
                SATTestDate = student.SATTestDate,
                SATTestScore = student.SATTestScore,
                ACTTestDate = student.ACTTestDate,
                ACTTestScore = student.ACTTestScore,
                PrepClassRequired = student.PrepClassRequired,
                AssistanceForForms = student.AssistanceForForms,
                FinancialAidProcessComplete = student.FinancialAidProcessComplete,
                SupportNeeded = student.SupportNeeded,
                StudentSignature = student.StudentSignature,
                StudentSignatureDate = student.StudentSignatureDate,
                ParentSignature = student.ParentSignature,
                ParentSignatureDate = student.ParentSignatureDate,
                CoachId = student.CoachId,
                Active = student.Active
            };

            return View(viewModel);
        }
        public async Task<IActionResult> SignatureEdit(Guid studentId)
        {
            if (!SignInManager.IsSignedIn(User))
            {
                return Error("401: Unauthorized");
            }
            // Check Permissions
            /*
             *  Students: Allowed if owned by user
             *  Coach: Not Allowed
             *  Admin: Allowed
             */
            if (User.IsInRole(StringConstants.RoleNameStudent))
            {
                // Find user
                var user = await UserManager.GetUserAsync(User);
                // Check that studentId is the AssociatedId of the user

                if (user.StudentId == Guid.Empty || user.StudentId != studentId)

                {
                    return Error("403: Forbidden");
                }
            }
            else if (User.IsInRole(StringConstants.RoleNameCoach))

            {
                return Error("403: Forbidden");
            }
            // Find student
            var student = await _studentService.GetStudent(studentId);
            // Check for bad student

            if (student == null || studentId == Guid.Empty)

            {
                return Error("400: Bad Request");
            }
            EditViewModel viewModel = new EditViewModel()
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
                OldStudentEmail = student.StudentEmail,
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
                OldParentEmail = student.ParentEmail,
                //Guidance Counselor Info
                KnowGuidanceCounselor = student.KnowGuidanceCounselor,
                GuidanceCounselorName = student.GuidanceCounselorName,
                MeetWithGuidanceCounselor = student.MeetWithGuidanceCounselor,
                HowOftenMeetWithGuidanceCounselor = student.HowOftenMeetWithGuidanceCounselor,
                DiscussWithGuidanceCounselor = student.DiscussWithGuidanceCounselor,
                PlanAfterHighSchool = student.PlanAfterHighSchool,
                CollegeApplicationStatus = student.CollegeApplicationStatus,
                CollegesList = student.CollegesList,
                CollegeEssayStatus = student.CollegeEssayStatus,
                CollegeEssayHelp = student.CollegeEssayHelp,
                FirstChoiceCollege = student.FirstChoiceCollege,
                SecondChoiceCollege = student.SecondChoiceCollege,
                ThirdChoiceCollege = student.ThirdChoiceCollege,
                TradeSchoolStatus = student.TradeSchoolStatus,
                TradeSchoolsList = student.TradeSchoolsList,
                ArmedForcesStatus = student.ArmedForcesStatus,
                ArmedForcesBranch = student.ArmedForcesBranch,
                WorkStatus = student.WorkStatus,
                CareerPathList = student.CareerPathList,
                OtherPlans = student.OtherPlans,
                PACTTestDate = student.PACTTestDate,
                PACTTestScore = student.PACTTestScore,
                PSATTestDate = student.PSATTestDate,
                PSATTestScore = student.PSATTestScore,
                SATTestDate = student.SATTestDate,
                SATTestScore = student.SATTestScore,
                ACTTestDate = student.ACTTestDate,
                ACTTestScore = student.ACTTestScore,
                PrepClassRequired = student.PrepClassRequired,
                AssistanceForForms = student.AssistanceForForms,
                FinancialAidProcessComplete = student.FinancialAidProcessComplete,
                SupportNeeded = student.SupportNeeded,
                StudentSignature = student.StudentSignature,
                StudentSignatureDate = student.StudentSignatureDate == DateTime.MinValue ? DateTime.Now : student.StudentSignatureDate,
                ParentSignature = student.ParentSignature,
                ParentSignatureDate = student.ParentSignatureDate == DateTime.MinValue ? DateTime.Now : student.ParentSignatureDate,
                CoachId = student.CoachId,
                Active = student.Active
            };

            return View(viewModel);
        }
        public async Task<IActionResult> ApplyingStudentsIndex()
        {
            if (User.IsInRole(StringConstants.RoleNameAdmin))

            {

                return View(await _studentService.GetApplyingStudents());

            }

            return Error("403: You are not authorized to view this page.");
        }
        public async Task<IActionResult> AcceptStudent(Guid studentId)
        {
            var student = await _studentService.GetStudent(studentId);
            student.Accepted = true;
            var student1 = await _studentService.Update(student);
            return RedirectToAction("CoachesStudents", "Coaches", new { id = student.CoachId });
        }
        public async Task<IActionResult> DeclineStudent(Guid studentId)
        {
            var student = await _studentService.GetStudent(studentId);
            student.Declined = true;
            var student1 = await _studentService.Update(student);
            return RedirectToAction("CoachesStudents", "Coaches", new { id = student.CoachId });
        }
    }
}

       