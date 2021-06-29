using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.Models
{
    public class CareerDeclaration
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public bool CollegeBound { get; set; }
        public int CareerCluster { get; set; }
        public string SpecificCareer { get; set; }
        public bool TechnicalCollegeBound { get; set; }
    }
}
