using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class StudentInterviewAndIDViewModel
    {
        public StudentInterviewAndIDViewModel(Guid interviewID)
        {
            this.InterviewID = interviewID;
        }
        public List<AnswersViewModel> AnswerModels { get; set; }
        public Guid InterviewID { get; set; }
        public string InterviewName { get; set; }
        public string StudentName { get; set; }
        public Guid StudentId { get; set; }
    }
}
