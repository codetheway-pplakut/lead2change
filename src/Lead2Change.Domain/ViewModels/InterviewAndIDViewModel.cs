using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class InterviewAndIDViewModel
    {
        public InterviewAndIDViewModel(Guid studentID)
        {
            StudentId = studentID;
        }
        public List<InterviewViewModel> InterviewViewModels { get; set; }
        public Guid InterviewId { get; set; }
        public Guid StudentId { get; set; }
        public Guid Id { get; set; }
     public string InterviewName { get; set; }
        public string StudentName { get; set; }
    }

}
