using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.Models
{
    public class QuestionInInterview
    {
        public Guid InterviewId { get; set; }
        public Guid QuestionId { get; set; }
        public Interview Interview { get; set; }
        public Question Question { get; set; }
        public int Order { get; set; }
    }
}
