using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    /**
     * This class is used to contain the added information needed by the page which creates both an interview and its questions
     * 
     */
    public class InterviewQuestionCreateViewModel
    {
        public InterviewQuestionCreateViewModel()
        {
            QuestionInInterviews = new List<QuestionInInterview>();
        }
        public List<QuestionInInterview> QuestionInInterviews { get; set; }

        public List<Question> UnselectedQuestions { get; set; } // Used in the Quesiton Select View
        public Guid Id { get; set; }
        public string InterviewName { get; set; }
       
        public string QuestionText { get; set; }
    }
}
