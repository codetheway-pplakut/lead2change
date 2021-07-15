﻿using Lead2Change.Domain.ViewModels;
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
            return View(await _studentService.GetActiveStudents());
        }

        public async Task<IActionResult> InactiveIndex()
        {
            return View(await _studentService.GetInactiveStudents());
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
                Active = studentscontainer.Active
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
                        DiscussWithGuidanceCounselor = model.DiscussWithGuidanceCounselor,
                        Active = true
                    };
                    var abc = await _studentService.Create(student);
                    await Email("1joel.kuriakose@gmail.com", model.ParentEmail, "Lead2Change Registration Confirmation: Your student is registered ", "Your student " + model.StudentFirstName + " " + model.StudentLastName + " has registered for Lead2Change!", "Your student " + model.StudentFirstName + " " + model.StudentLastName+ " has registered for Lead2Change!", "Lead2Change Student Registration", model.ParentFirstName + " " + model.ParentLastName);
                    await Email("1joel.kuriakose@gmail.com", "joeljk2003@gmail.com", "Lead2Change Student Registration Confirmation: A new student has been registered", model.StudentFirstName + " " + model.StudentLastName + " is a new registered student in Lead2Change!", model.StudentFirstName + " " + model.StudentLastName + " is a new registered student in Lead2Change!", "Lead2Change Student Registration", "Lead2Change");
                    await Email("1joel.kuriakose@gmail.com", model.StudentEmail, "Lead2Change Registration Confirmation: You are registered", "Congrats, you have sucessfully registered for Lead2Change!", "Congrats, you have sucessfully registered for Lead2Change!", "Lead2Change Student Registration", model.StudentFirstName + " " + model.StudentLastName);
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
                Active = student.Active
            };
            return View(list);
        }

        public async Task<IActionResult> Update(RegistrationViewModel model)
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
                        Active = model.Active
                    };
                    var student = await _studentService.Update(list);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}