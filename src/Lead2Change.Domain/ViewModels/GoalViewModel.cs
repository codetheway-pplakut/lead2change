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
        [Display(Name ="Goal:")]
        public string GoalSet { get; set; }
        [Display(Name = "Date Goal Set:")]
        public DateTime DateGoalSet { get; set; }
        [Display(Name = "Social Emotional Learning (SEL):")]
        public string SEL { get; set; }
        [Display(Name = "Goal Review Date:")]
        public DateTime GoalReviewDate { get; set; }
        [Display(Name = "Was It Accomplished?")]
        public string WasItAccomplished { get; set; }
        [Display(Name = "Explanation:")]
        public string Explanation { get; set; }
    }
}
