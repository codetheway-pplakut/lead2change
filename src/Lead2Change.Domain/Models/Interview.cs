using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.Models
{
    public class Interview
    {
        public ICollection<QuestionInInterview> QuestionInInterviews { get; set; }

        public string InterviewName { get; set; }

        public Guid Id { get; set; }
    }
}
