using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class StudentInterestFormViewModel
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentCellPhone { get; set; }
        public DateTime StudentDateOfBirth { get; set; }
    }
}
