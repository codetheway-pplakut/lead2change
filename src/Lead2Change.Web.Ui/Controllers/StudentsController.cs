using Lead2Change.Domain.ViewModels;
using Lead2Change.Services.Identity;
using Lead2Change.Services.Students;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lead2Change.Web.Ui.Controllers
{
    public class StudentsController : _BaseController
    {
        IStudentService _studentService;

        public StudentsController(IIdentityService identityService, IStudentService studentService) : base(identityService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
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
                DiscussWithGuidanceCounselor = studentscontainer.DiscussWithGuidanceCounselor,
                PlanAfterHighSchool = studentscontainer.PlanAfterHighSchool,
                CollegeApplicationStatus = studentscontainer.CollegeApplicationStatus,
                CollegesList = studentscontainer.CollegesList,
                CollegeEssayStatus = studentscontainer.CollegeEssayStatus,
                CollegeEssayHelp = studentscontainer.CollegeEssayHelp,
                FirstChoiceCollege = studentscontainer.FirstChoiceCollege,
                SecondChoiceCollege = studentscontainer.SecondChoiceCollege,
                ThirdChoiceCollege = studentscontainer.ThirdChoiceCollege,
                TradeSchoolStatus = studentscontainer.TradeSchoolStatus,
                TradeSchoolsList = studentscontainer.TradeSchoolsList,
                ArmedForcesStatus = studentscontainer.ArmedForcesStatus,
                ArmedForcesBranch = studentscontainer.ArmedForcesBranch,
                WorkStatus = studentscontainer.WorkStatus,
                CareerPathList = studentscontainer.CareerPathList,
                OtherPlans = studentscontainer.OtherPlans,
                PACTTestDate = studentscontainer.PACTTestDate,
                PACTTestScore = studentscontainer.PACTTestScore,
                PSATTestDate = studentscontainer.PSATTestDate,
                PSATTestScore = studentscontainer.PSATTestScore,
                SATTestDate = studentscontainer.SATTestDate,
                SATTestScore = studentscontainer.SATTestScore,
                ACTTestDate = studentscontainer.ACTTestDate,
                ACTTestScore = studentscontainer.ACTTestScore,
                PrepClassRequired = studentscontainer.PrepClassRequired,
                AssistanceForForms = studentscontainer.AssistanceForForms,
                FinancialAidProcessComplete = studentscontainer.FinancialAidProcessComplete,
                SupportNeeded = studentscontainer.SupportNeeded,
                StudentSignature = studentscontainer.StudentSignature,
                StudentSignatureDate = studentscontainer.StudentSignatureDate,
                ParentSignature = studentscontainer.ParentSignature,
                ParentSignatureDate = studentscontainer.ParentSignatureDate
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
                        //ParentAddress
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
                        DiscussWithGuidanceCounselor = model.DiscussWithGuidanceCounselor,
                        PlanAfterHighSchool = model.PlanAfterHighSchool,
                        CollegeApplicationStatus = model.CollegeApplicationStatus,
                        CollegesList = model.CollegesList,
                        CollegeEssayStatus = model.CollegeEssayStatus,
                        CollegeEssayHelp = model.CollegeEssayHelp,
                        FirstChoiceCollege = model.FirstChoiceCollege,
                        SecondChoiceCollege = model.SecondChoiceCollege,
                        ThirdChoiceCollege = model.ThirdChoiceCollege,
                        TradeSchoolStatus = model.TradeSchoolStatus,
                        TradeSchoolsList = model.TradeSchoolsList,
                        ArmedForcesStatus = model.ArmedForcesStatus,
                        ArmedForcesBranch = model.ArmedForcesBranch,
                        WorkStatus = model.WorkStatus,
                        CareerPathList = model.CareerPathList,
                        OtherPlans = model.OtherPlans,
                        PACTTestDate = model.PACTTestDate,
                        PACTTestScore = model.PACTTestScore,
                        PSATTestDate = model.PSATTestDate,
                        PSATTestScore = model.PSATTestScore,
                        SATTestDate = model.SATTestDate,
                        SATTestScore = model.SATTestScore,
                        ACTTestDate = model.ACTTestDate,
                        ACTTestScore = model.ACTTestScore,
                        PrepClassRequired = model.PrepClassRequired,
                        AssistanceForForms = model.AssistanceForForms,
                        FinancialAidProcessComplete = model.FinancialAidProcessComplete,
                        SupportNeeded = model.SupportNeeded,
                        StudentSignature = model.StudentSignature,
                        StudentSignatureDate = model.StudentSignatureDate,
                        ParentSignature = model.ParentSignature,
                        ParentSignatureDate = model.ParentSignatureDate

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
                ParentSignatureDate = student.ParentSignatureDate
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
                        DiscussWithGuidanceCounselor = model.DiscussWithGuidanceCounselor,
                        PlanAfterHighSchool = model.PlanAfterHighSchool,
                        CollegeApplicationStatus = model.CollegeApplicationStatus,
                        CollegesList = model.CollegesList,
                        CollegeEssayStatus = model.CollegeEssayStatus,
                        CollegeEssayHelp = model.CollegeEssayHelp,
                        FirstChoiceCollege = model.FirstChoiceCollege,
                        SecondChoiceCollege = model.SecondChoiceCollege,
                        ThirdChoiceCollege = model.ThirdChoiceCollege,
                        TradeSchoolStatus = model.TradeSchoolStatus,
                        TradeSchoolsList = model.TradeSchoolsList,
                        ArmedForcesStatus = model.ArmedForcesStatus,
                        ArmedForcesBranch = model.ArmedForcesBranch,
                        WorkStatus = model.WorkStatus,
                        CareerPathList = model.CareerPathList,
                        OtherPlans = model.OtherPlans,
                        PACTTestDate = model.PACTTestDate,
                        PACTTestScore = model.PACTTestScore,
                        PSATTestDate = model.PSATTestDate,
                        PSATTestScore = model.PSATTestScore,
                        SATTestDate = model.SATTestDate,
                        SATTestScore = model.SATTestScore,
                        ACTTestDate = model.ACTTestDate,
                        ACTTestScore = model.ACTTestScore,
                        PrepClassRequired = model.PrepClassRequired,
                        AssistanceForForms = model.AssistanceForForms,
                        FinancialAidProcessComplete = model.FinancialAidProcessComplete,
                        SupportNeeded = model.SupportNeeded,
                        StudentSignature = model.StudentSignature,
                        StudentSignatureDate = model.StudentSignatureDate,
                        ParentSignature = model.ParentSignature,
                        ParentSignatureDate = model.ParentSignatureDate
                    };
                    var student = await _studentService.Update(list);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}