using Lead2Change.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class CareerDeclarationViewModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public bool CollegeBound { get; set; }
        public CareerCluster CareerCluster { get; set; }
        public string SpecificCareer { get; set; }
        public bool TechnicalCollegeBound { get; set; }
    }
}
