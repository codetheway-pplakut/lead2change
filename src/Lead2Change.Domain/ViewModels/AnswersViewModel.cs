using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Lead2Change.Domain.ViewModels
{
    public class AnswersViewModel
    {
        [Display(Name = "Answer:")]
        public string AnswerString { get; set; }
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid QuestionId { get; set; }
    }
}
