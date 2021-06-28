using System;
using System.Collections.Generic;
using System.Text;

namespace Lead2Change.Domain.Models
{
    public class DbResponse<T>
    {
        public T Entity { get; set; }
        public string Status { get; set; } //NOTE: Could be an enum
        public string Message { get; set; }
    }
}
