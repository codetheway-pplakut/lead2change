using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Lead2Change.Domain.ViewModels
{
    public class StudentInterviewViewModel
    {
        public List<Student> Students { get; set; }
        public List<AnswerQuestionViewModel> StudentAnswer { get; set; }
    }
}
