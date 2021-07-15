using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class InterviewViewModel
    {
        public ICollection<QuestionInInterview> QuestionInInterviews { get; set; }
        public Guid Id { get; set; }
    }
}
