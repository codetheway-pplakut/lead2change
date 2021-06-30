using Lead2Change.Domain.ViewModels;
using Lead2Change.Services.Identity;
using Lead2Change.Services.Students;
using Microsoft.AspNetCore.Mvc;
using Lead2Change.Domain.Models;
using Lead2Change.Domain.ViewModels;
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
        public async Task<IActionResult> Create()
        {
            return View(new RegistrationViewModel());
        }
        /* [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model)
         {
             if (ModelState.IsValid)
             {

                     Student student = new Student()
                     {
                         public Guid Id { get; set; }
         public string StudentFirstName { get; set; }
         public string StudentLastName { get; set; }
         public DateTime StudentDateOfBirth { get; set; }
         public string StudentAddress { get; set; }
         public string StudentApartmentNumber { get; set; }
         public string StudentCity { get; set; }
         public int StudentZipCode { get; set; }
         public int StudentHomePhone { get; set; }
         public int StudentCellPhone { get; set; }
         public string StudentEmail { get; set; }
         public string StudentCareerPath { get; set; }
         public string StudentCareerInterest { get; set; }


         public string ParentFirstName { get; set; }
         public string ParentLastName { get; set; }
         public string Address { get; set; }

         public string ParentCity { get; set; }
         public string ParentState { get; set; }
         public int ParentZipCode { get; set; }
         public int ParentHomePhone { get; set; }
         public int ParentCellPhone { get; set; }
         public string ParentEmail { get; set; }

         public bool KnowGuidanceCounselor { get; set; }
         public string GuidanceCounselorName { get; set; }
         public bool MeetWithGuidanceCounselor { get; set; }
         public string HowOftenMeetWithGuidanceCounselor { get; set; }
         public string DiscussWithGuidanceCounselor { get; set; }

     };
                     var abc = await StudentService.Create(student);
                 return RedirectToAction("Index");
             }
             return View(model);
         } */
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
                if(model.StudentFirstName.Length > 0)
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
    }

   
}
