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
            AddedQuestions = new List<Question>();
        }
        public List<Question> AddedQuestions { get; set; }

        public List<Question> UnselectedQuestions { get; set; } // Used in the Question Select View
        public Guid Id { get; set; }
        public string InterviewName { get; set; }
       
        public string QuestionText { get; set; } // Used in the Interview Question Create View
    }
}
