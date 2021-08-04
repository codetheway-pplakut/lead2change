using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class CoachViewModel
    {
        public Guid Id { get; set; }
        public String CoachFirstName { get; set; }
        public String CoachLastName { get; set; } //if applicable
        public String CoachEmail { get; set; }
        public String CoachPhoneNumber { get; set; }
        public List<Student> Students { get; set; }
        public bool Active { get; set; }
    }
}
