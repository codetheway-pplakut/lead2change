using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public bool Enrolled { get; set; } //NOTE: This is an example only. Enrollment is more complicated than this.
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
