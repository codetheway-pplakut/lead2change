using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    /**
     * This class exists because the index goals page needs a way to remember the ID of the current student being looked at
     * even if the student initally has no goals (and thus no way to access the student ID from a goal model)
     */
    public class GoalsAndIDViewModel
    {

        public GoalsAndIDViewModel(Guid studentID)
        {
            this.StudentID = studentID;
        }
        public List<GoalViewModel> GoalModels { get; set; }
        public Guid StudentID { get; set; }
    }
}
