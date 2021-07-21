using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    /**
     * This class is used to contain the added information needed by the page which creates both an interview and it's questions
     * 
     */
    public class InterviewQuestionCreateViewModel
    {
        public InterviewQuestionCreateViewModel()
        {
            QuestionInInterviews = new List<QuestionInInterview>();
        }
        public List<QuestionInInterview> QuestionInInterviews { get; set; }
        public Guid Id { get; set; }
        public string InterviewName { get; set; }
       
        public string QuestionText { get; set; }
    }
}
