using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Lead2Change.Domain.ViewModels
{
    public class GoalViewModel
    {
    
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string GoalSet { get; set; }
        public DateTime DateGoalSet { get; set; }
        public string SEL { get; set; }
        public DateTime GoalReviewDate { get; set; }
        public string WasItAccomplished { get; set; }
        public string Explanation { get; set; }
    }
}
