using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lead2Change.Domain.Models
{
    public class Answer
    {
        [Display(Name = "Answer: ")]
        public string AnswerString { get; set; }
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid QuestionId { get; set; }
        public string QuestionString { get; set; }
        public Guid InterviewId { get; set; }
        public bool IsArchived { get; set; }
        public string StudentName { get; set; }
        public string InterviewName { get; set; }
    }
}
