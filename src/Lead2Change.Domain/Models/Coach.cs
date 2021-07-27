using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.Models
{
    public class Coach
    {
        /*public Coach()
        {
            this.Students = new List<Student>();
        }
        public ICollection<Student> Students { get; set; }
        */
        public Guid Id { get; set; }
        public String CoachFirstName { get; set; }
        public String CoachLastName { get; set; } //if applicable
        public String CoachEmail { get; set; }
        public String CoachPhoneNumber { get; set; }
        public List<Student> Students { get; set; }
    }
}
