using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class GoalViewModel
    {
        [Required]
        public string Goal { get; set; }
    }
}
