using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class AnswerAndIDViewModel
    {
        public AnswerAndIDViewModel(Guid studentID)
        {
            this.StudentID = studentID;
        }
        public List<AnswersViewModel> AnswerModels { get; set; }
        public Guid StudentID { get; set; }
    }
}

