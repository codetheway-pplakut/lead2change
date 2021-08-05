using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.ViewModels
{
    public class StudentIndexViewModel
    {
        public List<Student> Students { get; set; }
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
