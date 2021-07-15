using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class AnswerViewModel
    {
       // [Display(Name = "Answer String:")]
        public string AnswerString { get; set; }

        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid QuestionId { get; set; }
    }
}
