using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Lead2Change.Domain.ViewModels
{
    public class StudentInterview
    {
        public List<Student> Students { get; set; }
        public AnswerQuestionViewModel StudentAnswer { get; set; }
    }
}
