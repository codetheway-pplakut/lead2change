using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.Models
{
    public class Goal
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string CreatedOn { get; set; }
    }
}
