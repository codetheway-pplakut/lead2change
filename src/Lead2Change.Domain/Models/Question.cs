using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.Models
{
    public class Question
    {
        public Question()
        {
            this.QuestionInInterviews = new List<QuestionInInterview>();
        }
        public string QuestionString { get; set; }

        public Guid Id { get; set; }

        public ICollection<QuestionInInterview> QuestionInInterviews { get; set; }

        public bool IsArchived { get; set; }

    }
}
