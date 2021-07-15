using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class QuestionsViewModel
    {
        [Display(Name = "Question:")]
        public string QuestionString { get; set; }
        public Guid Id { get; set; }
        public ICollection<QuestionInInterview> QuestionInInterviews { get; set; }
    }
}
