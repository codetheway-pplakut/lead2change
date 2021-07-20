using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class AnswerQuestionViewModel
    {
        public ICollection<Answer> Answers { get; set; }
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid QuestionId { get; set; }
        public ICollection<QuestionInInterview> QuestionInInterviews { get; set; }
        public string InterviewName { get; set; }
    }
}
