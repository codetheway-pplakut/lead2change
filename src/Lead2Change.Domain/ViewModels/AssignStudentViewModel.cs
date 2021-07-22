using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    class AssignStudentViewModel
    {
        public Coach CurrentCoach { get; set; }
        public List<Student> UnassignedStudents { get; set; }
        public Student StudentBeingAdded { get; set; }
    }
}
